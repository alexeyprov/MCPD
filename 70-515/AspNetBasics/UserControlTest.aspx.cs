using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControlTestPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			lnkTable.Items = new LinkTableItem[] {
				new LinkTableItem("EPAM", "http://www.epam.com"),
				new LinkTableItem("OCM", "http://www.onecallmedical.com"),
				new LinkTableItem("M$", "http://www.microsoft.com")
			};
		}

		// dynamic loading sample
		AddressControl ctl = (AddressControl)LoadControl("~/UserControls/AddressControl.ascx");
		phAddress.Controls.Add(ctl);
    }

	protected void lnkTable_Click(object sender, LinkItemEventArgs e)
	{
		if ("OCM" == e.SelectedItem.Text)
		{
			e.Cancel = true;
		}
	}
}