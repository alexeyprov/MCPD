using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AdventureWorks.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AWDataAccessComponents.Tests
{
    [TestClass]
    public class EdmTest
    {
        [TestMethod]
        public void TestVendors()
        {
            using (Entities context = new Entities())
            {
                IEnumerable<Vendor> vendors = from v in context.Set<Vendor>().Include("VendorContacts")
                                              where v.Name.StartsWith("A")
                                              select v;

                vendors = vendors.ToList();

                Assert.IsTrue(vendors.All(v => v.ActiveFlag));
            }
        }

        [TestMethod]
        public void TestEntitySql()
        {
            using (Entities context = new Entities())
            {
                ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

                // define ObjectQuery<T>
                // IQueryable allows to extend the query further if necessary
                IQueryable<DbDataRecord> query = objectContext.CreateQuery<DbDataRecord>(
                    @"
  SELECT Title,
         ANYELEMENT(SELECT COUNT(gc.CustomerID)
                      FROM Entities.IndividualCustomers AS c
                     WHERE c.Title = gc.Title),
         (SELECT VALUE c
            FROM Entities.IndividualCustomers AS c
           WHERE c.Title = gc.Title)
    FROM Entities.IndividualCustomers AS gc
   WHERE gc.Title IS NOT NULL
     AND gc.Title <> @exclusion
GROUP BY gc.Title
ORDER BY gc.Title",
                  new ObjectParameter("exclusion", "Sr."));

                // execute ObjectQuery<T>
                DbDataRecord firstRecord = query.First();

                object title = firstRecord["Title"];
                Assert.IsInstanceOfType(title, typeof(string));
                Assert.AreEqual("Mr.", (string)title);

                object count = firstRecord[1];
                Assert.IsInstanceOfType(count, typeof(DbDataRecord));

                // unwrap, bcz VALUE is not used in sub-query
                count = ((DbDataRecord)count)[0];
                Assert.IsInstanceOfType(count, typeof(int));
                Assert.IsTrue((int)count > 0);

                object misters = firstRecord[2];
                Assert.IsInstanceOfType(misters, typeof(IEnumerable<IndividualCustomer>));
            }
        }

        [TestMethod] 
        public void TestEntityClient()
        {
            // AWDataAccessComponents assembly is not loaded for this method
            // hence, we cannot use connection string with wildcard pattern
            using (EntityConnection connection = new EntityConnection("name=EntitiesWithAssembly"))
            {
                connection.Open();

                EntityCommand command = new EntityCommand(
                    @"
SELECT TREAT(c AS AdventureWorksModel.Customer),
       (SELECT h FROM c.SalesOrderHeaders AS h)
  FROM OFTYPE(Entities.Counterparties, AdventureWorksModel.Customer) AS c
 WHERE c.ModifiedDate > DATETIME'2004-10-13 11:16'",
                    connection);

                using (EntityDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess))
                {
                    Assert.IsTrue(reader.Read());

                    object customer = reader[0];
                    Assert.IsInstanceOfType(customer, typeof(DbDataRecord));
                    DbDataRecord customerRecord = (DbDataRecord)customer;
                    Assert.IsTrue(customerRecord.FieldCount > 0);

                    object readerObject = reader[1];
                    Assert.IsInstanceOfType(readerObject, typeof(DbDataReader));

                    using (DbDataReader headerReader = (DbDataReader)readerObject)
                    {
                        while (headerReader.Read())
                        {
                            // SalesOrderHeader-shaped record is wrapped
                            // bcz "VALUE" was not used.
                            Assert.AreEqual(1, headerReader.FieldCount);
                            object wrapper = headerReader[0];
                            Assert.IsInstanceOfType(wrapper, typeof(DbDataRecord));

                            object headerId = ((DbDataRecord)wrapper)[0];
                            Assert.IsInstanceOfType(headerId, typeof(int));
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestStoredProcs()
        {
            Address address;

            using (Entities context = new Entities())
            {
                address = new Address
                {
                    AddressLine1 = "100 Center Grove rd",
                    City = "Randolph",
                    StateProvince = context.StateProvinces.Single(s => s.StateProvinceCode == "NJ"),
                    PostalCode = "07869"
                };

                context.Addresses.Add(address);

                context.SaveChanges();
            }

            Assert.IsTrue(address.AddressID > 0);

            using (Entities context = new Entities())
            {
                context.Set<Address>().Attach(address);
                address.City = "Dover";
                context.SaveChanges();
            }

            using (Entities context = new Entities())
            {
                context.Set<Address>().Attach(address);
                context.Addresses.Remove(address);
                context.SaveChanges();
            }
        }
    }
}
