using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdventureWorks.Account
{
	public partial class PasswordRecovery : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void pwdRecovery_SendingMail(object sender, MailMessageEventArgs e)
		{
			Label lblDestinationEmail = pwdRecovery.SuccessTemplateContainer.FindControl("DestinationEmail") as Label;
			if (lblDestinationEmail != null)
			{
				lblDestinationEmail.Text = e.Message.To[0].Address;
			}
		}
	}
}