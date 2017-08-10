using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessEntities
{
    /// <summary>
    /// Simple entity class for testing - book data
    /// </summary>
    public class BookInfo : IDataErrorInfo
    {
        #region Private Fields

        private static Regex _isbnRegex; 
        private string _author;
        private string _title;
        private string _isbn;
        private int _pageCount;

        private readonly IDictionary<string, string> _errors;

        #endregion

        #region Construction

        static BookInfo()
        {
            _isbnRegex = new Regex(@"^\d{13}|(\d{9}[0-9X])$", RegexOptions.Compiled);
        }

        public BookInfo()
        {
            _errors = new Dictionary<string, string>();
        }

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

                _errors["Author"] = string.IsNullOrEmpty(_author) ? "Author is required" : null;
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

                _errors["Title"] = string.IsNullOrEmpty(_title) ? "Title is required" : null;
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

                string error = null;

                if (string.IsNullOrEmpty(_isbn))
                {
                    error = "ISBN is required";
                }
                else if (!_isbnRegex.IsMatch(_isbn))
                {
                    error = "ISBN format is invalid";
                }

                _errors["Isbn"] = error;
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

                _errors["PageCount"] = _pageCount <= 0 ? "Page count must be positive" : null;
            }
        }

        #endregion

        #region Events

        public event EventHandler AuthorChanged;
        public event EventHandler TitleChanged;
        public event EventHandler IsbnChanged;
        public event EventHandler PageCountChanged;

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error
        {
            get
            {
                return string.Join(
                    Environment.NewLine,
                    from e in _errors.Values
                    where !string.IsNullOrEmpty(e)
                    select e);
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string result;
                return _errors.TryGetValue(columnName, out result) ?
                    result :
                    null;
            }
        }

        #endregion
    }
}