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
	public partial class OrderDetailsPage : Northwind.UI.NorthwindBasePage
	{
		private const string ORDER_INFO_FORMAT = "Orders # {0}";

		protected void Page_Load(object sender, EventArgs e)
		{
			int orderId = 0;

			if (Int32.TryParse(Request.Params[ConstantsHelper.ORDER_ID_PARAMETER], out orderId))
			{
				lblOrderInfo.Text = String.Format(ORDER_INFO_FORMAT, orderId);

				string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;

				using (NorthwindDataContext dataContext = new NorthwindDataContext(connString))
				{
					var lines = from line in dataContext.OrderLines
								where line.OrderID == orderId
								select line;

					grdLines.DataSource = lines;
					grdLines.DataBind();
				}
			}
		}
	} 
}
