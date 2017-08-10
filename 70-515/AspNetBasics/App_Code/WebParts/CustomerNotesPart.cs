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
	/// Summary description for CustomerNotesPart
	/// </summary>
	public class CustomerNotesPart :
		WebPart,
		INotesContract
	{
		private string _customerID;

		private TextBox txtNewNote;
		private Button btnAddNewNote;
		private GridView grdCustomerNotes;

		public CustomerNotesPart()
		{
			this.Init += new EventHandler(CustomerNotesPart_Init);
			this.Load += new EventHandler(CustomerNotesPart_Load);
			this.PreRender += new EventHandler(CustomerNotesPart_PreRender);
		}

		[WebBrowsable(true)]
		[WebDisplayName("Customer ID")]
		[Personalizable(PersonalizationScope.User)]
		public string CustomerID
		{
			get
			{
				return _customerID;
			}
			set
			{
				if (_customerID != value)
				{
					_customerID = value;

					if (!base.DesignMode)
					{
						EnsureChildControls();

						grdCustomerNotes.PageIndex = 0;
						grdCustomerNotes.SelectedIndex = -1;

						BindGrid();
					}
				}
			}
		}

		public override bool AllowClose
		{
			get
			{
				return false;
			}
			set
			{
				// do nothing
			}
		}

		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			txtNewNote = new TextBox();

			btnAddNewNote = new Button();
			btnAddNewNote.Text = "Add note";
			btnAddNewNote.Click += new EventHandler(btnAddNewNote_Click);

			grdCustomerNotes = new GridView();
			grdCustomerNotes.AllowPaging = true;
			grdCustomerNotes.AutoGenerateSelectButton = true;
			grdCustomerNotes.PageSize = 5;
			grdCustomerNotes.PageIndexChanging += new GridViewPageEventHandler(grdCustomerNotes_PageIndexChanging);

			base.Controls.Add(txtNewNote);
			base.Controls.Add(btnAddNewNote);
			base.Controls.Add(grdCustomerNotes);
		}

		public override EditorPartCollection CreateEditorParts()
		{
			return new EditorPartCollection(new EditorPart[]
			{
				new CustomerIdEditor()
				{
					 ID = this.ID + "_EditorPart_1"
				}
			});
		}

		public override object WebBrowsableObject
		{
			get
			{
				return this;
			}
		}

		[ConnectionProvider("Notes Content", "NotesProviderID")]
		public INotesContract GetConnectionPoint()
		{
			return this;
		}

		private void grdCustomerNotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			grdCustomerNotes.PageIndex = e.NewPageIndex;
		}

		private void btnAddNewNote_Click(object sender, EventArgs e)
		{
			using (CustomersTableAdapters.CustomerNotesTableAdapter adapter = new CustomersTableAdapters.CustomerNotesTableAdapter())
			{
				adapter.Insert(_customerID, DateTime.Now, txtNewNote.Text);
			}

			BindGrid();
		}

		private void CustomerNotesPart_Init(object sender, EventArgs e)
		{
			base.TitleIconImageUrl = "~/Images/NoteHS.png";
			base.TitleUrl = "http://www.fratria.ru";
		}

		private void CustomerNotesPart_Load(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				BindGrid();
			}
		}

		private void CustomerNotesPart_PreRender(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				btnAddNewNote.Enabled = !String.IsNullOrEmpty(_customerID);

				grdCustomerNotes.DataBind();
			}
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Table);

			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);

			txtNewNote.RenderControl(writer);
			btnAddNewNote.RenderControl(writer);

			writer.RenderEndTag();
			writer.RenderEndTag();

			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);

			grdCustomerNotes.RenderControl(writer);

			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		private void BindGrid()
		{
			EnsureChildControls();

			using (CustomersTableAdapters.CustomerNotesTableAdapter adapter = new CustomersTableAdapters.CustomerNotesTableAdapter())
			{
				grdCustomerNotes.DataSource = (String.IsNullOrEmpty(this.CustomerID)) ?
					adapter.GetData() :
					adapter.GetDataByCustomer(_customerID);
			}
		}

		private Customers.CustomerNotesRow SelectedRow
		{
			get
			{
				EnsureChildControls();

				if (grdCustomerNotes.SelectedIndex >= 0)
				{
					int rowIndex = grdCustomerNotes.SelectedRow.DataItemIndex;

					Customers.CustomerNotesDataTable dataTable = grdCustomerNotes.DataSource as Customers.CustomerNotesDataTable;

					if (dataTable != null && rowIndex >= 0)
					{
						return dataTable[rowIndex];
					}
				}

				return null;
			}
		}

		#region INotesContract Members

		string INotesContract.Notes
		{
			get
			{
				Customers.CustomerNotesRow row = this.SelectedRow;

				return (row != null) ? row.NoteContent : null;
			}
			set
			{
				Customers.CustomerNotesRow row = this.SelectedRow;

				if (row != null)
				{
					row.NoteContent = value;

					using (CustomersTableAdapters.CustomerNotesTableAdapter adapter = new CustomersTableAdapters.CustomerNotesTableAdapter())
					{
						adapter.Update(row);
					}

					BindGrid();
				}
			}
		}

		DateTime INotesContract.CreationDate
		{
			get
			{
				Customers.CustomerNotesRow row = this.SelectedRow;

				return (row != null) ? row.NoteDate : DateTime.MinValue;
			}
		}

		#endregion
	}
}