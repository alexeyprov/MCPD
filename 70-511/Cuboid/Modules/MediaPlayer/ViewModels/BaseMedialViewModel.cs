using System.Windows.Input;
using Microsoft.Win32;

using Cuboid.Shared;
using Prism.Commands;

namespace Cuboid.Modules.MediaPlayer.ViewModels
{
    /// <summary>
    /// Base view model for media players
    /// </summary>
    public class BaseMedialViewModel : NotifyPropertyChangedObject
    {
        #region Private Fields

        private string _selectedFileName; 
        
        #endregion

        #region Constructor

        public BaseMedialViewModel()
        {
            OpenFileCommand = new DelegateCommand(ExecuteOpenFile);
        }

        #endregion

        #region Public Properties

        public ICommand OpenFileCommand
        {
            get;
            private set;
        }

        public string SelectedFileName
        {
            get
            {
                return _selectedFileName;
            }
            set
            {
                _selectedFileName = value;
                NotifyPropertyChanged(() => SelectedFileName);
            }
        }

        #endregion

        #region Protected Interface

        protected virtual string OpenDialogTitle
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual string OpenDialogFilter
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion

        #region Implementation

        private void ExecuteOpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckPathExists = true,
                Multiselect = false,
                Title = OpenDialogTitle,
                ValidateNames = true,
                Filter = OpenDialogFilter
            };

            if (dialog.ShowDialog() ?? false)
            {
                SelectedFileName = dialog.FileName;
            }
        }

        #endregion
    }
}
