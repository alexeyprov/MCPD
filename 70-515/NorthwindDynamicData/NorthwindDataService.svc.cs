using System.Data.Services;
using System.Data.Services.Common;

using Northwind.Data.Entities;

namespace NorthwindDynamicData
{
	public class NorthwindDataService : DataService<NorthwindObjectContext>
	{
		// This method is called only once to initialize service-wide policies.
		public static void InitializeService(DataServiceConfiguration config)
		{
			// TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
			// Examples:
			// config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
			// config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
			config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;

			config.SetEntitySetAccessRule("Customers", EntitySetRights.AllRead);
			config.SetEntitySetAccessRule("Orders", EntitySetRights.AllRead);
			config.SetEntitySetAccessRule("Order_Details", EntitySetRights.AllRead | EntitySetRights.WriteReplace | EntitySetRights.WriteMerge);
		}
	}
}
