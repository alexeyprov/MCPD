using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultBlueMasterPage : MasterPage
{
	public bool SiteMapPathVisible
	{
		get
		{
			return smpMain.Visible;
		}
		set
		{
			smpMain.Visible = value;
		}
	}

	protected void ddlMasters_SelectedIndexChanged(object sender, EventArgs e)
	{
		Session[ConstantsHelper.SELECTED_MASTER_KEY] = ddlMasters.SelectedValue;
		Profile.SchemeColor = ddlMasters.SelectedItem.Text;
		Response.Redirect(Request.Url.ToString());
	}

	protected void ddlMasters_DataBound(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			ddlMasters.SelectedValue = this.AppRelativeVirtualPath;
		}
	}

	protected void ddlLanguages_SelectedIndexChanged(object sender, EventArgs e)
	{
		Session[ConstantsHelper.SELECTED_LANGUAGE_KEY] = ddlLanguages.SelectedValue;
		Response.Redirect(Request.Url.ToString());
	}

	protected void ddlLanguages_PreRender(object sender, EventArgs e)
	{
		if (ddlLanguages.SelectedValue != Thread.CurrentThread.CurrentUICulture.Name)
		{
			ddlLanguages.SelectedValue = Thread.CurrentThread.CurrentUICulture.Name;
		}
	}
}
