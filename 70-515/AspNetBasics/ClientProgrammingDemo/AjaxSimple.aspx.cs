using System;
using System.Web.UI.HtmlControls;

namespace AspNetBasics.ClientProgrammingDemo.UI
{
	public partial class AjaxSimplePage : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack && !IsCallback)
			{
				HtmlGenericControl body = Master.FindControl("body") as HtmlGenericControl;
				if (body != null)
				{
					body.Attributes.Add("onload", "CreateAjaxRequest();");
				}
			}
		}
	}
}