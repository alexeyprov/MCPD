using System;

namespace Northwind.UI.Entities
{
	public partial class TerritoriesByRegionsPage : 
		NorthwindBasePage
	{
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
	}
}