using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Xml;
using Rashim.Discovery.Announcement.Common;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace Rashim.Discovery.Announcement.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var announcementService = new AnnouncementService();

            announcementService.OnlineAnnouncementReceived += (sender, e) =>
            {
                var mexContractDescrition = ContractDescription.GetContract(typeof(IMetadataExchange));
                var mexQualifiedName = new XmlQualifiedName(mexContractDescrition.Name, mexContractDescrition.Namespace);

                e.EndpointDiscoveryMetadata.ContractTypeNames.ToList().ForEach((name) =>
                                        {
                                            if (mexQualifiedName != name) return;
                                            
                                            var endpoints = MetadataResolver.Resolve(typeof(IMessageServices), e.EndpointDiscoveryMetadata.Address);
                                            
                                            if (endpoints.Count <= 0) return;
                                            
                                            var factory = new ChannelFactory<IMessageServices>(endpoints[0].Binding, endpoints[0].Address);
                                            var channel = factory.CreateChannel();
                                            
                                            Console.WriteLine("\nService is online now..");
                                            var replyMessage = channel.GetMessage("Server is responding");
                                            Console.WriteLine(replyMessage);
                                            ((ICommunicationObject)channel).Close();
                                        });
            };

            announcementService.OfflineAnnouncementReceived += (sender, e) =>
            {                
                if (e.EndpointDiscoveryMetadata.ContractTypeNames.FirstOrDefault(contract => contract.Name == typeof(IMessageServices).Name) == null) return;

                Console.WriteLine("\nService says 'Good Bye'");
            };

            using (var announecementServiceHost = new ServiceHost(announcementService))
            {
                announecementServiceHost.AddServiceEndpoint(new UdpAnnouncementEndpoint());
                announecementServiceHost.Open();
                Console.WriteLine("Please enter to exit\n\n");
                Console.ReadLine();                             
            }
        }        
    }
}
