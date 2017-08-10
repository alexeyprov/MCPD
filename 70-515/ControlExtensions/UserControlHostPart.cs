using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ControlExtensions
{
	/// <summary>
	/// Web part to host user controls
	/// </summary>
	public class UserControlHostPart : WebPart
	{
		private Control _innerControl;
		private Label _fallbackLabel;

		public UserControlHostPart()
		{
			base.ExportMode = WebPartExportMode.All;
			base.PreRender += new EventHandler(UserControlHostPart_PreRender);
		}

		[WebBrowsable(true)]
		[WebDisplayName("Server Control Path")]
		[Personalizable(PersonalizationScope.User)]
		public string ControlPath
		{
			get;
			set;
		}

		protected override void CreateChildControls()
		{
			_fallbackLabel = new Label();
			_fallbackLabel.Text = "(select a control)";
			_fallbackLabel.EnableViewState = false;

			LoadInnerControl();
		}

		private void UserControlHostPart_PreRender(object sender, EventArgs e)
		{
			// ControlPath property may be set from an event handler
			// => reload control, if needed
			LoadInnerControl();
		}

		public override void RenderControl(HtmlTextWriter writer)
		{
			if (_innerControl != null)
			{
				_innerControl.RenderControl(writer);
			}
			else
			{
				_fallbackLabel.RenderControl(writer);
			}
		}

		private void LoadInnerControl()
		{
			if (!String.IsNullOrEmpty(this.ControlPath))
			{
				if (_innerControl != null)
				{
					base.Controls.Remove(_innerControl);
					_innerControl.Dispose();
					_innerControl = null;
				}

				try
				{
					_innerControl = Page.LoadControl(this.ControlPath);

					if (_innerControl != null)
					{
						base.Controls.Add(_innerControl);
					}
				}
				catch (Exception ex) //TODO: change exception type
				{
					_fallbackLabel.Text = "Error: " + ex.Message;
				}
			}
		}
	}
}
