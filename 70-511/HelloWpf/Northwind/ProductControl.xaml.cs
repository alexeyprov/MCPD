using System.Globalization;
using System.Windows.Controls;
using System.Windows.Markup;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
        public ProductControl()
        {
            Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            InitializeComponent();
        }
    }
}
