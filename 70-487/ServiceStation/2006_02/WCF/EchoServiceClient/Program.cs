using System;
using System.ServiceModel;
using ServiceLibrary;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // define service endpoints on client
                /*
                ServiceEndpoint httpEndpoint = new ServiceEndpoint(
                    ContractDescription.GetContract(typeof(IEchoService)),
                    new BasicHttpBinding(),
                    new EndpointAddress("http://localhost:8080/echo/svc"));
                ServiceEndpoint tcpEndpoint= new ServiceEndpoint(
                    ContractDescription.GetContract(typeof(IEchoService)),
                    new NetTcpBinding(),
                    new EndpointAddress("net.tcp://localhost:8081/echo/svc"));
                */
                IEchoService svc = null;
                
                // create channel factory based on HTTP endpoint
                using (ChannelFactory<IEchoService> httpFactory = 
                    new ChannelFactory<IEchoService>("httpEndpoint"))
                {
                    // create channel proxy for endpoint
                    svc = httpFactory.CreateChannel();
                    // invoke service operation
                    Console.WriteLine("Invoking HTTP endpoint: {0}",
                        svc.Echo("Hello, world"));
                }

                // create channel factory based on TCP endpoint
                using (ChannelFactory<IEchoService> tcpFactory =
                    new ChannelFactory<IEchoService>("tcpEndpoint"))
                {
                    // create channel proxy for endpoint
                    svc = tcpFactory.CreateChannel();
                    // invoke service operation
                    Console.WriteLine("Invoking TCP endpoint: {0}",
                        svc.Echo("Hello, world"));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
