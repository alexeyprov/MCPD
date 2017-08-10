using System;
using System.ComponentModel;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

using CustomUI.Interop;
using HelloWpf.Controls;
using HelloWpf.Core;
using HelloWpf.Northwind;
using HelloWpf.UiTricks;

namespace HelloWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : 
        Window,
        INotifyPropertyChanged,
        IImplementsNotifyPropertyChanged
    {
        #region Private Fields

        private IInputBox _inputBox;
        private string _userInfo; 

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            _userInfo = "User: Unknown";

            DataContext = this;
        } 

        #endregion

        #region Public Properties

        public string UserInfo
        {
            get
            {
                return _userInfo;
            }
            set
            {
                _userInfo = value;
                this.NotifyPropertyChanged(() => UserInfo);
            }
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            synthesizer.SpeakAsync("Hello, WPF");
        }

        private void OnInputBoxUpdated(object sender, EventArgs e)
        {
            UserInfo = string.Format("User: {0}", _inputBox.Text);
        }

        private void OnInputBoxClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)_inputBox).FormClosed -= OnInputBoxClosed;
            _inputBox.DataUpdated -= OnInputBoxUpdated;
            _inputBox = null;
        }

        #endregion

        #region Command Handlers

        #region Core

        private void DynamicXaml_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<ExternalXamlWindow>();
        }

        private void ElementBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<ElementBindingWindow>();
        }

        private void MonitoredCommands_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<CommandMonitorWindow>();
        }

        private void FlowDocument_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<FlowDocumentWindow>();
        }

        #endregion

        #region UI Tricks

        private void CustomBehavior_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<BehaviorsWindow>();
        }

        private void Reflection_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<ReflectionWindow>();
        }

        private void SquaresGame_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<SquaresWindow>();
        }

        private void Animations_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<AnimationsWindow>();
        }

        private void BombGame_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<BombGameWindow>();
        }

        private void CalloutWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<CalloutWindow>();
        }

        private void ThreeDDemo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<ThreeDWindow>();
        }

        private void WinFormsInterop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_inputBox == null)
            {
                WinFormsInputBox inputBox = InputBoxFactory.CreateInputBox<WinFormsInputBox>(
                    "What is your name?",
                    "Name:",
                    OnInputBoxUpdated);

                inputBox.FormClosed += OnInputBoxClosed;

                inputBox.Show();

                _inputBox = inputBox;
            }
            else
            {
                _inputBox.Activate();
            }
        }

        private void DragnDrop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<DragnDropWindow>();
        }

        #endregion

        #region Controls

        private void Popup_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<PopupHostWindow>();
        }

        private void ControlTemplateBrowser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<TemplateBrowserWindow>();
        }

        private void ColorPicker_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<ColorPickerHostWindow>();
        }

        private void FlipPanel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<FlipPanelHostWindow>();
        }

        private void CustomDrawn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<CustomDrawnHostWindow>();
        }

        #endregion

        #region Northwind

        private void Customers_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<CustomerListWindow>();
        }

        private void Employees_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<EmployeeListWindow>();
        }

        private void Suppliers_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<SupplierListWindow>();
        }

        private void Geography_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<GeographyWindow>();
        }

        private void Products_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowWindow<ProductWindow>();
        }

        #endregion 

        #endregion

        #region Implementation

        private void ShowWindow<T>() where T : Window, new()
        {
            T window = new T()
            {
                Owner = this
            };

            window.Show();
        }

        #endregion
    }
}
