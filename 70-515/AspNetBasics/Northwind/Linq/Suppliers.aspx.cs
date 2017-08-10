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
	public partial class SuppliersPage : Northwind.UI.NorthwindBasePage
	{
		protected void srcSuppliers_ContextCreating(object sender, LinqDataSourceContextEventArgs e)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
			e.ObjectInstance = new NorthwindDataContext(connectionString);
		}
	} 
}
