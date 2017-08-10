using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace LoggerService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] 
			{ 
				new LoggerService.LoggerSvc(),
				new LoggerService.DebuggerSvc()
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
