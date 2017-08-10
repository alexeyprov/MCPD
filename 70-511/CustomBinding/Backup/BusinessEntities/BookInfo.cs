using System;

namespace BusinessEntities
{
	/// <summary>
	/// Simple entity class for testing - book data
	/// </summary>
	public class BookInfo
	{
		#region Fields

		private string _author;
		private string _title;
		private string _isbn;
		private int _pageCount;

		#endregion

		#region Construction

		public void CopyFrom(BookInfo other)
		{
			Author = other.Author;
			Title = other.Title;
			Isbn = other.Isbn;
			PageCount = other.PageCount;
		}

		#endregion

		#region Properties

		public string Author
		{
			get
			{
				return _author;
			}
			set
			{
				if (_author != value)
				{
					_author = value;
					if (AuthorChanged != null)
					{
						AuthorChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (_title != value)
				{
					_title = value;
					if (TitleChanged != null)
					{
						TitleChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		public string Isbn
		{
			get
			{
				return _isbn;
			}
			set
			{
				if (_isbn != value)
				{
					_isbn = value;
					if (IsbnChanged != null)
					{
						IsbnChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		public int PageCount
		{
			get
			{
				return _pageCount;
			}
			set
			{
				if (_pageCount != value)
				{
					_pageCount = value;
					if (PageCountChanged != null)
					{
						PageCountChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		#endregion

		#region Events

		public event EventHandler AuthorChanged;
		public event EventHandler TitleChanged;
		public event EventHandler IsbnChanged;
		public event EventHandler PageCountChanged;

		#endregion
	}
}