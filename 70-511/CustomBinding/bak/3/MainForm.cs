using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CustomBinding
{
    public partial class MainForm : Form
	{
		#region Fields

		private BookInfo[] _books;

		#endregion

		#region Construction

		public MainForm()
        {
            InitializeComponent();
		}

		#endregion

		#region Event Handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			// Load books array
			XmlSerializer ser = new XmlSerializer(typeof(BookInfo[]), new XmlRootAttribute("BookInfos"));
			using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream("CustomBinding.Books.xml"))
			{
				_books = (BookInfo[]) ser.Deserialize(stm);
			}

			// Bind controls to the current book
			Binding b = new Binding("Text", _books, "Author", true);
			txtAuthor.DataBindings.Add(b);
			txtTitle.DataBindings.Add("Text", _books, "Title");
			txtIsbn.DataBindings.Add("Text", _books, "Isbn");
			txtPageCount.DataBindings.Add("Text", _books, "PageCount");
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			MovePosition(-1);
		}

		private void btnForward_Click(object sender, EventArgs e)
		{
			MovePosition(1);
		}

		#endregion

		#region Implementation

		private void MovePosition(int delta)
		{
			Debug.Assert(_books != null);

			int idx = (BindingContext[_books].Position + delta) % _books.Length;
			if (idx < 0)
			{
				idx += _books.Length;
			}

			Debug.Assert(idx >= 0);
			Debug.Assert(idx < _books.Length);

			BindingContext[_books].Position = idx;
			lblBookNo.Text = String.Format("Book number {0}", idx + 1); 
		}

		#endregion
	}
}