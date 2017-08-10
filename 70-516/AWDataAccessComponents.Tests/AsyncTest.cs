using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using AdventureWorks.Data.Entities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AWDataAccessComponents.Tests
{
    [TestClass]
    public class AsyncTest
    {
        private const decimal MIN_TOTAL_PRICE = 996M;

        [TestMethod]
        public void TestAsyncAdoReader()
        {
            Task t = AsyncAdoHelper();
            t.Wait();
        }

        [TestMethod]
        public void TestAsyncEntityReader()
        {
            Task t = AsyncEntityHelper();
            t.Wait();
        }

        private static async Task AsyncAdoHelper()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["AdoNet"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);

            using (DbConnection cn = factory.CreateConnection())
            {
                cn.ConnectionString = settings.ConnectionString;
                await cn.OpenAsync();

                using (DbCommand cmd = factory.CreateCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = @"
SELECT p.Name,
       ListPrice
  FROM Production.Product p
 INNER JOIN Production.ProductSubcategory sc ON sc.ProductSubcategoryID = p.ProductSubcategoryID
 INNER JOIN Production.ProductCategory pc ON pc.ProductCategoryId = sc.ProductCategoryID
   AND pc.Name = 'Accessories'";
                    cmd.CommandType = CommandType.Text;

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        decimal sum = 0M;
                        while (await reader.ReadAsync())
                        {
                            sum += reader.GetDecimal(1);
                        }

                        Assert.IsTrue(sum > MIN_TOTAL_PRICE);
                    }
                }
            }
        }

        private static async Task AsyncEntityHelper()
        {
            using (EntityConnection cn = new EntityConnection("name=EntitiesWithAssembly"))
            {
                await cn.OpenAsync();

                using (EntityTransaction tx = cn.BeginTransaction())
                {
                    const decimal price = 99.98M;
                    // insert a new product to update the total price
                    InsertProduct(cn, tx, price);

                    const string sql = @"
SELECT p.Name,
       p.ListPrice
  FROM Entities.Products AS p
 WHERE p.ProductSubCategory.ProductCategory.Name = 'Accessories'";

                    using (EntityCommand cmd = new EntityCommand(sql, cn, tx))
                    {
                        using (DbDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                        {
                            decimal sum = 0M;
                            while (await reader.ReadAsync())
                            {
                                sum += (decimal)reader["ListPrice"];
                            }

                            Assert.IsTrue(sum > MIN_TOTAL_PRICE + price);
                        }
                    }

                    // rollback insert
                    tx.Rollback();
                }
            }
        }

        private static void InsertProduct(EntityConnection cn, EntityTransaction tx, decimal price)
        {
            using (Entities ctx = new Entities(cn))
            {
                // already enlisted in this transaction
                //ctx.Database.UseTransaction(tx.StoreTransaction);

                ProductSubcategory subCategory = ctx.ProductSubcategories.Single(sc => sc.Name == "Helmets");

                subCategory.Products.Add(
                    new Product
                    {
                        Name = "Sex Toy",
                        ProductNumber = "ST-1234",
                        SafetyStockLevel = 20,
                        ReorderPoint = 30,
                        StandardCost = 90M,
                        ListPrice = price,
                        DaysToManufacture = 28,
                        SellStartDate = DateTime.Now
                    });

                ctx.SaveChanges();
            }
        }
    }
}
