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

using ControlExtensions.Helpers;

using Northwind;
using Northwind.Data.ClassicAdo;
using Northwind.UI;

namespace Northwind.UI.Ado
{
	public partial class TerritoriesByRegionsPage : NorthwindBasePage, ICallbackEventHandler
	{
		private const string REGION_CHANGED_CLIENT_HANDLER = "OnRegionSelected";
		private const string CALLBACK_COMPLETED_CLIENT_HANDLER = "OnCallbackCompleted";

		private string _regionId;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsCallback && !IsPostBack)
			{
				cmbRegions.Attributes.Add("onchange", REGION_CHANGED_CLIENT_HANDLER + "()");

				if (!ClientScript.IsClientScriptBlockRegistered(REGION_CHANGED_CLIENT_HANDLER))
				{
					ClientScript.RegisterClientScriptBlock(typeof(TerritoriesByRegionsPage),
						REGION_CHANGED_CLIENT_HANDLER,
						BuildRegionChangedHandler());
				}
			}
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

		#region ICallbackEventHandler Members

		public string GetCallbackResult()
		{
			if (String.IsNullOrEmpty(_regionId))
			{
				return null;
			}

			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Territory>));

			using (MemoryStream stream = new MemoryStream())
			{
				serializer.WriteObject(stream, GetTerritories());

				// rewind for reading
				stream.Seek(0L, SeekOrigin.Begin);

				using (StreamReader reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}

		public void RaiseCallbackEvent(string eventArgument)
		{
			_regionId = eventArgument;
		}

		#endregion

		private List<Territory> GetTerritories()
		{
			TerritoryData dac = new TerritoryData(WebConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString);

			return dac.GetTerritoriesByRegion(_regionId);
		}

		private string BuildRegionChangedHandler()
		{
			ScriptBuilder scriptBuilder = new ScriptBuilder();

			scriptBuilder.AppendFormat("function {0}()\n", REGION_CHANGED_CLIENT_HANDLER);
			scriptBuilder.AppendLine("{");
			scriptBuilder.Indent();

			scriptBuilder.AppendFormat("var cmb = document.getElementById(\'{0}\');\n",
				cmbTerritories.ClientID);
			scriptBuilder.AppendLine("cmb.disabled = true;");

			scriptBuilder.AppendLine(ClientScript.GetCallbackEventReference(this,
						String.Format("document.getElementById(\'{0}\').value", cmbRegions.ClientID),
						CALLBACK_COMPLETED_CLIENT_HANDLER,
						null));

			scriptBuilder.Outdent();
			scriptBuilder.AppendLine("}");

			return scriptBuilder.ToString();
		}
	}
}