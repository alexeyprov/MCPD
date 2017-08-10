using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdventureWorks.Account
{
	public partial class PersonalInfo : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				txtFirstName.Text = (string) HttpContext.Current.Profile.GetPropertyValue("FirstName");
				txtLastName.Text = (string) HttpContext.Current.Profile.GetPropertyValue("LastName");
				ddlDateOfBirth.VisibleDate = ddlDateOfBirth.SelectedDate = (DateTime) HttpContext.Current.Profile.GetPropertyValue("DateOfBirth");
			}
		}

		protected void ddlDateOfBirth_DayRender(object sender, DayRenderEventArgs e)
		{
			if (e.Day.Date >= DateTime.Today)
			{
				e.Day.IsSelectable = false;
			}
		}

		protected void cmdUpdate_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Profile.SetPropertyValue("FirstName", txtFirstName.Text);
			HttpContext.Current.Profile.SetPropertyValue("LastName", txtLastName.Text);
			HttpContext.Current.Profile.SetPropertyValue("DateOfBirth", ddlDateOfBirth.SelectedDate);
		}

		protected void cmdReset_Click(object sender, EventArgs e)
		{
			ProfileManager.DeleteProfile(User.Identity.Name);
		}
	}
}