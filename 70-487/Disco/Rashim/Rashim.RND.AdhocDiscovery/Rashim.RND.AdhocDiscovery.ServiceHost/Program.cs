using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rashim.RND.AdhocDiscovery.Services;

namespace Rashim.RND.AdhocDiscovery.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {           
            var host = new System.ServiceModel.ServiceHost(typeof(MessageService));
            host.Open();
            host.Description.Endpoints.ToList().ForEach((endpoint)=> Console.WriteLine(endpoint.ListenUri));            
            Console.WriteLine("Please enter to exit");
            Console.ReadLine();
            host.Close();
        }
    }
}
