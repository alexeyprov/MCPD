using System;
using System.Data.Common;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

using Northwind.Data.ClassicAdo;

namespace Northwind.UI.Ado
{
	public partial class ChartingPage : Northwind.UI.NorthwindBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ProductData dac = new ProductData(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);
			//dataChart.Series.Clear();

			using (DbDataReader reader = dac.GetProductStatistics())
			{
				//dataChart.DataBindTable(reader);
				//dataChart.Series["UNITSINSTOCK"].ChartType = SeriesChartType.StackedBar;
				dataChart.Series[0].Points.DataBindXY(reader, "PRODUCTNAME", reader, "UNITSINSTOCK");
				dataChart.Series[0].ChartType = SeriesChartType.StackedBar;
			}
		}
	}
}