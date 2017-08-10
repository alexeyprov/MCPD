using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;

namespace Northwind.Services
{
	[ServiceContract(Namespace = "Northwind.Services")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class TerritoriesWcfService
	{
		// To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
		// To create an operation that returns XML,
		//     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
		//     and include the following line in the operation body:
		//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
		[OperationContract]
		public Territory[] GetTerritoriesByRegion(int regionId)
		{
			string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;

			var context = new Northwind.Data.Entities.NorthwindObjectContext(connString);

			return context.Territories.Where(t => t.Region.RegionID == regionId).
				Select(t => new Territory() { ID = t.TerritoryID, Description = t.TerritoryDescription}).ToArray();
		}

		// Add more operations here and mark them with [OperationContract]
	}
}
