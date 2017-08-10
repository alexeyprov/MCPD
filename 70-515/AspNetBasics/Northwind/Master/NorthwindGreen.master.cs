using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Northwind.UI
{
	public partial class GreenMasterPage : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			((DefaultGreenMasterPage)Master).SiteMapPathVisible = false;
		}
	}
	
}