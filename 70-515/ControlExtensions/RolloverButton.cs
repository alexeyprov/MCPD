using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ControlExtensions.Helpers;

[assembly:WebResource("ControlExtensions.Scripts.RolloverButton.js", "text/javascript")]

namespace ControlExtensions
{
	[ToolboxData("<{0}:RolloverButton runat=server></{0}:RolloverButton>")]
	public class RolloverButton : WebControl, IPostBackEventHandler
	{
		private const string DEFAULT_URL_PROPERTY = "DefaultUrl";
		private const string HIGHLIGHTED_URL_PROPERTY = "HighlightedUrl";

		public event EventHandler Click;

		public RolloverButton() :
			base(HtmlTextWriterTag.Img)
		{
		}

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string DefaultImageUrl
		{
			get
			{
				string s = (string) ViewState[DEFAULT_URL_PROPERTY];
				return s ?? String.Empty;
			}

			set
			{
				ViewState[DEFAULT_URL_PROPERTY] = value;
			}
		}

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string HighlightedImageUrl
		{
			get
			{
				string s = (string) ViewState[HIGHLIGHTED_URL_PROPERTY];
				return s ?? String.Empty;
			}

			set
			{
				ViewState[HIGHLIGHTED_URL_PROPERTY] = value;
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (IsClientScriptEnabled)
			{
				Page.ClientScript.RegisterClientScriptResource(typeof(RolloverButton), 
					ConstantsHelper.ScriptResources.ROLLOVER_BUTTON);

				if (!Page.ClientScript.IsStartupScriptRegistered(StartupScriptKey))
				{
					Page.ClientScript.RegisterStartupScript(typeof(RolloverButton), StartupScriptKey,
						BuildStartupScript());
				}

			}
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);

			writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveClientUrl(DefaultImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Onclick,
				Page.ClientScript.GetPostBackEventReference(this, String.Empty), false);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);

			if (IsClientScriptEnabled)
			{
				writer.AddAttribute(ConstantsHelper.HtmlAttributes.ON_MOUSE_OVER,
					FormatImageSwappingScript(HighlightedImageUrl), false);
				writer.AddAttribute(ConstantsHelper.HtmlAttributes.ON_MOUSE_OUT,
					FormatImageSwappingScript(DefaultImageUrl), false);
			}
		}

		#region IPostBackEventHandler Members

		public void RaisePostBackEvent(string eventArgument)
		{
			OnClick(EventArgs.Empty);
		}

		#endregion

		protected virtual void OnClick(EventArgs e)
		{
			if (Click != null)
			{
				Click(this, e);
			}
		}

		protected bool IsClientScriptEnabled
		{
			get
			{
				if (Page.Request != null)
				{
					HttpBrowserCapabilities browser = Page.Request.Browser;

					if (browser != null && 
						browser.EcmaScriptVersion.Major >= 1 && browser.W3CDomVersion.Major >= 1)
					{
						return true;
					}
				}

				return false;
			}
		}

		private string StartupScriptKey
		{
			get
			{
				return "preload" + ClientID;
			}
		}

		private string FormatImageSwappingScript(string url)
		{
 			return String.Format("SwapImage('{0}', '{1}');",
				ClientID, 
				ResolveClientUrl(url));
		}

		private string BuildStartupScript()
		{
			ScriptBuilder scriptBuilder = new ScriptBuilder();

			scriptBuilder.AppendFormat("var preload{0} = new Image();\n", ClientID);
			scriptBuilder.AppendFormat("preload{0}.src = \"{1}\";\n", ClientID, ResolveClientUrl(HighlightedImageUrl));

			return scriptBuilder.ToString();
		}
	}

}
