using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Threading;
using System.Resources;


namespace WpfLocalizationResx
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
            else if ((sender == btnLocalizationInfoAttached))
            {
                this.ShowWindow<LocalizationInfoAttached>();
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
