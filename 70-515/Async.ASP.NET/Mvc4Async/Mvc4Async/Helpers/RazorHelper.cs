using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Mvc4Async.Helpers
{
	public static class RazorHelper
	{
		public static HtmlHelper Html
		{
			get
			{
				return ((WebViewPage)(WebPageContext.Current.Page)).Html; 
			}
		}

		public static UrlHelper Url
		{
			get
			{
				return ((WebViewPage)(WebPageContext.Current.Page)).Url;
			}
		}

		public static AjaxHelper Ajax
		{
			get
			{
				return ((WebViewPage)(WebPageContext.Current.Page)).Ajax;
			}
		}

		public static HelperResult RenderSection(
			this WebViewPage page,
			string sectionName,
			Func<string, HelperResult> @default)
		{
			return page.IsSectionDefined(sectionName) ?
				page.RenderSection(sectionName) :
				@default(null);
		}
	}
}