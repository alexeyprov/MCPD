using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CrossPageWizardStart : BasePage
{
	private const string ERROR_PARAMETER = "err";
	private const string PARAMETER_FORMAT = "?{0}={1}";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!String.IsNullOrEmpty(Request[ERROR_PARAMETER]))
		{
			Validate();
		}
	}

	public string FirstName
	{
		get
		{
			return txtFirstName.Text;
		}
	}

	public string LastName
	{
		get
		{
			return txtLastName.Text;
		}
	}

	public static string GetRedirectUrl(bool isError)
	{
		string url = "CrossPageWizardStart.aspx";
		if (isError)
		{
			url += String.Format(PARAMETER_FORMAT, ERROR_PARAMETER, 1);
		}

		return url;
	}
	protected void btnNext_Click(object sender, EventArgs e)
	{
		if (IsValid)
		{
			Server.Transfer("CrossPageWizardEnd.aspx");
		}
	}
}
