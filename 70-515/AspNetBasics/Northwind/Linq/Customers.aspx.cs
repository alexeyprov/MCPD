using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Linq;

namespace Northwind.UI.Linq
{
	public partial class CustomersPage : Northwind.UI.NorthwindBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsCallback)
			{
				Debug.WriteLine("Callback in Customers.aspx");
			}
			else
			{
				pnlData.Visible = true;
				lblError.Visible = false;
			}
		}

		protected void grdCustomers_SelectedIndexChanged(object sender, EventArgs e)
		{
			Response.Redirect(String.Format("CustomerOrders.aspx?{0}={1}",
				ConstantsHelper.CUSTOMER_ID_PARAMETER,
				Server.UrlEncode(grdCustomers.SelectedDataKey.Value.ToString())));
		}

		protected void srcCustomers_CommandDone(object sender, LinqDataSourceStatusEventArgs e)
		{
			if (e.Exception != null)
			{
				Debug.WriteLine(e.Exception);
				pnlData.Visible = false;
				lblError.Visible = true;
				e.ExceptionHandled = true;
			}
		}

		protected void srcCustomers_ContextCreating(object sender, LinqDataSourceContextEventArgs e)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
			e.ObjectInstance = new NorthwindDataContext(connectionString);
		}
		protected void grdCustomers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
		{
			if (e.Exception != null)
			{
				Debug.WriteLine(e.Exception);
				pnlData.Visible = false;
				lblError.Visible = true;
				e.ExceptionHandled = true;
			}
		}

		protected void dvwNewCustomer_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
		{
			if (e.Exception != null)
			{
				Debug.WriteLine(e.Exception);
				pnlData.Visible = false;
				lblError.Visible = true;
				e.ExceptionHandled = true;
			}
		}
	} 
}
