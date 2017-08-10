using System.Configuration;
using System.Windows;

using Northwind.Data.Entities;

namespace HelloWpf.Northwind
{
    /// <summary>
    /// Interaction logic for GeographyWindow.xaml
    /// </summary>
    public partial class GeographyWindow : Window
    {
        public GeographyWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings[ConstantsHelper.NORTHWIND_ENTITIES_CONNECTION_STRING].ConnectionString;
            NorthwindObjectContext context = new NorthwindObjectContext(connectionString);

            GeographyTree.ItemsSource = context.Regions.Include("Territories");
        }
    }
}
