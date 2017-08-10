using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using Rashim.RND.ManagedDiscovery.DiscoverableService;

namespace Rashim.RND.ManagedDiscovery.ServiceHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var announcementAddress =
                new Uri("http://localhost/demo/announcement");
            var host = new System.ServiceModel.ServiceHost(typeof (StringService));

            try
            {
                host.AddDefaultEndpoints();
                var announcementEndpoint = new AnnouncementEndpoint(new WSHttpBinding(),
                                                                    new EndpointAddress(announcementAddress));
                var serviceDiscoveryBehavior = new ServiceDiscoveryBehavior();
                serviceDiscoveryBehavior.AnnouncementEndpoints.Add(announcementEndpoint);
                host.Description.Behaviors.Add(serviceDiscoveryBehavior);
                host.Open();

                host.Description.Endpoints.ToList().ForEach((endpoint) => Console.WriteLine(endpoint.ListenUri));
                Console.WriteLine("Please enter to exit");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception oEx)
            {
                Console.WriteLine(oEx.ToString());
            }
        }
    }
}
