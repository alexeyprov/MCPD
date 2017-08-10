using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ControlExtensions.Helpers;

namespace ControlExtensions
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:PopUp runat=server></{0}:PopUp>")]
	public class PopUp : Control
	{
		private const string URL_PROPERTY = "Url";
		private const string POP_UNDER_PROPERTY = "PopUnder";
		private const string RESIZABLE_PROPERTY = "Resizable";
		private const string SCROLLBARS_PROPERTY = "Scrollbars";
		private const string WIDTH_PROPERTY = "Width";
		private const string HEIGHT_PROPERTY = "Height";

		private const int DEFAULT_SIZE = 100;

		[Category("Appearance")]
		[DefaultValue("true")]
		[Localizable(true)]
		public bool PopUnder
		{
			get
			{
				bool? value = (bool?) ViewState[POP_UNDER_PROPERTY];
				return value.GetValueOrDefault(true);
			}
			set
			{
				ViewState[POP_UNDER_PROPERTY] = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue("false")]
		[Localizable(true)]
		public bool Resizable
		{
			get
			{
				bool? value = (bool?) ViewState[RESIZABLE_PROPERTY];
				return value.GetValueOrDefault();
			}
			set
			{
				ViewState[RESIZABLE_PROPERTY] = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue("false")]
		[Localizable(true)]
		public bool ScrollBars
		{
			get
			{
				bool? value = (bool?) ViewState[SCROLLBARS_PROPERTY];
				return value.GetValueOrDefault();
			}
			set
			{
				ViewState[SCROLLBARS_PROPERTY] = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue("100")]
		[Localizable(true)]
		public int WindowWidth
		{
			get
			{
				int? value = (int?) ViewState[WIDTH_PROPERTY];
				return value.GetValueOrDefault(DEFAULT_SIZE);
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("WindowWidth should be positive");
				}
				ViewState[WIDTH_PROPERTY] = value;
			}
		}

		[Category("Appearance")]
		[DefaultValue("100")]
		[Localizable(true)]
		public int WindowHeight
		{
			get
			{
				int? value = (int?) ViewState[HEIGHT_PROPERTY];
				return value.GetValueOrDefault(DEFAULT_SIZE);
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException("WindowHeight should be positive");
				}
				ViewState[HEIGHT_PROPERTY] = value;
			}
		}

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("about:blank")]
		[Localizable(true)]
		public string Url
		{
			get
			{
				String s = (String) ViewState[URL_PROPERTY];
				return ((s == null) ? String.Empty : s);
			}
			set
			{
				ViewState[URL_PROPERTY] = value;
			}
		}

		protected override void Render(HtmlTextWriter output)
		{
			if (Page.Request.Browser.EcmaScriptVersion.Major >= 1)
			{
				ScriptBuilder scriptBuilder = new ScriptBuilder();
				
				scriptBuilder.AppendFormat("window.open('{0}',\n", Url);
				scriptBuilder.Indent();
				scriptBuilder.AppendFormat("'{0}',\n", ID);
				scriptBuilder.AppendFormat("'toolbar=0, resizable={0}, scrollbars={1}, width={2}, height={3}');\n",
					Convert.ToInt16(Resizable),
					Convert.ToInt16(ScrollBars),
					WindowWidth,
					WindowHeight);
				scriptBuilder.Outdent();

				if (PopUnder)
				{
					scriptBuilder.AppendLine("window.focus();");
				}

				output.Write(scriptBuilder.ToString());
			}
			else
			{
				output.Write("<!-- JavaScript is required -->");
			}
		}
	}
}
