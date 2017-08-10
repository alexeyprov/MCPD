using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Linq;

namespace Northwind.UI.Linq
{
	public partial class TerritoriesPage : Northwind.UI.NorthwindBasePage
	{
		#region Private Constants

		private const string REGION_ID_PARAMETER = "RegionID";

		private const string NO_REGION = "-1";
		private const string ALL_REGIONS = "-2";

		#endregion

		#region Private Fields

		private NorthwindDataContext _dataContext;

		#endregion

		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
			string connectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
			_dataContext = new NorthwindDataContext(connectionString);

			if (!IsPostBack)
			{
				//Do manual binding for regions combo
				cmbRegions.DataSource = _dataContext.Regions;
				cmbRegions.DataBind();

				cmbRegions.Items.Insert(0, new ListItem("(Choose a region)", NO_REGION));
				cmbRegions.Items.Insert(1, new ListItem("(All regions)", ALL_REGIONS));
			}

			pnlData.Visible = true;
			lblError.Visible = false;
		}

		// Provide custom parameter binding for srcRegion

		protected void dataSource_SqlCommandDone(object sender, LinqDataSourceStatusEventArgs e)
		{
			if (e.Exception != null)
			{
				Debug.WriteLine(e.Exception);
				pnlData.Visible = false;
				lblError.Visible = true;
				e.ExceptionHandled = true;
			}
		}

		protected void srcTerritories_Selecting(object sender,  LinqDataSourceSelectEventArgs e)
		{
			switch (e.WhereParameters[REGION_ID_PARAMETER].ToString())
			{
				case NO_REGION:
					// do nothing
					e.Cancel = true;
					break;
				case ALL_REGIONS:
					e.WhereParameters.Clear();
					break;
				default:
					break;
			}
		}

		protected void srcTerritories_ContextCreating(object sender, LinqDataSourceContextEventArgs e)
		{
			e.ObjectInstance = _dataContext;
		}

		protected void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
		{
			pnlTerritories.Visible = (cmbRegions.SelectedValue != NO_REGION.ToString());
		}

		#endregion
	} 
}
