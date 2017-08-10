using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using Northwind;
using Northwind.Data.ClassicAdo;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        #region Private Fields

        private readonly CustomerData _customers; 

        #endregion

        #region Constructors

        public CustomerWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_CONNECTION_STRING].ConnectionString;
            _customers = new CustomerData(connectionString);
        }

        public CustomerWindow(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }

            InitializeComponent();

            CustomerDataControl.DataContext = customer;
            CustomerIDTextBox.Text = customer.ID;

            CustomerIDTextBox.IsEnabled = false;
            LoadButton.IsEnabled = false;
        }

        #endregion

        #region Event Handlers

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerDataControl.DataContext = _customers.GetCustomer(CustomerIDTextBox.Text);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            ICollection<ValidationError> errors = new List<ValidationError>();

            GetErrors(this, errors);

            if (errors.Count > 0)
            {
                MessageBox.Show(
                    "One or more validation errors occured.\n" +
                    "Please fix them before continuing:\n\n" +
                    string.Join("\n", errors.Select(v => v.ErrorContent)),
                    "Validation Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                Customer customer = (Customer)CustomerDataControl.DataContext;

                if (customer != null)
                {
                    _customers.UpdateCustomer(customer);
                }
            }
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            Window ordersWindow = new OrdersWindow(CustomerIDTextBox.Text);
            ordersWindow.ShowDialog();
        }

        #endregion

        #region Implementation

        private static void GetErrors(DependencyObject element, ICollection<ValidationError> errors)
        {
            if (element == null)
            {
                return;
            }

            foreach (ValidationError error in Validation.GetErrors(element))
            {
                errors.Add(error);
            }

            foreach (DependencyObject child in
                LogicalTreeHelper.GetChildren(element).OfType<DependencyObject>())
            {
                GetErrors(child, errors);
            }
        }
 
        #endregion
    }
}
