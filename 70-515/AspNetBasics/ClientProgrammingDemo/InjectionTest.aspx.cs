using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetBasics.ClientProgrammingDemo.UI
{
	public partial class InjectionTestPage : BasePage
	{
		protected void btnSubmit_OnClick(object sender, EventArgs e)
		{
			lblOutput.Text = "Hello, " + Server.HtmlEncode(txtInput.Text);
		}
	} 
}