using System.Linq;
using System.Web.Configuration;
using System.Web.Script.Services;
using System.Web.Services;

using Northwind.Data.Linq;

namespace Northwind.Services
{
	/// <summary>
	/// Summary description for TerritoriesService
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[ScriptService]
	public class TerritoriesWebService : WebService
	{
		public TerritoriesWebService()
		{
			//Uncomment the following line if using designed components 
			//InitializeComponent(); 
		}

		[WebMethod]
		public Northwind.Data.Linq.Territory[] GetTerritoriesByRegion(int regionId)
		{
			string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
			NorthwindDataContext context = new NorthwindDataContext(connString);

			// we don't want to load anything else (from relations)
			context.DeferredLoadingEnabled = false;

			return context.Territories.Where(t => t.RegionID == regionId).ToArray();
		}

	}
}
