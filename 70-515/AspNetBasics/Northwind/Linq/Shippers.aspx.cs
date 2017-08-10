using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Linq;

namespace Northwind.UI.Linq
{
	public partial class ShippersPage : Northwind.UI.NorthwindBasePage
	{
		protected void dlShippers_SelectedIndexChanged(object sender, EventArgs e)
		{
			lblOutput.Text = String.Format("You selected item with ID={0}", dlShippers.SelectedValue);
		}

		protected void srcShippers_ContextCreating(object sender, LinqDataSourceContextEventArgs e)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
			e.ObjectInstance = new NorthwindDataContext(connectionString);
		}
	} 
}