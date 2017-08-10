using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_SampleCacheControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		lblDateTime.Text = DateTime.Now.ToString();
    }

	public Color ForeColor
	{
		get
		{
			return lblDateTime.ForeColor;
		}
		set
		{
			lblDateTime.ForeColor = value;
		}
	}
}