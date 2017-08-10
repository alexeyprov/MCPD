// © 2009 Rick Strahl. All rights reserved. 
// See http://wpflocalization.codeplex.com for related whitepaper and updates
// See http://wpfclientguidance.codeplex.com for other WPF resources

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Configuration;
using System.Diagnostics;

namespace WpfLocalizationLocBaml
{
    /// <summary>
    /// Interaction logic for LocalizationInfo.xaml
    /// </summary>
    public partial class LocalizationInfo : Window, INotifyPropertyChanged
    {

        public CultureInfo CurrentCulture
        {
            get { return _CultureInfo; }
            set { _CultureInfo = value; this.RaisePropertyChanged("CurrentCulture"); }
        }
        private CultureInfo _CultureInfo = null;


        public decimal NumberValue
        {
            get { return _NumberValue; }
            set
            {
                _NumberValue = value;
                this.RaisePropertyChanged("NumberValue");
            }
        }
        private decimal _NumberValue = 0M;



        public bool IsRightToLeft
        {
            get { return _IsRightToLeft; }
            set
            {
                _IsRightToLeft = value;
                if (value)
                    this.FlowDirection = FlowDirection.RightToLeft;
                else
                    this.FlowDirection = FlowDirection.LeftToRight;

                RaisePropertyChanged("RightToLeft");
            }
        }
        private bool _IsRightToLeft = false;


        public LocalizationInfo()
        {
            //ResExtension.InitializeResExtension( typeof(WpfClickOnce.Properties.Resources) );

            this.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

            // MAKE SURE you set the language of the page explicitly or else
            // all number and date formatting occurs using 
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            InitializeComponent();

            this.Loaded += LocalizationInfo_Loaded;
        }

        void LocalizationInfo_Loaded(object sender, RoutedEventArgs e)
        {

            if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                this.IsRightToLeft = true;
            }


            this.NumberValue = 3342.22M;

            string culture = CurrentCulture.IetfLanguageTag;
            foreach (ComboBoxItem item in this.lstLanguageSelections.Items)
            {
                if ((string)item.Tag == culture)
                {
                    item.IsSelected = true;
                    break;
                }
            }

            string[] themes = new string[] { "BureauBlue", "ExpressionDark", "WhistlerBlue", "Luna" };

            ObservableCollection<string> themes2 = new ObservableCollection<string>(themes);
            this.lstThemeSelections.ItemsSource = themes2;
            this.lstThemeSelections.SelectedValue = App.Theme;
            this.txtDate.SelectedDate = DateTime.Now;


            BitmapImage newBitmapImage = new BitmapImage(new Uri(string.Format("assets/{0}.jpg", this.CurrentCulture.IetfLanguageTag), UriKind.Relative));
            this.imgFlag.BeginInit();
            this.imgFlag.Source = newBitmapImage;
            this.imgFlag.EndInit();
        }


        private void ButtonHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender == this.btnExit || sender == this.mnuExit)
                this.Close();

        }

        private void lstLanguageSelections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string origCulture = CultureInfo.CurrentUICulture.IetfLanguageTag;
            string culture = ((ComboBoxItem)this.lstLanguageSelections.SelectedValue).Tag as string;

            // static app method that acutally sets the culture
            App.SetCulture(culture, true);  // force windows to close 

            if (this.IsInitialized && culture != origCulture)
            {
                System.Configuration.Configuration config =
                       ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Add an Application Setting.            
                config.AppSettings.Settings.Remove("Culture");
                config.AppSettings.Settings.Add("Culture", culture);

                // Save the configuration file.
                config.Save(ConfigurationSaveMode.Modified);

                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");

                //// if you'd rather restart the app use the following code
                //Process.Start(
                //    new ProcessStartInfo(Environment.CommandLine)
                //    {
                //        UseShellExecute = false,
                //        WindowStyle = ProcessWindowStyle.Normal
                //    });

                //Application.Current.Shutdown();
            }
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal val = 0;
            decimal.TryParse(txtNumber.Text, out val);
            this.NumberValue = val;
        }

        private void lstThemeSelections_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            App.SetTheme(this.lstThemeSelections.SelectedValue as string);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
