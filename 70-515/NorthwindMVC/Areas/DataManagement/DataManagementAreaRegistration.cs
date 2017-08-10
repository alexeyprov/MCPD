using System.Web.Mvc;

namespace NorthwindMVC.Areas.DataManagement
{
	public class DataManagementAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "DataManagement";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"DataManagement_default",
				"DataManagement/{controller}/{action}/{id}",
				new
					{
						controller = "Supplier",
						action = "Index",
						id = UrlParameter.Optional
					} /*,
				new
					{
						id = @"\d+"
					}*/);
		}
	}
}
