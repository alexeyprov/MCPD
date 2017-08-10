using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;

using Northwind.Data.Entities;

namespace Northwind.UI.Entities
{
	public partial class ChartingPage : Northwind.UI.NorthwindBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			using (NorthwindObjectContext context = new NorthwindObjectContext(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString))
			{
				var products = context.Products.Where(p => !p.Discontinued).Select(p => p).Take(5);
				dataChart.Series["ProductPrice"].Points.DataBind(products, "ProductName", "UnitPrice", String.Empty);
				dataChart.Series["ProductStock"].Points.DataBind(products, "ProductName", "UnitsInStock", String.Empty);
			}
		}
	}
}