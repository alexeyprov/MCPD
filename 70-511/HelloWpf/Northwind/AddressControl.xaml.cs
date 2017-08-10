using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for AddressControl.xaml
    /// </summary>
    public partial class AddressControl : UserControl
    {
        public AddressControl()
        {
            InitializeComponent();
        }

        private void TextBox_ValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                foreach (ValidationError error in Validation.GetErrors((DependencyObject)sender))
                {
                    Debug.WriteLine(error.ErrorContent);
                }
            }
        }
    }
}
