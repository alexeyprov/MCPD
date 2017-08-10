using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CrossPageWizardEnd : BasePage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		CrossPageWizardStart startPage = PreviousPage as CrossPageWizardStart;
		bool validationError = false;
		
		if (startPage != null)
		{
			if (startPage.IsValid)
			{
				txtSummary.Text = String.Format("First name = {0}{2}Last name = {1}",
					startPage.FirstName,
					startPage.LastName,
					Environment.NewLine);

				lblRedirectMethod.Text = startPage.IsCrossPagePostBack ? "PostBackUrl" : "Server.Transfer";
				return;
			}
			else
			{
				validationError = true;
			}
		}

		Response.Redirect(CrossPageWizardStart.GetRedirectUrl(validationError));
	}
}
