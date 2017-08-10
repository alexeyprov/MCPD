using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebControlsDemo_NormalWizard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	protected void wizSurvey_FinishButtonClick(object sender, WizardNavigationEventArgs e)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("<b>You chose: <br />");
		sb.Append("Programming Language: ");
		sb.Append(lstLanguage.Text);
		sb.Append("<br />Total Employees: ");
		sb.Append(txtEmployeeCount.Text);
		sb.Append("<br />Total Locations: ");
		sb.Append(txtLocationCount.Text);
		sb.Append("<br />Licenses Required: ");
		foreach (ListItem item in cblLicenses.Items)
		{
			if (item.Selected)
			{
				sb.Append(item.Text);
				sb.Append(" ");
			}
		}
		sb.Append("</b>");
		lblSummaryInfo.Text = sb.ToString();
	}
}