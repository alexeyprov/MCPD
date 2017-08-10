using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AWDynamicData
{
	public partial class EnableDynamicData : System.Web.UI.Page
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			gvOrderDetails.EnableDynamicData(typeof(AdventureWorks.Data.Linq.SalesOrderDetail));
		}
	}
}