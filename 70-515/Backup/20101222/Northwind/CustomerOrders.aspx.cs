using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind;
using Northwind.Data.ClassicAdo;

public partial class Northwind_CustomerOrders : Page
{
	private const string CUSTOMER_ORDERS_FORMAT = "Orders for customer {0}:";

	protected void Page_Load(object sender, EventArgs e)
	{
		string customerId = Request.Params[ConstantsHelper.CUSTOMER_ID_PARAMETER];
		if (!String.IsNullOrEmpty(customerId))
		{
			lblCustomerOrders.Text = String.Format(CUSTOMER_ORDERS_FORMAT, customerId);

			string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
			OrderData dac = new OrderData(connString);

			var orders = from o in dac.GetAllOrders(null)
						 where o.CustomerID == customerId
						 orderby o.OrderedDate
						 select new
						 {
							o.ID,
							o.OrderedDate,
							o.Freight
						 };
			grdOrders.DataSource = orders;
			grdOrders.DataBind();

			grdOrderHistory.DataSource = from o in orders
										 group o by o.OrderedDate.Year into g
										 select new
										 {
											 Year = g.Key,
											 Count = g.Count(),
											 Average = g.Average(item => item.Freight),
											 Maximum = g.Max(item => item.Freight)
										 };
			grdOrderHistory.DataBind();
		}
	}

	protected void grdOrders_SelectedIndexChanged(object sender, EventArgs e)
	{
		Response.Redirect(String.Format("OrderDetails.aspx?{0}={1}",
			ConstantsHelper.ORDER_ID_PARAMETER, 
			grdOrders.SelectedDataKey[0]));
	}
}
