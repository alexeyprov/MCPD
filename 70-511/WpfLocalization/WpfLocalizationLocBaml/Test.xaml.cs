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
using System.Resources;
using System.Globalization;

namespace WpfLocalizationLocBaml
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public DateTime TestDate
        {
            get { return _TestDate; }
            set { _TestDate = value; }
        }
        private DateTime _TestDate = DateTime.Now;


        public decimal NumberValue
        {
            get { return _NumberValue; }
            set { _NumberValue = value; }
        }
        private decimal _NumberValue = 54123.33M;


        public Test()
        {
            // *** MUST SET THE LANGUAGE OR ELSE CURRENT CULTURE IS NOT RESPECTED
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            
            InitializeComponent();
            
            this.txtManualBoundNumberValue.Text = this.NumberValue.ToString("c");

            
        }
    }
}
