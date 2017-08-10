using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.ServiceModel.Discovery;
using Rashim.RND.AdhocDiscovery.Services;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Rashim.RND.AdhocDiscovery.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            var findCriteria = FindCriteria.CreateMetadataExchangeEndpointCriteria(typeof(IMessageService));
            findCriteria.MaxResults = 1;
            Debug.Assert(findCriteria.Scopes != null, "findCriteria.Scopes != null");
            findCriteria.Scopes.Add(new Uri("ldap:///ou=people,o=rashim"));
            var findResponse = discoveryClient.Find(findCriteria);

            if (findResponse != null)
            {
                if(findResponse.Endpoints!=null)
                {
                    if (findResponse.Endpoints.Count > 0)
                    {
                        var endpoints = MetadataResolver.Resolve(typeof (IMessageService),
                                                                findResponse.Endpoints[0].Address);
                        var factory = new ChannelFactory<IMessageService>(endpoints[0].Binding, endpoints[0].Address);
                        var channel = factory.CreateChannel();

                        Console.WriteLine("Say something and press enter");
                        var input = Console.ReadLine();
                        while (input != null && input.ToString(CultureInfo.InvariantCulture).ToLower()!="exit")
                        {
                            Console.WriteLine(channel.GetMessage(input));
                            input = Console.ReadLine();
                        }
                          ((IChannel)channel).Close();                    
                    }
                }
            }
            else
            {
                Console.WriteLine("Could not find the Services");
            }                    
        }
    }
}
