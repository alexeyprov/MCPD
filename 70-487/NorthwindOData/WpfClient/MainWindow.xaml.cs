using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Windows;
using System.Xml.Linq;

using WpfClient.NorthwindDataService;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string CUSTOMER_ID = "ALFKI";
        private const string BASE_URI = "http://localhost:49283/NorthwindDataService.svc";

        private readonly NorthwindObjectContext _context;

        public MainWindow()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _context = new NorthwindObjectContext(
                    new Uri(BASE_URI));
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                IEnumerable<Order> orders = 
                    from o in _context.Orders.Expand(x => x.Lines)
                    where o.Customer.CustomerID == CUSTOMER_ID
                    select o;

                DataContext = new DataServiceCollection<Order>(orders);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DataServiceRequestException re)
            {
                MessageBox.Show(ExtractErrorMessage(re));
            }
        }

        private void buttonTestJson_Click(object sender, RoutedEventArgs e)
        {
            string json;

            using (WebClient client = new WebClient())
            {
                client.Headers["Accept"] = "application/json";

                json = client.DownloadString(
                    string.Format("{0}/Customers('{1}')", BASE_URI, CUSTOMER_ID));
            }

            MessageBox.Show(json);
        }

        private string ExtractErrorMessage(DataServiceRequestException re)
        {
            DataServiceClientException ce = re.InnerException as DataServiceClientException;

            if (ce == null || string.IsNullOrEmpty(ce.Message))
            {
                return re.Message;
            }

            XNamespace ns = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
            XElement xe = XElement.Parse(ce.Message);

            return xe.Descendants(ns + "message").Single().Value;
        }
    }
}
