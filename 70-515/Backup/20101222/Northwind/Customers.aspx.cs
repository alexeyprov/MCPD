using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.ClassicAdo;

public partial class Northwind_Customers : System.Web.UI.Page
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
	protected void dataSource_SqlCommandDone(object sender, ObjectDataSourceStatusEventArgs e)
	{
		if (e.Exception != null)
		{
			Debug.WriteLine(e.Exception);
			pnlData.Visible = false;
			lblError.Visible = true;
			e.ExceptionHandled = true;
		}
	}

	protected void srcCustomers_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
	{
		string connectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
		e.ObjectInstance = new CustomerData(connectionString);
	}

	protected void grdCustomers_SelectedIndexChanged(object sender, EventArgs e)
	{
		Response.Redirect(String.Format("CustomerOrders.aspx?{0}={1}",
			ConstantsHelper.CUSTOMER_ID_PARAMETER,
			Server.UrlEncode(grdCustomers.SelectedDataKey.Value.ToString())));
	}
}
