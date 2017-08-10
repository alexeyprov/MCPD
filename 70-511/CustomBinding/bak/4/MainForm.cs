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
			bsBooks.DataSource = _books;
			// Bug workaround: have to set null in designer
			// and valid value here
			grdBooks.DataSource = bsBooks;

			// Bind text controls to the book list manually
			Binding b = new Binding("Text", bsBooks, "Author", true);
			txtAuthor.DataBindings.Add(b);
			txtTitle.DataBindings.Add("Text", bsBooks, "Title");
			txtIsbn.DataBindings.Add("Text", bsBooks, "Isbn");
			txtPageCount.DataBindings.Add("Text", bsBooks, "PageCount");
		}

		#endregion
	}
}