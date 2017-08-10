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
	/// Summary description for CustomerNotesEditorPart
	/// </summary>
	public class CustomerNotesEditorPart : WebPart
	{
		private Label lblCaption;
		private TextBox txtNotes;
		private Button btnUpdate;

		private INotesContract _notesContract;
		private bool _updateRequired;

		public CustomerNotesEditorPart()
		{
			this.PreRender += new EventHandler(CustomerNotesEditorPart_PreRender);
		}

		private void CustomerNotesEditorPart_PreRender(object sender, EventArgs e)
		{
			if (_notesContract != null)
			{
				EnsureChildControls();

				if (_updateRequired)
				{
					_notesContract.Notes = txtNotes.Text;
				}
				else
				{
					txtNotes.Text = _notesContract.Notes;
				}

				lblCaption.Text = _notesContract.CreationDate.ToShortDateString();
			}
		}

		[ConnectionConsumer("Customer Notes", "NotesConsumerID")]
		public void Connect(INotesContract notesContract)
		{
			_notesContract = notesContract;

			// the connection method may be called after event handlers
			if (_updateRequired && _notesContract != null)
			{
				EnsureChildControls();
				_notesContract.Notes = txtNotes.Text;
			}
		}

		protected override void CreateChildControls()
		{
			lblCaption = new Label();
			lblCaption.Text = "Not connected";

			txtNotes = new TextBox();
			txtNotes.TextMode = TextBoxMode.MultiLine;
			txtNotes.Rows = 5;

			btnUpdate = new Button();
			btnUpdate.Text = "Update";
			btnUpdate.Click += new EventHandler(btnUpdate_Click);

			base.Controls.Add(lblCaption);
			base.Controls.Add(txtNotes);
			base.Controls.Add(btnUpdate);
		}

		public override WebPartVerbCollection Verbs
		{
			get
			{
				return new WebPartVerbCollection(base.Verbs,
					new WebPartVerb[]
					{
						new WebPartVerb("Refresh", Refresh_Click)
						{
							Text = "Refresh"
						}
					});
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			_updateRequired = true;
		}

		private void Refresh_Click(object sender, WebPartEventArgs e)
		{
			_updateRequired = true;
		}
	} 
}