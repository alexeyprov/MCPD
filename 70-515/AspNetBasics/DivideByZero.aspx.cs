using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DivideByZero : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		int i = 2 + 3;
		int j = i - 5;
		i = i / j;
    }
}
