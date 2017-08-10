using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebControlsDemo_ValidationGroups : BasePage
{
	#region Event Handlers

	protected void cmdValidateAll_Click(object sender, EventArgs e)
	{
		StringBuilder sb = new StringBuilder("Initial Page.IsValid=");
		sb.Append(IsValid);
		sb.AppendLine("<br />");

		Page.Validate("Existing User");
		sb.AppendFormat("Existing User group validated. Page.IsValid={0}<br/>", IsValid);
		sb.AppendLine();

		Page.Validate("New User");
		sb.AppendFormat("New User group validated. Page.IsValid={0}", IsValid);

		lblValidationResult.Text = sb.ToString();
	}
 
	#endregion
}
