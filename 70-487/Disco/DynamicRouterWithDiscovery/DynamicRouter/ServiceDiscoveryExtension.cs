using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Routing;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Discovery;

namespace DynamicRouter
{
   internal class ServiceDiscoveryExtension : IExtension<ServiceHostBase>, IDisposable
   {
      private ServiceHostBase owner;
      private RoutingConfiguration mRouterConfiguration = new RoutingConfiguration();
      private List<ServiceEndpoint>  mEndpoints = null;


      public ServiceDiscoveryExtension()
      {
         // holds the list of endpoints
         mEndpoints = new List<ServiceEndpoint>();
         mRouterConfiguration.FilterTable.Add(new MatchAllMessageFilter(), mEndpoints);
      }
      void IExtension<ServiceHostBase>.Attach(ServiceHostBase owner)
      {
         this.owner = owner;
         PopulateFromManagedDiscovery();         
         ListenToAnnouncements();         
      }

      void IExtension<ServiceHostBase>.Detach(ServiceHostBase owner)
      {
         this.Dispose();
      }

      public void Dispose()
      {
      }

      /// <summary>
      /// Initialize the routing table based on managed discovery
      /// </summary>
      private void PopulateFromManagedDiscovery()
      {
         // Create a DiscoveryEndpoint that points to the DiscoveryProxy
         Uri probeEndpointAddress = new Uri("net.tcp://localhost/DiscoveryProxy/DiscoveryProxy.svc");
         
         var binding = new NetTcpBinding(SecurityMode.None);

         DiscoveryEndpoint discoveryEndpoint = new DiscoveryEndpoint(binding, new EndpointAddress(probeEndpointAddress));

         DiscoveryClient discoveryClient = new DiscoveryClient(discoveryEndpoint);
         var results = discoveryClient.Find(new FindCriteria(typeof(Service.Api.IService)));

         // add these endpoint to the router table.
         foreach (var endpoint in results.Endpoints)
         {
            AddEndpointToRoutingTable(endpoint);
         }
      }

      /// <summary>
      /// Update the routing table based on UDB announcement
      /// </summary>
      private void ListenToAnnouncements()
      {

         AnnouncementService announcementService = new AnnouncementService();

         // Subscribe to the announcement events
         
         announcementService.OnlineAnnouncementReceived += new EventHandler<AnnouncementEventArgs>(ServiceOnlineEvent);
         announcementService.OfflineAnnouncementReceived += new EventHandler<AnnouncementEventArgs>(ServiceOffLineEvent);

         // Host the AnnouncementService
         ServiceHost announcementServiceHost = new ServiceHost(announcementService);

         try
         {
            // Listen for the announcements sent over UDP multicast
            announcementServiceHost.AddServiceEndpoint(new UdpAnnouncementEndpoint());
            announcementServiceHost.Open();
         }
         catch (CommunicationException communicationException)
         {
            throw new FaultException("Can't listen to notification of services " + communicationException.Message);
         }
         catch (TimeoutException timeoutException)
         {
            throw new FaultException("Timeout trying to open the notification service " + timeoutException.Message);
         }

      }

      /// <summary>
      /// Fires when a service is online
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ServiceOffLineEvent(object sender, AnnouncementEventArgs e)
      {
         // service went offline, remove it from the routing table.
         Trace("Endpint offline detected: {0}", e.EndpointDiscoveryMetadata.Address);
         RemoveEndpointFromRoutingTable(e.EndpointDiscoveryMetadata);
      }

      /// <summary>
      /// Fires when a service goes offline
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private void ServiceOnlineEvent(object sender, AnnouncementEventArgs e)
      {
         // a service is added, add it to the router table.
         Trace("Endpint online detected: {0}", e.EndpointDiscoveryMetadata.Address);
         AddEndpointToRoutingTable(e.EndpointDiscoveryMetadata);  
       
      }

      private void AddEndpointToRoutingTable(EndpointDiscoveryMetadata endpointMetadata)
      {

         // set the address, for now all bindings are wsHttp
         WSHttpBinding binding = new WSHttpBinding();
         binding.Security.Mode = SecurityMode.None;

         // set the address
         EndpointAddress address = endpointMetadata.Address;

         // set the contract
         var contract = ContractDescription.GetContract(typeof(IRequestReplyRouter));

         ServiceEndpoint endpoint = new ServiceEndpoint(contract, binding, address);

         
         mEndpoints.Add(endpoint);

         mRouterConfiguration.FilterTable.Clear();
         mRouterConfiguration.FilterTable.Add(new MatchAllMessageFilter(), new RoundRobinList<ServiceEndpoint>(mEndpoints)); 

         this.owner.Extensions.Find<RoutingExtension>().ApplyConfiguration(mRouterConfiguration);

         Trace("Endpint added: {0}", endpointMetadata.Address);
      }

      private void RemoveEndpointFromRoutingTable(EndpointDiscoveryMetadata endpointMetadata)
      {
         // a service is going offline, take it out of the routing table.
         var foundEndpoint = mEndpoints.Find(e => e.Address == endpointMetadata.Address);
         if (foundEndpoint != null)
         {
            Trace("Endpint removed: {0}", endpointMetadata.Address);
            mEndpoints.Remove(foundEndpoint);
         }

         mRouterConfiguration.FilterTable.Clear();
         mRouterConfiguration.FilterTable.Add(new MatchAllMessageFilter(), new RoundRobinList<ServiceEndpoint>(mEndpoints)); 

         this.owner.Extensions.Find<RoutingExtension>().ApplyConfiguration(mRouterConfiguration);         
      }

      private void Trace(string msg, params object[] args)
      {
         System.Diagnostics.Trace.WriteLine(String.Format(msg, args));
      }
   }



}


