using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind;
using Northwind.Data.ClassicAdo;

namespace Northwind.UI.Ado
{
	public partial class OrderDetailsPage : Northwind.UI.NorthwindBasePage
	{
		private const string ORDER_INFO_FORMAT = "Orders # {0}";

		protected void Page_Load(object sender, EventArgs e)
		{
			string orderId = Request.Params[ConstantsHelper.ORDER_ID_PARAMETER];
			if (!String.IsNullOrEmpty(orderId))
			{
				lblOrderInfo.Text = String.Format(ORDER_INFO_FORMAT, orderId);

				string connString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
				OrderData dac = new OrderData(connString);

				Order o = dac.GetOrder(orderId);

				if (o != null && o.Lines != null)
				{
					grdLines.DataSource = o.Lines;
					grdLines.DataBind();
				}
			}
		}
	} 
}
