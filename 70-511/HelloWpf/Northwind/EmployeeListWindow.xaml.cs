using System.Configuration;
using System.Windows;
using Northwind.Data.Entities;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for EmployeeListWindow.xaml
    /// </summary>
    public partial class EmployeeListWindow : Window
    {
        public EmployeeListWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;
            NorthwindObjectContext context = new NorthwindObjectContext(connectionString);
            EmployeeList.ItemsSource = context.Employees.Include(nameof(Employee.Subordinates));
        }
    }
}
