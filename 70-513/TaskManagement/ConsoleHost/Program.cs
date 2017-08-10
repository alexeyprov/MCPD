using System;
using System.ServiceModel;

namespace ConsoleHost
{
	class Program
	{
		static void Main(string[] args)
		{
			using (ServiceHost host = new ServiceHost(typeof(TaskService.TaskService)))
			{
				host.Open();
				
				Console.WriteLine("Console service host started.");
				Console.WriteLine("Press RETURN to terminate...");
				Console.ReadLine();
			}
		}
	}
}
