using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlExtensions
{
	[ToolboxData("<{0}:DynamicPanelRefreshLink runat=server></{0}:DynamicPanelRefreshLink>")]
	public class CallbackPanelRefreshLink : LinkButton
	{
		#region Private Constants

		private const string PANEL_ID_PROPERTY = "PanelID"; 

		#endregion

		#region Public Properties

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string PanelID
		{
			get
			{
				string value = (string) ViewState[PANEL_ID_PROPERTY];
				return value ?? String.Empty;
			}

			set
			{
				ViewState[PANEL_ID_PROPERTY] = value;
			}
		}

		#endregion

		#region Overrides

		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(Text);
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			//base.AddAttributesToRender(writer);

			ICallbackContainer target = NamingContainer.FindControl(PanelID) as ICallbackContainer;

			if (target != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, target.GetCallbackScript(this, String.Empty));
			}
		}

		#endregion
	}
}
