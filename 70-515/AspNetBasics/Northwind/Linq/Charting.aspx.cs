using System;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

using Northwind.Data.Linq;

namespace Northwind.UI.Linq
{
	public partial class ChartingPage : Northwind.UI.NorthwindBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			using (NorthwindDataContext context = new NorthwindDataContext(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString))
			{
				var products = context.Products.Where(p => !p.Discontinued).Select(p => p).Take(5);
				dataChart.Series["ProductPrice"].Points.DataBind(products, "ProductName", "UnitPrice", String.Empty);
				dataChart.Series["ProductStock"].Points.DataBind(products, "ProductName", "UnitsInStock", String.Empty);
			}
		}
	}
}