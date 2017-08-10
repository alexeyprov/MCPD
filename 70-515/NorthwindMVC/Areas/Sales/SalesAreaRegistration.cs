using System.Web.Mvc;

namespace NorthwindMVC.Areas.Sales
{
	public class SalesAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Sales";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
				"Orders", // Route name
				"Sales/Order/{pageIndex}", // URL with parameters
				new
				{
					controller = "Order",
					action = "Index",
					pageIndex = UrlParameter.Optional
				} // Parameter defaults
			);

			context.MapRoute(
				"Sales_default",
				"Sales/{controller}/{action}/{id}",
				new
				{
					action = "Index",
					id = UrlParameter.Optional
				}
			);
		}
	}
}
