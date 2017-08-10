using System.Configuration;
using System.Linq;
using System.Windows;

using Northwind.Data.Entities;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        public OrdersWindow(string customerId)
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;
            NorthwindObjectContext context = new NorthwindObjectContext(connectionString);

            OrdersGrid.ItemsSource = context.Orders
                .Include("Employee") // to display linked data
                .Include("Shipper")
                .Where(o => o.Customer.CustomerID == customerId)
                .ToArray(); // must be IList for sorting support

            ShipperColumn.ItemsSource = context.Shippers;
        }
    }
}
