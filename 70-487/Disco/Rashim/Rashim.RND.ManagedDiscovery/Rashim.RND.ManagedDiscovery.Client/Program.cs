using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Discovery;
using Rashim.RND.ManagedDiscovery.DiscoverableService;

namespace Rashim.RND.ManagedDiscovery.Client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var probeAddress = new Uri("http://localhost/demo/probe");
            var discoveryEndpoint = new DiscoveryEndpoint(new WSHttpBinding(), new EndpointAddress(probeAddress));
            var discoveryClient = new DiscoveryClient(discoveryEndpoint);
            var findCriteria = new FindCriteria(typeof (IStringService));

            findCriteria.Scopes.Add(new Uri("ldap:///ou=people,o=rashim"));

            var findResponse = discoveryClient.Find(findCriteria);

            if (findResponse != null)
            {
                if (findResponse.Endpoints != null)
                {
                    if (findResponse.Endpoints.Count > 0)
                    {
                        var factory = new ChannelFactory<IStringService>(new BasicHttpBinding(),
                                                                         findResponse.Endpoints[0].Address);
                        var channel = factory.CreateChannel();
                        Console.WriteLine(channel.ToUpper("Rashim"));
                    }
                }
            }
            else
            {
                Console.WriteLine("Could not find the Services");
            }
            Console.ReadLine();
        }
    }
}
