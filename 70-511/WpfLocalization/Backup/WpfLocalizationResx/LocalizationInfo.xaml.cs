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
using Westwind.Wpf.Controls;
using System.ComponentModel;
using System.Windows.Markup;
using WpfLocalizationResx.LocalResources;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Interop;

namespace WpfLocalizationResx
{
    /// <summary>
    /// Interaction logic for LocalizationInfo.xaml
    /// </summary>
    public partial class LocalizationInfo : Window, INotifyPropertyChanged
    {
        
        public CultureInfo CurrentCulture
        {
            get { return _CultureInfo; }
            set { _CultureInfo = value; this.RaisePropertyChanged("CurrentCulture");  }
        }
        private CultureInfo _CultureInfo = null;

        
        public decimal NumberValue
        {
            get { return _NumberValue; }
            set { _NumberValue = value; this.RaisePropertyChanged("NumberValue"); }
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
            
            //ResExtension.InitializeResExtension( typeof(WpfLocalizationResx.Properties.Resources) );

            this.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            this.NumberValue = 3342.22M;            

            InitializeComponent();

            this.Loaded += new RoutedEventHandler(LocalizationInfo_Loaded);
        }

        void LocalizationInfo_Loaded(object sender, RoutedEventArgs e)
        {
            if (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
            {
                this.FlowDirection = FlowDirection.RightToLeft;
                this.IsRightToLeft = true;
            }
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

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
        }


        private void ButtonHandler(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender == this.btnExit || sender == this.mnuExit)
                this.Close();            
        }

        private void lstLanguageSelections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lstLanguageSelections.SelectedValue == null)
                return;

            string culture = ((ComboBoxItem) this.lstLanguageSelections.SelectedValue).Tag as string;
            App.SetCulture( culture,false);
            this.CurrentCulture = CultureInfo.CurrentUICulture;

            // this has no effect - has to be done before form initializes
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            LocalizationSettings.Current.CurrentCulture = this.CurrentCulture;

            // Update the image - do not for initial assignment only after form has initialized.
            if (this.IsInitialized)
            {
                MemoryStream ms = new MemoryStream();
                LocalizationInfoRes.CountryFlag.Save(ms, ImageFormat.Jpeg);
                ms.Position = 0;

                this.imgFlag.BeginInit();
                this.imgFlag.Source = BitmapFrame.Create(ms);
                this.imgFlag.EndInit();
                ms.Close();

                // this unfortunately has no effect - the document has to be reloaded
                this.Language = XmlLanguage.GetLanguage(this.CurrentCulture.IetfLanguageTag);

                //MessageBox.Show(WpfLocalizationResx.Properties.Resources.LanguageChangedMessageResxForm,
                // this.Title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void lstThemeSelections_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            App.SetTheme(this.lstThemeSelections.SelectedValue as string);
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal val = 0;
            decimal.TryParse(txtNumber.Text,out val);
            this.NumberValue = val;

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
