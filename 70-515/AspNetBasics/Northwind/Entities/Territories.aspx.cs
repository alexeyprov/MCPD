using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind.Data.Entities;

namespace Northwind.UI.Entities
{
	public partial class TerritoriesPage : Northwind.UI.NorthwindBasePage
	{
		#region Private Constants

		private const string REGION_ID_PARAMETER = "RegionID";

		private const string NO_REGION = "-1";
		private const string ALL_REGIONS = "-2";

		#endregion

		#region Event Handlers

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string connectionString = WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;

				using (NorthwindObjectContext context = new NorthwindObjectContext(connectionString))
				{
					//Do manual binding for regions combo
					cmbRegions.DataSource = context.Regions;
					cmbRegions.DataBind();
				}

				cmbRegions.Items.Insert(0, new ListItem("(Choose a region)", NO_REGION));
				cmbRegions.Items.Insert(1, new ListItem("(All regions)", ALL_REGIONS));
			}

			pnlData.Visible = true;
			lblError.Visible = false;
		}

		// Provide custom parameter binding for srcRegion

		protected void srcTerritories_ReadCommandDone(object sender, EntityDataSourceSelectedEventArgs e)
		{
			e.ExceptionHandled = ProcessException(e.Exception);
		}

		protected void srcTerritories_WriteCommandDone(object sender, EntityDataSourceChangedEventArgs e)
		{
			e.ExceptionHandled = ProcessException(e.Exception);
		}

		protected void srcTerritories_Selecting(object sender,  EntityDataSourceSelectingEventArgs e)
		{
			switch (e.DataSource.WhereParameters[REGION_ID_PARAMETER].ToString())
			{
				case NO_REGION:
					// do nothing
					e.Cancel = true;
					break;
				case ALL_REGIONS:
					e.DataSource.WhereParameters.Clear();
					break;
				default:
					break;
			}
		}

		protected void cmbRegions_SelectedIndexChanged(object sender, EventArgs e)
		{
			pnlTerritories.Visible = (cmbRegions.SelectedValue != NO_REGION);
		}

		protected void srcTerritories_Inserting(object sender, EntityDataSourceChangingEventArgs e)
		{
			if (ALL_REGIONS == cmbRegions.SelectedValue || NO_REGION == cmbRegions.SelectedValue)
			{
				e.Cancel = true;
				return;
			}

			int regionId = Int32.Parse(cmbRegions.SelectedValue);

			NorthwindObjectContext context = e.Context as NorthwindObjectContext;

			((Northwind.Data.Entities.Territory)e.Entity).Region = 
				(from r in context.Regions
				where r.RegionID == regionId
				select r).Single();

		}

		#endregion

		#region Implementation

		private bool ProcessException(Exception exception)
		{
			if (exception != null)
			{
				Debug.WriteLine(exception);
				pnlData.Visible = false;
				lblError.Visible = true;
				return true;
			}
			return false;
		}

		#endregion
	} 
}
