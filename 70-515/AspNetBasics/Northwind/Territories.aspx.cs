using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Northwind_Territories : System.Web.UI.Page
{
	#region Private Constants

	private const string TERRITORY_ID_FIELD = "@TerritoryID";
	private const string TERRITORY_DESC_FIELD = "@TerritoryDescription";
	private const string TERRITORY_ID_PARAM = "@PVI_TERRITORY_ID";
	private const string TERRITORY_DESC_PARAM = "@PVI_TERRITORY_DESC";

	private const string NO_REGION = "-1";
	private const string ALL_REGIONS = "-2";

	#endregion

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			//Do manual binding for regions combo
			cmbRegions.DataSource = srcRegions.Select(DataSourceSelectArguments.Empty);
			cmbRegions.DataBind();

			cmbRegions.Items.Insert(0, new ListItem("(Choose a region)", NO_REGION));
			cmbRegions.Items.Insert(1, new ListItem("(All regions)", ALL_REGIONS));
		}

		pnlData.Visible = true;
		lblError.Visible = false;
	}

	// Provide custom parameter binding for srcRegion
	protected void srcRegions_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
	{
		e.Command.Parameters[0].Value = 0; // skip nothing
	}
	protected void dataSource_SqlCommandDone(object sender, SqlDataSourceStatusEventArgs e)
	{
		if (e.Exception != null)
		{
			Debug.WriteLine(e.Exception);			
			pnlData.Visible = false;
			lblError.Visible = true;
			e.ExceptionHandled = true;
		}
	}
	protected void srcTerritories_InsertingUpdating(object sender, SqlDataSourceCommandEventArgs e)
	{
		ReplaceCommandParameter(e.Command.Parameters, TERRITORY_ID_FIELD, TERRITORY_ID_PARAM);
		ReplaceCommandParameter(e.Command.Parameters, TERRITORY_DESC_FIELD, TERRITORY_DESC_PARAM);
	}

	private static void ReplaceCommandParameter(DbParameterCollection pars, string oldPar, string newPar)
	{
		DbParameter p = pars[oldPar];
		pars[newPar].Value = p.Value;
		pars.Remove(p);
	}

	protected void srcTerritories_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
	{
		switch (e.Command.Parameters[0].Value.ToString())
		{
			case NO_REGION:
				// do nothing
				e.Cancel = true;
				break;
			case ALL_REGIONS:
				e.Command.Parameters[0].Value = DBNull.Value;
				break;
			default:
				break;
		}
	}

	protected void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.pnlTerritories.Visible = (cmbRegions.SelectedValue != NO_REGION.ToString());

	}
}
