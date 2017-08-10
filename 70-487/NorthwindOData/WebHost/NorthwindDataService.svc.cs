using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Web;
using System.Web.Configuration;

using Northwind.Data.Entities;

namespace NorthwindOData.WebHost
{
    public class NorthwindDataService : DataService<NorthwindObjectContext>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Customers", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Orders", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule(
                "Order_Details", 
                EntitySetRights.AllRead | EntitySetRights.WriteReplace | EntitySetRights.WriteMerge);

            config.SetServiceOperationAccessRule("CustomerOrderHistory", ServiceOperationRights.AllRead);

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        [WebGet]
        public IQueryable<CustomerOrderHistoryItem> CustomerOrderHistory(string customerId)
        {
            return CurrentDataSource.GetCustomerOrderHistory(customerId).AsQueryable();
        }

        [QueryInterceptor("Customers")]
        public Expression<Func<Customer, bool>> OnCustomerSelecting()
        {
            string filter = WebConfigurationManager.AppSettings["customerCountryFilter"];

            return string.IsNullOrEmpty(filter) ?
                (Expression<Func<Customer, bool>>) (c => true) :
                c => c.Address.Country == filter;
        }

        [ChangeInterceptor("Order_Details")]
        public void OnOrderLineChanging(OrderLine line, UpdateOperations operation)
        {
            if (operation != UpdateOperations.Add && operation != UpdateOperations.Change)
            {
                throw new DataServiceException(
                    string.Format("Unsupported operation: {0}", operation));
            }

            if (line.Quantity <= 0)
            {
                throw new DataServiceException("Quantity must be greater than zero");
            }
            if (line.UnitPrice <= 0)
            {
                throw new DataServiceException("Unit price must be greater than zero");
            }
            if (line.Discount < 0)
            {
                throw new DataServiceException("Discount cannot be less than zero");
            }
        }
    }
}
