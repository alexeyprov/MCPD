using System;
using System.ServiceModel;

namespace DuplexHost
{
	class Program
	{
		private static void Main()
		{
			using (ServiceHost host = new ServiceHost(typeof(DuplexService.GreetingsService)))
			{
				host.Open();

				Console.WriteLine("Service started. Press any key to exit.");
				Console.ReadKey();
			}
		}
	}
}
