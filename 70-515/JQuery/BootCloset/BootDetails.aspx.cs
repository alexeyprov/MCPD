using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BootCloset_BootDetails : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Random rng = new Random();
		string[] colors = Enum.GetNames(typeof(KnownColor));

		//lblName.Text = Request.Params["bootStyle"];
		lblSKU.Text = Request.Params["bootStyle"];
		lblColor.Text = colors[rng.Next(colors.Length)];
		lblHeight.Text = String.Format("{0}\"", lblColor.Text.Length);
		lblPrice.Text = (((Int32.Parse(lblSKU.Text) + 5) % 29) + 1.0).ToString("C");
    }
}