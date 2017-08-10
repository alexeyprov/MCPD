using System;

using HelloClient.WebHostedService;

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

            GetCostServiceClient client = new GetCostServiceClient("BasicHttpBinding_IGetCostService");
            Console.WriteLine(client.GetTotalCost(order));
            client.Close();

            Console.ReadLine();
        }
    }
}
