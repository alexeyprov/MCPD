using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LinkTableControl : UserControl
{
	private const string ITEMS_PROPERTY = "LinkTableControl_Items";
	private const string LINK_COMMAND = "LinkClick";

	public event EventHandler<LinkItemEventArgs> Click;

	public string Caption
	{
		get
		{
			return lblCaption.Text;
		}
		set
		{
			lblCaption.Text = value;
		}
	}

	public LinkTableItem[] Items
	{
		get
		{
			return ViewState[ITEMS_PROPERTY] as LinkTableItem[];
		}
		set
		{
			ViewState[ITEMS_PROPERTY] = value;
			grdLinks.DataSource = value;
			grdLinks.DataBind();
		}
	}
	
    protected void Page_Load(object sender, EventArgs e)
    {

    }

	protected void grdLinks_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		string url = e.CommandArgument as string;
		if (LINK_COMMAND == e.CommandName && !String.IsNullOrEmpty(url))
		{
			LinkTableItem selectedItem = Array.Find(Items, item => (url == item.Url));
			if (selectedItem != null && Click != null)
			{
				LinkItemEventArgs args = new LinkItemEventArgs(selectedItem);
				Click(this, args);

				if (!args.Cancel)
				{
					Response.Redirect(url);
				}
			}
		}
	}
}