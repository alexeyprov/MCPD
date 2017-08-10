using System;
using System.Web;
using System.Web.UI;

namespace Northwind.UI
{
	/// <summary>
	/// Base page for Northwind samples
	/// </summary>
	public class NorthwindBasePage : BasePage
	{
		protected override string PostProcessMasterPagePath(string masterPagePath)
		{
			return masterPagePath.Replace("Master", "Northwind/Master").Replace("Default", "Northwind");
		}
	}
}
