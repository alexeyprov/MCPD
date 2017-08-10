using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UpdateControls.XAML;

namespace DataBoundMenus
{
    /// <summary>
    /// Wraps an ApplicationDataModel and provides properties suitable
    /// for data binding. These properties control the window title,
    /// status bar, and menus.
    /// </summary>
    public class ApplicationViewModel : IFileHandler
    {
        // The application data model holds the current open file name
        // and the list of most recently opened files.
        private ApplicationDataModel _dataModel;
        // Generate random file names, just for demonstration.
        private Random _random = new Random();

        public ApplicationViewModel(ApplicationDataModel dataModel)
        {
            _dataModel = dataModel;
        }

        public string Title
        {
            get
            {
                // Display the open file name in the window title.
                if (_dataModel.OpenFileName == null)
                    return "DataBoundMenus";
                else
                    return string.Format("{0} - DataBoundMenus",
                        _dataModel.OpenFileName);
            }
        }

        public string LastAction
        {
            get { return _dataModel.LastAction; }
            set { _dataModel.LastAction = value; }
        }

        public IEnumerable<RecentFileViewModel> RecentFiles
        {
            get
            {
                // Create a RecentFileViewModel for each recent file.
                // The view model serves the menu item.
                return _dataModel.RecentFiles
                    .Select((fileName, index) =>
                        new RecentFileViewModel(index, fileName, this));
            }
        }

        public bool IsFileOpen
        {
            get { return _dataModel.OpenFileName != null; }
        }

        public ICommand FileNewCommand
        {
            get
            {
                // Simulate opening a new file.
                return MakeCommand
                    .Do(() =>
                    {
                        _dataModel.OpenFile("New" + _random.Next(9999));
                        _dataModel.LastAction = "New";
                    });
            }
        }

        public ICommand FileOpenCommand
        {
            get
            {
                // Simulate opening an existing file.
                return MakeCommand
                    .Do(() =>
                    {
                        _dataModel.OpenFile("Existing" + _random.Next(9999));
                        _dataModel.LastAction = "Open";
                    });
            }
        }

        public ICommand FileSaveCommand
        {
            get
            {
                // We can only save a file when one is open.
                return MakeCommand
                    .When(() => _dataModel.OpenFileName != null)
                    .Do(() => _dataModel.LastAction = "Save");
            }
        }

        public ICommand FileCloseCommand
        {
            get
            {
                // We can only close a file when one is open.
                return MakeCommand
                    .When(() => _dataModel.OpenFileName != null)
                    .Do(() =>
                    {
                        _dataModel.CloseFile();
                        _dataModel.LastAction = "Close";
                    });
            }
        }

        #region IFileHandler Members

        public void Open(string fileName)
        {
            // Respond to the RecentFileViewModel.
            // Simulate reopening a file.
            _dataModel.OpenFile(fileName);
            _dataModel.LastAction = "Open " + fileName;
        }

        #endregion
    }
}
