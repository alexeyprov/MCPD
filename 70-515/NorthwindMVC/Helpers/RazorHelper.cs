using System.Web.Mvc;
using System.Web.WebPages;

namespace NorthwindMVC.Helpers
{
	public static class RazorHelper
	{
		public static HtmlHelper Html
		{
			get
			{
				return ((WebViewPage)WebPageContext.Current.Page).Html;
			}
		}

		public static UrlHelper Url
		{
			get
			{
				return ((WebViewPage)WebPageContext.Current.Page).Url;
			}
		}

		public static AjaxHelper Ajax
		{
			get
			{
				return ((WebViewPage)WebPageContext.Current.Page).Ajax;
			}
		}
	}
}