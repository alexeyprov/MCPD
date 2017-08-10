using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetBasics.ClientProgrammingDemo.UI
{
	public partial class GetImagePage : Page
	{
		private const string ISBN_PARAMETER = "isbn";
		private const string MAIN_URL_PREFIX = "http://www.amazon.com/exec/obidos/ASIN/";
		private const string IMAGE_REGEX = "<img src=\"(http://[^\\.]+\\.images\\-amazon\\.com/images/I/[^\"]+)\"";

		protected void Page_Load(object sender, EventArgs e)
		{
			string isbn = Request.QueryString[ISBN_PARAMETER];
			if (!String.IsNullOrEmpty(isbn))
			{
				Response.Redirect(FindBookCoverUrl(isbn));
			}
		}

		private static string FindBookCoverUrl(string isbn)
		{
			try
			{
				string url = MAIN_URL_PREFIX + isbn.Replace("-", String.Empty).Trim();
				string content = ReadHtmlContent(url);

				Regex regex = new Regex(IMAGE_REGEX, RegexOptions.IgnoreCase);
				Match m = regex.Match(content);

				if (m != null && m != Match.Empty)
				{
					return m.Groups[1].Value;
				}
			}
			catch
			{
			}
			return String.Empty;
		}

		private static string ReadHtmlContent(string url)
		{
			WebRequest request = HttpWebRequest.Create(url);
			WebResponse response = request.GetResponse();
			using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			{
				return reader.ReadToEnd();
			}
		}
	} 
}