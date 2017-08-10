using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace Rashim.RND.ManagedDiscovery.ServiceHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var baseAddress =
                new Uri("http://localhost/demo");

            using (var host = new System.ServiceModel.ServiceHost(new DiscoveryProxyService(), baseAddress))
            {               
                host.AddServiceEndpoint(new DiscoveryEndpoint(new WSHttpBinding(), new EndpointAddress(host.BaseAddresses[0]+"/probe")){IsSystemEndpoint = false});
                host.AddServiceEndpoint(new AnnouncementEndpoint(new WSHttpBinding(), new EndpointAddress(host.BaseAddresses[0] + "/announcement")) { IsSystemEndpoint = false });
                host.Description.Endpoints.ToList().ForEach((item)=> Console.WriteLine(item.ListenUri));
              
                host.Open();

                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
