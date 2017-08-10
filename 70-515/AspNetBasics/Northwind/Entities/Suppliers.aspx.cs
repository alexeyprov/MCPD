using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.ClassicAdo;

namespace Northwind.UI.Entities
{
	public partial class SuppliersPage : Northwind.UI.NorthwindBasePage
	{
		protected void txtAutoSearch_TextChanged(object sender, EventArgs e)
		{
			lstSuppliers.DataBind();
		}
	} 
}
