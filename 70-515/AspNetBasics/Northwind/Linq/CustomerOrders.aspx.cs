using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Linq;

namespace Northwind.UI.Linq
{
	public partial class CustomerOrdersPage : Northwind.UI.NorthwindBasePage
	{
		private const string CUSTOMER_ORDERS_FORMAT = "Orders for customer {0}:";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string customerId = Request.Params[ConstantsHelper.CUSTOMER_ID_PARAMETER];
				if (!String.IsNullOrEmpty(customerId))
				{
					lblCustomerOrders.Text = String.Format(CUSTOMER_ORDERS_FORMAT, customerId);

					string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;

					using (NorthwindDataContext dataContext = new NorthwindDataContext(connString))
					{
						// set first order's date to today, 
						// just to demonstrate that LINQ to SQL uses a single entity object per primary key
						var matches = from o in dataContext.Orders
									  where o.CustomerID == customerId
									  select o;

						Northwind.Data.Linq.Order firstOrder = matches.FirstOrDefault();

						if (firstOrder != null)
						{
							firstOrder.OrderDate = DateTime.Today;
						}

						// this query should now yield an order with changed date
						var orders = from o in dataContext.Orders
									 where o.CustomerID == customerId
									 orderby o.OrderDate
									 select o;
						grdOrders.DataSource = orders;
						grdOrders.DataBind();

						// some grouping example
						grdOrderHistory.DataSource = from o in orders
													 group o by o.OrderDate.Value.Year into g
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
			}
		}

		protected void grdOrders_SelectedIndexChanged(object sender, EventArgs e)
		{
			Response.Redirect(String.Format("OrderDetails.aspx?{0}={1}",
				ConstantsHelper.ORDER_ID_PARAMETER,
				grdOrders.SelectedDataKey[0]));
		}
	}
	
}