using System;
using System.ServiceModel;
using ServiceLibrary;

namespace EchoServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // create the service host based on the service type
                using (ServiceHost host = new ServiceHost(
                    typeof(EchoService), new Uri("http://localhost:8080/echo")))
                {
                    /*
                    // create customized binding
                    BasicHttpBinding b = new BasicHttpBinding();
                    b.Security.Mode = BasicHttpSecurityMode.Transport;
                    b.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;

                    // define the service endpoints
                    host.AddServiceEndpoint(
                        typeof(IEchoService), new BasicHttpBinding(), "svc");
                    host.AddServiceEndpoint(
                        typeof(IEchoService), new NetTcpBinding(),
                        "net.tcp://localhost:8081/echo/svc");
                    host.AddServiceEndpoint(
                        typeof(IEchoService), b, "https://localhost:8082/echo/svc");
                    */

                    // open the service host (builds the service runtime)
                    host.Open();

                    // retrieve information about the service runtime
                    Console.WriteLine("{0} is open and has the following endpoints:\n",
                        host.Description.ServiceType);

                    int i=1;
                    foreach (ServiceEndpoint end in host.Description.Endpoints)
                    {
                        Console.WriteLine("Endpoint #{0}", i++);
                        Console.WriteLine("Address: {0}", end.Address.Uri.AbsoluteUri);
                        Console.WriteLine("Binding: {0}", end.Binding.Name);
                        Console.WriteLine("Contract: {0}\n", end.Contract.Name);
                    }

                    Console.WriteLine("The following EndpointListeners are active:\n");
                    foreach (EndpointListener l in host.EndpointListeners)
                        Console.WriteLine(l.Listener.Uri.AbsoluteUri);
                        
                    // keep the process alive
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e); ;
            }
        }
    }
}