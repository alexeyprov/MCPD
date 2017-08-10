using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlExtensions
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:NumericUpDown runat=server></{0}:NumericUpDown>")]
	public class NumericUpDown : 
		WebControl,
		IPostBackDataHandler,
		IPostBackEventHandler
	{
		#region Private Constants

		private const string VALUE_KEY = "Value";
		private const string DECREMENT_COMMAND = "dec";
		private const string INCREMENT_COMMAND = "inc";
		
		#endregion

		#region Events

		public event EventHandler<NumericValueChangedEventArgs> ValueChanged;
		public event EventHandler Incremented;
		public event EventHandler Decremented;
		
		#endregion

		#region Public Properties

		[Bindable(true)]
		[DefaultValue("0")]
		[Localizable(true)]
		public int Value
		{
			get
			{
				int? value = (int?)ViewState[VALUE_KEY];
				return value ?? 0;
			}

			set
			{
				ViewState[VALUE_KEY] = value;
			}
		}

		#endregion

		#region IPostBackDataHandler Members

		public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			int newValue = 0;
			Int32.TryParse(postCollection[postDataKey], out newValue);

			if (newValue != Value)
			{
				Value = newValue;
				return true;
			}

			return false;
		}

		public void RaisePostDataChangedEvent()
		{
			OnValueChanged(new NumericValueChangedEventArgs(Value));
		}

		#endregion

		#region IPostBackEventHandler Members

		public void RaisePostBackEvent(string eventArgument)
		{
			switch (eventArgument)
			{
				case INCREMENT_COMMAND:
					Value++;
					OnIncremented(EventArgs.Empty);
					break;
				case DECREMENT_COMMAND:
					Value--;
					OnDecremented(EventArgs.Empty);
					break;
				default:
					break;
			}
		}

		#endregion

		#region Overrides

		protected override void RenderContents(HtmlTextWriter output)
		{
			// "<" link
			RenderLink(output, "&lt;", DECREMENT_COMMAND);

			// text box
			output.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			output.AddAttribute(HtmlTextWriterAttribute.Value, Value.ToString());

			output.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID); //needed for data change tracking

			output.RenderBeginTag(HtmlTextWriterTag.Input);
			output.RenderEndTag();

			// ">" link
			RenderLink(output, "&gt;", INCREMENT_COMMAND);
		}

		#endregion

		#region Implementation

		protected virtual void OnValueChanged(NumericValueChangedEventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}

		protected virtual void OnIncremented(EventArgs e)
		{
			if (Incremented != null)
			{
				Incremented(this, e);
			}
		}

		protected virtual void OnDecremented(EventArgs e)
		{
			if (Decremented != null)
			{
				Decremented(this, e);
			}
		}

		private void RenderLink(HtmlTextWriter output, string caption, string command)
		{
			output.AddAttribute(HtmlTextWriterAttribute.Href,
				"javascript:" + Page.ClientScript.GetPostBackEventReference(this, command));
			output.RenderBeginTag(HtmlTextWriterTag.A);
			output.Write(caption);
			output.RenderEndTag();
		}

		#endregion
	}
}

public class NumericValueChangedEventArgs : EventArgs
{
	internal NumericValueChangedEventArgs(int value)
	{
		this.Value = value;
	}

	public int Value
	{
		get;
		private set;
	}
}

