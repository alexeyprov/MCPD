using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;

using NorthwindDataClient.NorthwindService;

namespace NorthwindDataClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private NorthwindObjectContext _context;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// Save changes made to objects tracked by the context.
				_context.SaveChanges();
			}
			catch (DataServiceRequestException ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void buttonClose_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Window1_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				const string customerId = "ALFKI";

				// Replace the host server and port number with the values 
				// for the test server hosting your Northwind data service instance.
				Uri svcUri = new Uri("http://localhost:49282/NorthwindDataService.svc");

				// Instantiate the DataServiceContext.
				_context = new NorthwindObjectContext(svcUri);

				// Define a LINQ query that returns Orders and 
				// Order_Details for a specific customer.
				IEnumerable<Order> ordersQuery = from o in _context.Orders.Expand("Lines")
												 where o.Customer.CustomerID == customerId
												 select o;

				// Create an DataServiceCollection<T> based on 
				// execution of the LINQ query for Orders.
				DataServiceCollection<Order> customerOrders = new DataServiceCollection<Order>(ordersQuery);

				// Make the DataServiceCollection<T> the binding source for the Grid.
				orderItemsGrid.DataContext = customerOrders;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
	}
}
