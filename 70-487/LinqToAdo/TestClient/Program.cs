using System;
using System.Configuration;
using System.Data.Common;
using System.Linq;

namespace LinqToAdo.TestClient
{
    internal static class Program
    {
        private static void Main()
        {
            ConnectionStringSettings connectionInfo = ConfigurationManager.ConnectionStrings["Default"];

            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionInfo.ProviderName);

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionInfo.ConnectionString;
                connection.Open();

                NorthwindObjectContext context = new NorthwindObjectContext(connection);

                string city = "London";

                var customers =
                    from c in context.Customers
                    where c.City == city
                    select new
                    {
                        c.ContactName,
                        c.Phone
                    };

                Console.WriteLine(customers);

                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.ContactName);
                }
            }
        }
    }
}
