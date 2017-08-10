using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace ListDrivers
{
	class Program
	{
		static void Main(string[] args)
		{
			ServiceController[] drivers = ServiceController.GetDevices();
			int adapters = 0,
				fileDrivers = 0,
				kernelDrivers = 0,
				recogDrivers = 0;

			foreach (ServiceController sc in drivers)
			{
				Console.WriteLine("[{0}]: {1} ({2}), type {3}", 
					sc.Status,
					sc.ServiceName, sc.DisplayName, sc.ServiceType);

				switch (sc.ServiceType)
				{
					case ServiceType.Adapter:
						adapters++;
						break;
					case ServiceType.FileSystemDriver:
						fileDrivers++;
						break;
					case ServiceType.KernelDriver:
						kernelDrivers++;
						break;
					case ServiceType.RecognizerDriver:
						recogDrivers++;
						break;
				}
			}
			Console.WriteLine("=============");
			Console.WriteLine("Hardware device drivers: {0}", adapters);
			Console.WriteLine("File-system drivers: {0}", fileDrivers);
			Console.WriteLine("Kernel drivers: {0}", kernelDrivers);
			Console.WriteLine("File-system recognition drivers: {0}", recogDrivers);
			Console.WriteLine("Total: {0}", drivers.Length);
			Console.ReadKey(false);
		}
	}
}
