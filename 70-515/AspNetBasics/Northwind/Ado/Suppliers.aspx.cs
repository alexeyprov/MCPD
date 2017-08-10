using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.ClassicAdo;

namespace Northwind.UI.Ado
{
	public partial class SuppliersPage : Northwind.UI.NorthwindBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void srcSuppliers_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
		{
			e.ObjectInstance = new SupplierData(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);
		}
	} 
}
