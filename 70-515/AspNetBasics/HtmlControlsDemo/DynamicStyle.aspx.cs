using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HtmlControlsDemo_DynamicStyle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	protected void btnUpdate_Click(object sender, EventArgs e)
	{
		if (IsValid)
		{
			//set style
			txtSample.Style["font-size"] = txtFontWeight.Text.Trim() + "pt";
			txtSample.Style["color"] = cbForeColor.SelectedValue;
			txtSample.Style.Add("background-color", cbBackColor.SelectedValue);

			//set client-side event handler
			txtSample.Attributes["onblur"] = "alert(txtSample.value);";
		}
	}
}
