using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

using HelloWcf;


namespace HelloHost
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a ServiceHost for the GetCostService type and use
			// the base address from config.
			ServiceHost hostDefault = new ServiceHost(typeof(GetCostService));

			TimeSpan closeTimeout = hostDefault.CloseTimeout;
			TimeSpan openTimeout = hostDefault.OpenTimeout;

			ServiceAuthorizationBehavior authorization = hostDefault.Authorization;

			ServiceCredentials credentials = hostDefault.Credentials;

			ServiceDescription description = hostDefault.Description;

			int manualFlowControlLimit = hostDefault.ManualFlowControlLimit;

			NetTcpBinding portsharingBinding = new NetTcpBinding();
			hostDefault.AddServiceEndpoint(typeof(GetCostService),
				portsharingBinding,
				"net.tcp://localhost/HelloWcf/GetCostService");

			int newLimit = hostDefault.IncrementManualFlowControlLimit(100);

			using (ServiceHost serviceHost = new ServiceHost(typeof(GetCostService)))
			{
				try
				{
					// Open the ServiceHost to start listening for messages.
					serviceHost.Open();
					// The service can now be accessed.
					Console.WriteLine("The service is ready.");
					Console.WriteLine("Press <ENTER> to terminate service.");
					Console.ReadLine();

					// Close the ServiceHost.
					serviceHost.Close();
				}
				catch (TimeoutException timeProblem)
				{
					Console.WriteLine(timeProblem.Message);
					Console.ReadLine();
				}
				catch (CommunicationException commProblem)
				{
					Console.WriteLine(commProblem.Message);
					Console.ReadLine();
				}
			}
		}
	}
}
