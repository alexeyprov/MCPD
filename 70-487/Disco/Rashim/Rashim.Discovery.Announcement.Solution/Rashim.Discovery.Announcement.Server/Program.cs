using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Rashim.Discovery.Announcement.Common;

namespace Rashim.Discovery.Announcement.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof (MessageService));
            host.Open();
            host.Description.Endpoints.ToList().ForEach((endpoint)=> Console.WriteLine(endpoint.ListenUri.ToString()));
            Console.WriteLine("Please Enter to Exit");
            Console.ReadLine();
            host.Close();
        }
    }
}
