using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

using Northwind;
using Northwind.Data.ClassicAdo;
using Northwind.Data.Linq;
using Northwind.UI;

namespace Northwind.UI.Linq
{
	public partial class TerritoriesByRegionsPage :
		NorthwindBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// TODO: revise
		// For now it doesn't make sense to register each and every territory
		// EnableEventValidation = false was chosen instead

		//protected override void Render(HtmlTextWriter output)
		//{
		//    if (!IsPostBack)
		//    {
		//        TerritoryData dac = new TerritoryData(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);

		//        dac.GetAllTerritories().ForEach(t =>
		//            ClientScript.RegisterForEventValidation(cmbTerritories.UniqueID, t.TerritoryID));
		//    }

		//    base.Render(output);
		//}

		protected void btnOK_OnClick(object sender, EventArgs e)
		{
			lblOutput.Text = String.Format("You selected territory {0} in region {1}",
				/*cmbTerritories.SelectedValue*/ Request.Form[cmbTerritories.UniqueID], cmbRegions.SelectedItem.Text);
		}

		protected void dsRegions_ContextCreating(object sender, LinqDataSourceContextEventArgs e)
		{
			e.ObjectInstance = new NorthwindDataContext(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);
		}
	}
}