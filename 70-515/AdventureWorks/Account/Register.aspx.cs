using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdventureWorks.Account
{
	public partial class Register : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
		}

		protected void RegisterUser_CreatedUser(object sender, EventArgs e)
		{
			for (int i = 0; i < RegisterUser.WizardSteps.Count; ++i)
			{
				WizardStepBase step = RegisterUser.WizardSteps[i];
				if ("NameStep" == step.ID)
				{
					string firstName = ((TextBox)step.FindControl("FirstName")).Text;
					string lastName = ((TextBox)step.FindControl("LastName")).Text;

					// TODO: store in DB instead
					Debug.WriteLine("Personal info: {1}, {0}", firstName, lastName);
					break;
				}
			}

			FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

			string continueUrl = RegisterUser.ContinueDestinationPageUrl;
			if (String.IsNullOrEmpty(continueUrl))
			{
				continueUrl = "~/";
			}
			Response.Redirect(continueUrl);
		}

	}
}
