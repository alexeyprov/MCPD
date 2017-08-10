using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WebParts
{
	/// <summary>
	/// Summary description for CustomerIdEditor
	/// </summary>
	public class CustomerIdEditor : EditorPart
	{
		protected DropDownList ddlCustomers;

		public CustomerIdEditor()
		{
			this.Init += new EventHandler(CustomerIdEditor_Init);
		}

		private void CustomerIdEditor_Init(object sender, EventArgs e)
		{
			EnsureChildControls();

			if (!base.DesignMode)
			{
				using (CustomersTableAdapters.CustomerTableAdapter adapter = new CustomersTableAdapters.CustomerTableAdapter())
				{
					ddlCustomers.DataSource = adapter.GetData();
					ddlCustomers.DataBind();
				}

				ddlCustomers.Items.Insert(0, String.Empty);
			}
		}

		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			ddlCustomers = new DropDownList();
			ddlCustomers.DataTextField = "CompanyName";
			ddlCustomers.DataValueField = "CustomerID";

			base.Controls.Add(ddlCustomers);
		}

		public override void SyncChanges()
		{
			EnsureChildControls();

			CustomerNotesPart customerPart = base.WebPartToEdit as CustomerNotesPart;

			if (customerPart != null)
			{
				ddlCustomers.SelectedValue = customerPart.CustomerID.PadRight(5);
			}
		}

		public override bool ApplyChanges()
		{
			EnsureChildControls();

			string selectedValue = ddlCustomers.SelectedValue;

			if (!String.IsNullOrEmpty(selectedValue))
			{
				CustomerNotesPart customerPart = base.WebPartToEdit as CustomerNotesPart;

				if (customerPart != null)
				{
					customerPart.CustomerID = ddlCustomers.SelectedValue.Trim();
					return true;
				}
			}

			return false;
		}
	} 
}