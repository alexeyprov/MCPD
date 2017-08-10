using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Shell;

namespace SingletonDocManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    internal partial class App : Application, INotifyPropertyChanged
    {
        #region Private Constants

        private const string RESET_COMMAND = "#?RESET#"; 
        
        #endregion

        #region Private Fields

        private readonly IDictionary<string, Window> _documents = new Dictionary<string, Window>(StringComparer.InvariantCultureIgnoreCase);
        private readonly ObservableCollection<string> _paths = new ObservableCollection<string>(); 
        
        #endregion

        #region Public Events

        public event PropertyChangedEventHandler PropertyChanged; 
        
        #endregion

        #region Public Properties

        public IEnumerable<string> LoadedDocuments
        {
            get
            {
                //return _documents.Keys;
                return _paths;
            }
        }

        #endregion

        #region Public Methods

        public void ProcessCommandLine(IList<string> commandLine)
        {
            if (commandLine == null)
            {
                throw new ArgumentNullException("commandLine");
            }

            if (commandLine.Count > 0)
            {
                string pathOrCommand = commandLine[0];

                if (!HandleCommand(pathOrCommand))
                {
                    LoadDocument(pathOrCommand);
                }
            }
        }

        public void UnloadDocument(string path)
        {
            _documents.Remove(path);
            _paths.Remove(path);
            OnNotifyPropertyChanged("LoadedDocuments");
        }

        #endregion

        #region Overrides

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureTracing();
            ConfigureMainWindow();
            ConfigureJumpList();

            ProcessCommandLine(e.Args);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            ClearJumpList();
        }

        #endregion

        #region Implementation

        private bool HandleCommand(string command)
        {
            switch (command)
            {
                case RESET_COMMAND:
                    Reset();
                    return true;
            }

            return false;
        }

        private void Reset()
        {
            foreach (Window window in _documents.Values.ToArray())
            {
                window.Close();
            }
        }

        private void LoadDocument(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return;
            }

            Window window;
            if (!_documents.TryGetValue(path, out window))
            {
                window = new DocumentWindow(path)
                {
                    Owner = MainWindow
                };
                _documents[path] = window;
                _paths.Add(path);
                window.Show();

                OnNotifyPropertyChanged("LoadedDocuments");
            }

            window.Activate();
        }

        private void ConfigureJumpList()
        {
            JumpList list = new JumpList()
            {
                ShowRecentCategory = true
            };

            list.JumpItems.Add(
                new JumpTask()
                {
                    ApplicationPath = Assembly.GetEntryAssembly().Location,
                    Arguments = RESET_COMMAND,
                    Title = "Close all children",
                    Description = "Closes all children windows while leaving the top-level window open"
                });

            JumpList.SetJumpList(this, list);
        }

        private void ClearJumpList()
        {
            JumpList list = JumpList.GetJumpList(this);

            if (list != null)
            {
                JumpItem[] tasks = list.JumpItems.Where(i => i is JumpTask).ToArray();

                foreach (JumpItem task in tasks)
                {
                    list.JumpItems.Remove(task);
                }

                list.Apply();
            }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static void ConfigureTracing()
        {
            PresentationTraceSources.Refresh();

            PresentationTraceSources.DependencyPropertySource.Switch.Level = SourceLevels.All;
            PresentationTraceSources.DependencyPropertySource.Listeners.Add(
                //new XmlWriterTraceListener("trace.xml"));
                new ConsoleTraceListener());
        }

        private void ConfigureMainWindow()
        {
            base.MainWindow = new MainWindow();
            base.MainWindow.DataContext = this;
            base.MainWindow.Show();
        }

        #endregion
    }
}
