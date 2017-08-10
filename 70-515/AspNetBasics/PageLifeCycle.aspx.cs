using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageLifeCycle : BasePage 
{
	//access level private - page event is hooked by delegate
	//when partial class is merged
	private void Page_Init(object sender, EventArgs e)
	{
		// Since the label has view state disabled
		// page resubmits will not accumulate text here
		lblInfo.Text += "Page_Init event<br/>";
	}

    private void Page_Load(object sender, EventArgs e)
    {
		lblInfo.Text += "Page_Load event<br/>";
		if (IsPostBack)
		{
			lblInfo.Text += "<b>Page was resubmitted</b><br/>";
		}
    }

	// access level - protected. ASP.NET-generated derived class
	// should have access to event handlers
	protected void btnAction_Click(object sender, EventArgs e)
	{
		Trace.Write("btnAction_Click", "Before updating the label");
		lblInfo.Text += "btnAction_Click event<br/>";
		Trace.Write("btnAction_Click", "After updating the label");
	}

	private void Page_PreRender(object sender, EventArgs e)
	{
		lblInfo.Text += "Page_PreRender event<br/>";
	}

	private void Page_Unload(object sender, EventArgs e)
	{
		// This text never appears because the HTML is already
		// rendered for the page at this point.
		if (lblInfo != null)
		{
			lblInfo.Text += "Page_Unload event<br/>";
		}
	}

}
