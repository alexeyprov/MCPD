using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WebConnStrings
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ConnectionStringSettingsCollection connStrs = WebConfigurationManager.ConnectionStrings;
			if (connStrs != null)
			{
				StringBuilder sb = new StringBuilder();
				int maxLen = txtOutput.Cols;
				foreach (ConnectionStringSettings connStr in connStrs)
				{
					string s = String.Format("Name: {0}, Value: {1}, Provider: {2}\n",
							connStr.Name, connStr.ConnectionString, connStr.ProviderName);
					sb.Append(s);
					if (s.Length > maxLen)
					{
						maxLen = s.Length;
					}
				}
				txtOutput.Cols = maxLen;
				txtOutput.Rows = connStrs.Count;
				txtOutput.Value = sb.ToString();
			}
		}
	}
}
