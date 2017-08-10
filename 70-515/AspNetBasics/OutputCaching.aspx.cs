using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OutputCaching : BasePage
{
	static Color[] _colors = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.LightBlue, Color.Navy };

	protected void Page_Load(object sender, EventArgs e)
	{
		lblCachedTime.Text = GetDate(HttpContext.Current);
		//Response.WriteSubstitution(GetDate);

		//since control is cached, it may be missing
		if (ucSampleCacheControl != null)
		{
			ucSampleCacheControl.ForeColor = _colors[(DateTime.Now.Second / 10) % 6];
		}
	}

	protected static string GetDate(HttpContext context)
	{
		return DateTime.Now.ToString();
	}
}
