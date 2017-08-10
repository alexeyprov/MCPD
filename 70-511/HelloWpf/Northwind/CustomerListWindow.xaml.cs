using System.Configuration;
using System.Windows;
using System.Windows.Input;
using Northwind;
using Northwind.Data.ClassicAdo;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        private readonly CustomerData _customers;

        public CustomerListWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
            _customers = new CustomerData(connectionString);

            CustomersList.ItemsSource = _customers.GetAllCustomers(null);
        }

        private void CustomersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Customer customer = CustomerDataControl.DataContext as Customer;
            if (customer != null)
            {
                Window customerWindow = new CustomerWindow(customer)
                {
                    Owner = this
                };

                customerWindow.ShowDialog();
            }
        }
    }
}
