using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetBasics.ClientProgrammingDemo.UI
{
	public partial class PageProcessorPage : Page
	{
		private const string PAGE_ARGUMENT = "page";

		protected string PageToLoad
		{
			get;
			set;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack && !IsCallback)
			{
				string requestedPage = Request.Params[PAGE_ARGUMENT];
				if (!String.IsNullOrEmpty(requestedPage))
				{
					PageToLoad = requestedPage;
				}
			}
		}
	} 
}