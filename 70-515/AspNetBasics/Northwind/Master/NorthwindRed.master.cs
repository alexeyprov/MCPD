﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Northwind.UI
{
	public partial class RedMasterPage : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			((DefaultRedMasterPage)Master).SiteMapPathVisible = false;
		}
	}
}
