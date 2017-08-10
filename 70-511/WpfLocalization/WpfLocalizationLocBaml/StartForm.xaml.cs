// © 2009 Rick Strahl. All rights reserved. 
// See http://wpflocalization.codeplex.com for related whitepaper and updates
// See http://wpfclientguidance.codeplex.com for other WPF resources

using System.Windows;

namespace WpfLocalizationLocBaml
{
    /// <summary>
    /// Interaction logic for StartForm.xaml
    /// </summary>
    public partial class StartForm : Window
    {
        public StartForm()
        {
            InitializeComponent();            
        }

        private void ButtonHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender ==  this.btnLocalizationInfo)
            {
                this.ShowWindow<LocalizationInfo>();                
            }
            else if ((sender == btnTest))
            {
                this.ShowWindow<Test>();
            }
            else if (sender == btnExit)
            {
                this.Close();
                Application.Current.Shutdown();
            }
        }

        private TWindow ShowWindow<TWindow>() 
                where TWindow: Window, new()           
        {
            TWindow form = new TWindow();
            form.Show();
            return form;
        }
    }
}
