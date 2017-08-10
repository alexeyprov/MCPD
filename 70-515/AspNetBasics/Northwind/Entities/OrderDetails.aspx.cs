using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Entities;

namespace Northwind.UI.Entities
{
	public partial class OrderDetailsPage : Northwind.UI.NorthwindBasePage
	{
		private const string ORDER_INFO_FORMAT = "Orders # {0}";

		protected void Page_Load(object sender, EventArgs e)
		{
			int orderId = 0;

			if (Int32.TryParse(Request.Params[ConstantsHelper.ORDER_ID_PARAMETER], out orderId))
			{
				lblOrderInfo.Text = String.Format(ORDER_INFO_FORMAT, orderId);

				string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;

				using (NorthwindObjectContext context = new NorthwindObjectContext(connString))
				{
					grdLines.DataSource = context.GetOrderLines(orderId);
					grdLines.DataBind();
				}
			}
		}
	} 
}
