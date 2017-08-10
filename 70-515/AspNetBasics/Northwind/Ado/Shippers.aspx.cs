using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Northwind.UI.Ado
{
	public partial class ShippersPage : Northwind.UI.NorthwindBasePage
	{
		protected void dlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
		{
			lblOutput.Text = String.Format("You selected item with ID={0}", dlSuppliers.SelectedValue);
		}
	} 
}