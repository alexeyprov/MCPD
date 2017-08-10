using System;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace WebParts.UI
{
	public partial class CustomersControl :
		UserControl,
		IWebPart
	{
		#region Private Constants

		private const string TITLE_VIEWSTATE_KEY = "Customer Title";
		private const string ICON_VIEWSTATE_KEY = "Customer Icon";

		#endregion

		#region IWebPart Members

		public string CatalogIconImageUrl
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string Subtitle
		{
			get
			{
				return "Customer List";
			}
		}

		public string Title
		{
			get
			{
				return ((string) ViewState[TITLE_VIEWSTATE_KEY]) ?? String.Empty;
			}
			set
			{
				ViewState[TITLE_VIEWSTATE_KEY] = value;
			}
		}

		public string TitleIconImageUrl
		{
			get
			{
				return ((string) ViewState[ICON_VIEWSTATE_KEY]) ?? "~/Images/Customers.gif";
			}
			set
			{
				ViewState[ICON_VIEWSTATE_KEY] = value;
			}
		}

		public string TitleUrl
		{
			get;
			set;
		}

		#endregion
	} 
}