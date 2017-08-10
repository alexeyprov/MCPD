using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms.Integration;

namespace CustomUI.Interop
{
    /// <summary>
    /// Interaction logic for WpfInputBox.xaml
    /// </summary>
    public partial class WpfInputBox :
        Window,
        IInputBox,
        INotifyPropertyChanged,
        IImplementsNotifyPropertyChanged
    {
        #region Private Fields

        private string _header;
        private string _label;
        private string _text; 
        
        #endregion

        #region Constructor

        public WpfInputBox()
        {
            InitializeComponent();

            DataContext = this;

            ElementHost.EnableModelessKeyboardInterop(this);
        }

        #endregion

        #region IInputBox Members

        public event EventHandler DataUpdated;

        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                this.NotifyPropertyChanged(() => Header);
            }
        }

        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                this.NotifyPropertyChanged(() => Label);
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                this.NotifyPropertyChanged(() => Text);
            }
        }

        void IInputBox.Activate()
        {
            Activate();
        }

        #endregion
    
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IImplementsNotifyPropertyChanged Members

        void IImplementsNotifyPropertyChanged.OnNotifyPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion

        #region Event Handlers

        private void Button_Click(object sender, EventArgs e)
        {
            if (DataUpdated != null)
            {
                DataUpdated(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
