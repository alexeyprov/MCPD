using System.Data.Common;
using System.Linq;

using LinqToAdo.Provider;

namespace LinqToAdo.TestClient
{
    public class NorthwindObjectContext
    {
        public NorthwindObjectContext(DbConnection connection)
        {
            QueryProvider provider = new AdoQueryProvider(connection, true);

            Customers = new Query<Customer>(provider);
            Orders = new Query<Order>(provider);
        }

        public IQueryable<Customer> Customers
        {
            get;
            private set;
        }

        public IQueryable<Order> Orders
        {
            get;
            private set;
        }
    }
}
