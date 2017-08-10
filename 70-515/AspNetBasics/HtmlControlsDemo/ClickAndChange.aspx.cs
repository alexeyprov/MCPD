using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class HtmlControlsDemo_ClickAndChange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			lstOptions.Items.Add("Option 3");
			lstOptions.Items.Add("Option 4");
			lstOptions.Items.Add("Option 5");
		}
    }

	protected void ctrl_ServerChange(object sender, EventArgs e)
	{
		Response.Write(String.Format("<li>ServerChange detected for {0}</li>",
			((Control) sender).ID));
	}

	protected void lstOptions_ServerChange(object sender, EventArgs e)
	{
		Response.Write("<li>ServerChange detected for lstOptions</li>");
		Response.Write("Selected items:<br/>");
		foreach (ListItem li in lstOptions.Items)
		{
			if (li.Selected)
			{
				Response.Write(String.Format("{1}{1}-{1}{0}<br/ >", li.Text, "&nbsp;"));
			}
		}
	}

	protected void cmdSubmit_ServerClick(object sender, EventArgs e)
	{
		Response.Write("<li>ServerClick detected for cmdSubmit</li>");
	}
}
