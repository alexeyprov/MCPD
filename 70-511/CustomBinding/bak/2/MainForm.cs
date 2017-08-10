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
		private BookInfo _currentBook;
		private int _bookIdx;
		private IList<SimpleBinding> _bindings;

		#endregion

		#region Construction

		public MainForm()
        {
			_currentBook = new BookInfo();
			_bindings = new List<SimpleBinding>();

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
			Binding b = new Binding("Text", _currentBook, "Author", true);
			txtAuthor.DataBindings.Add(b);
			txtTitle.DataBindings.Add("Text", _currentBook, "Title");
			txtIsbn.DataBindings.Add("Text", _currentBook, "Isbn");
			txtPageCount.DataBindings.Add("Text", _currentBook, "PageCount");

			// Refresh current book from DB (array)
			LoadCurrentBook();
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			SaveCurrentBook();
			if (--_bookIdx < 0)
			{
				_bookIdx += _books.Length;
			}
			LoadCurrentBook();
		}

		private void btnForward_Click(object sender, EventArgs e)
		{
			SaveCurrentBook();
			if (++_bookIdx >= _books.Length)
			{
				_bookIdx -= _books.Length;
			}
			LoadCurrentBook();
		}

		#endregion

		#region Implementation

		private void SaveCurrentBook()
		{
			Debug.Assert(_books != null);
			Debug.Assert(_bookIdx >= 0);
			Debug.Assert(_bookIdx < _books.Length);

			_books[_bookIdx].CopyFrom(_currentBook);
		}

		private void LoadCurrentBook()
		{
			Debug.Assert(_books != null);
			Debug.Assert(_bookIdx >= 0);
			Debug.Assert(_bookIdx < _books.Length);

			lblBookNo.Text = String.Format("Book number {0}", _bookIdx + 1); 
			_currentBook.CopyFrom(_books[_bookIdx]);
		}

		#endregion
	}
}