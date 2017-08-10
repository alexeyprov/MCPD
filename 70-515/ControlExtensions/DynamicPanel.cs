using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ControlExtensions.Helpers;

[assembly:WebResource("ControlExtensions.Scripts.DynamicPanel.js", "text/javascript")]

namespace ControlExtensions
{
	[ToolboxData("<{0}:DynamicPanel runat=server></{0}:DynamicPanel>")]
	public class DynamicPanel : 
		Panel, 
		ICallbackEventHandler, 
		ICallbackContainer
	{
		#region Private Constants

		private const string SEPARATOR = "$";
		private const string REFRESH_CLIENT_SCRIPT = "Refresh";
		
		#endregion

		#region Public Events

		public event EventHandler Refresh; 

		#endregion

		#region ICallbackEventHandler Members

		public string GetCallbackResult()
		{
			// Ensure child controls are created before rendering
			EnsureChildControls();

			// Render
			using (StringWriter writer = new StringWriter())
			{
				using (HtmlTextWriter output = new HtmlTextWriter(writer))
				{
					// Write client ID first
					output.Write(ClientID + SEPARATOR);

					// Render children
					RenderContents(output);
				}

				return writer.ToString();
			}
		}

		public void RaiseCallbackEvent(string eventArgument)
		{
			OnRefresh(EventArgs.Empty);
		}

		#endregion

		#region ICallbackContainer Members

		public string GetCallbackScript(IButtonControl buttonControl, string argument)
		{
			return String.Format("javascript:{0}();", RefreshClientFunctionName);
		}

		#endregion

		#region Overrides

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			Page.ClientScript.RegisterClientScriptResource(typeof(DynamicPanel), ConstantsHelper.ScriptResources.DYNAMIC_PANEL);
			if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(DynamicPanel), REFRESH_CLIENT_SCRIPT))
			{
				Page.ClientScript.RegisterClientScriptBlock(typeof(DynamicPanel), REFRESH_CLIENT_SCRIPT, BuildRefreshClientScript());
			}
		}

		#endregion

		#region Virtual Functions

		protected virtual void OnRefresh(EventArgs e)
		{
			if (Refresh != null)
			{
				Refresh(this, e);
			}
		}

		#endregion

		#region Implementation

		private string BuildRefreshClientScript()
		{
			ScriptBuilder scriptBuilder = new ScriptBuilder();

			scriptBuilder.AppendFormat("function {0}()\n", RefreshClientFunctionName);
			scriptBuilder.AppendLine("{");
			scriptBuilder.Indent();

			scriptBuilder.AppendLine("__theFormPostData = \"\";");
			scriptBuilder.AppendLine("WebForm_InitCallback(); //update form fields");
			scriptBuilder.AppendFormat("{0}; //do callback\n",
				Page.ClientScript.GetCallbackEventReference(this, null, "DynamicPanelCallback", null, true));

			scriptBuilder.Outdent();
			scriptBuilder.AppendLine("}");

			return scriptBuilder.ToString();
		}

		private string RefreshClientFunctionName
		{
			get
			{
				return String.Format("{0}{1}", REFRESH_CLIENT_SCRIPT, ClientID);
			}
		}

		#endregion
	}
}
