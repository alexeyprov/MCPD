using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HelloClient.HelloWcfService;

namespace HelloClient
{
	class Program
	{
		static void Main(string[] args)
		{
			ProductData order = new ProductData();

			order.ItemCount = 5;
			order.ItemPrice = 199;
			order.Name = "Wii Console";

			GetCostServiceClient client = new GetCostServiceClient();
			Console.WriteLine(client.GetTotalCost(order));
			client.Close();
		}
	}
}
