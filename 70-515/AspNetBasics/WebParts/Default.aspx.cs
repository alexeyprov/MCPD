using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WebParts.UI
{
	public partial class DefaultPage : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MenuItem root = mnuDisplayMode.Items.OfType<MenuItem>().Where(i => null == i.Parent).FirstOrDefault();

				if (root != null)
				{
					foreach (WebPartDisplayMode mode in wpManager.DisplayModes.OfType<WebPartDisplayMode>().Where(m => m.IsEnabled(wpManager)))
					{
						root.ChildItems.Add(new MenuItem(mode.Name));
					}
				}
			}
		}

		protected void mnuDisplayMode_MenuItemClick(object sender, MenuEventArgs e)
		{
			wpManager.DisplayMode = wpManager.DisplayModes[e.Item.Text];
		}

		protected void ucCustomers_Load(object sender, EventArgs e)
		{
			GenericWebPart wrapper = (GenericWebPart) ((Control) sender).Parent;

			wrapper.TitleUrl = "http://www.epam.com";
			wrapper.Description = "Displays all customers in database";
		}

		protected void wpManager_AuthorizeWebPart(object sender, WebPartAuthorizationEventArgs e)
		{
			if (e.Type == typeof(ControlExtensions.UserControlHostPart))
			{
				// ImportCatalogPart.OnUpload uses a sandbox with
				// SecurityPermission(SecurityPermissionFlag.Execution)
				// AspNetHostingPermission(AspNetHostingPermissionLevel.Medium)
				try
				{
					e.IsAuthorized = User.IsInRole(@"BUILTIN\Administrators");
				}
				catch (SecurityException)
				{
				}
			}
		}
}
}