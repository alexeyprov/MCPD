//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.ServiceModel.Description;

namespace ServiceModelEx
{
   static class Program
   {
      static void Main()
      {
         string solution = "MySolution";
         string password = "MyPassword";
         
         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();
         credential.CredentialType = TransportClientCredentialType.UserNamePassword;
         credential.Credentials.UserName.UserName = solution;
         credential.Credentials.UserName.Password = password;

         ServiceRegistrySettings registeryBehavior = new ServiceRegistrySettings(DiscoveryType.Public);
         ////////////////////////////////////////////////////////////////////////////
         Console.WriteLine("Creating simple services...");

         ServiceHost host1 = new ServiceHost(typeof(MyService));
         host1.AddServiceEndpoint(typeof(IMyContract),new NetTcpRelayBinding(),@"sb://MySolution.servicebus.windows.net/MyService1");
         host1.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host1.SetServiceBusPassword(password);
         host1.Open();

         ServiceHost host2 = new ServiceHost(typeof(MyService));
         host2.AddServiceEndpoint(typeof(IMyContract),new NetTcpRelayBinding(),@"sb://MySolution.servicebus.windows.net/MyService2");
         host2.AddServiceEndpoint(typeof(IMyContract),new NetTcpRelayBinding(),@"sb://MySolution.servicebus.windows.net/MyService3");
         host2.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host2.SetServiceBusPassword(password);
         host2.Open();

         ////////////////////////////////////////////////////////////////////////////

         Console.WriteLine("Creating router and subscribers...");
               
         Uri routerAddress1 = new Uri(@"sb://MySolution.servicebus.windows.net/MyRouter/");
         if(RouterExists(credential,routerAddress1))
         {
            RouterManagementClient.DeleteRouter(credential,routerAddress1);
         }
         RouterPolicy routerPolicy = new RouterPolicy();

         routerPolicy.ExpirationInstant = DateTime.UtcNow + TimeSpan.FromMinutes(5);
         routerPolicy.MaxSubscribers = 50;
         routerPolicy.MessageDistribution = MessageDistributionPolicy.AllSubscribers;
         routerPolicy.Discoverability = DiscoverabilityPolicy.Public;
         RouterManagementClient.CreateRouter(credential,routerAddress1,routerPolicy);

         ServiceHost host3 = new ServiceHost(typeof(MyService));
         host3.AddServiceEndpoint(typeof(IMyContract),new NetOnewayRelayBinding(),routerAddress1);
         host3.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host3.Open();

         //host4 and host 5 do not work due to a bug in the service bus
         ServiceHost host4 = new ServiceHost(typeof(MyService));
         host4.AddServiceEndpoint(typeof(IMyContract),new NetOnewayRelayBinding(),routerAddress1);
         host4.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host4.SetServiceBusPassword(password);
         //host4.Open();

         ServiceHost host5 = new ServiceHost(typeof(MyService));
         host5.AddServiceEndpoint(typeof(IMyContract),new NetOnewayRelayBinding(),routerAddress1);
         host5.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host5.SetServiceBusPassword(password);
         //host5.Open();
                  
         ////////////////////////////////////////////////////////////////////////////

         Console.WriteLine("Creating router which subscribes to another router...");

         Uri topRouterAddress = new Uri(@"sb://MySolution.servicebus.windows.net/MyTopRouter/");
         if(RouterExists(credential,topRouterAddress))
         {
            RouterManagementClient.DeleteRouter(credential,topRouterAddress);
         }
         RouterClient topRouter = RouterManagementClient.CreateRouter(credential,topRouterAddress,routerPolicy);

         Uri subRouterAddress = new Uri(@"sb://MySolution.servicebus.windows.net/MySubRouter/");
         if(RouterExists(credential,subRouterAddress))
         {
            RouterManagementClient.DeleteRouter(credential,subRouterAddress);
         }
         RouterClient subRouter = RouterManagementClient.CreateRouter(credential,subRouterAddress,routerPolicy);
             
         subRouter.SubscribeToRouter(topRouter,TimeSpan.MaxValue);

         ServiceHost host6 = new ServiceHost(typeof(MyService));
         host6.AddServiceEndpoint(typeof(IMyContract),new NetOnewayRelayBinding(),topRouterAddress.AbsoluteUri);
         host6.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host6.Open();

         ServiceHost host7 = new ServiceHost(typeof(MyService));
         host7.AddServiceEndpoint(typeof(IMyContract),new NetOnewayRelayBinding(),topRouterAddress.AbsoluteUri,subRouterAddress);
         host7.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host7.Open();

         //host8 should work but does not - bug in the service bus
         ServiceHost host8 = new ServiceHost(typeof(MyService));
         host8.AddServiceEndpoint(typeof(IMyContract),new NetOnewayRelayBinding(),topRouterAddress,subRouterAddress);
         host8.Description.Endpoints[0].Behaviors.Add(registeryBehavior);
         host8.SetServiceBusPassword(password);
         //host8.Open();

         ////////////////////////////////////////////////////////////////////////////

         Console.WriteLine("Creating a queue...");

             
         Uri queueAddress1 = new Uri(@"sb://MySolution.servicebus.windows.net/MyQueue/");
         if(QueueExists(credential,queueAddress1))
         {
            QueueManagementClient.DeleteQueue(credential,queueAddress1);
         }
         QueuePolicy queuePolicy = new QueuePolicy();

         queuePolicy.ExpirationInstant = DateTime.UtcNow + TimeSpan.FromMinutes(5);
         queuePolicy.Discoverability = DiscoverabilityPolicy.Public;
         QueueManagementClient.CreateQueue(credential,queueAddress1,queuePolicy);


         ////////////////////////////////////////////////////////////////////////////

         Console.WriteLine("Creating a queue which subscribes to a router...");
             
         Uri queueAddress2 = new Uri(@"sb://MySolution.servicebus.windows.net/MySubQueue/");
         if(QueueExists(credential,queueAddress2))
         {
            QueueManagementClient.DeleteQueue(credential,queueAddress2);
         }
         QueueClient queueClient = QueueManagementClient.CreateQueue(credential,queueAddress2,queuePolicy);
         
         queueClient.SubscribeToRouter(topRouter,TimeSpan.MaxValue);

         ////////////////////////////////////////////////////////////////////////////
         Console.WriteLine();
         Console.WriteLine();

         Console.WriteLine("Press any key to close services and junctions");
         Console.ReadLine();

         host1.Close();
         host2.Close();
         host3.Close();
         //host4.Close();
         //host5.Close();
         host6.Close();
         host7.Close();
         //host8.Close();

         try
         {
            RouterManagementClient.DeleteRouter(credential,routerAddress1);
         }
         catch
         {}

         try
         {
            RouterManagementClient.DeleteRouter(credential,topRouterAddress);
         }
         catch
         {}  
       
         try
         {
            RouterManagementClient.DeleteRouter(credential,subRouterAddress);
         }
         catch
         {}       
  
         try
         {
            QueueManagementClient.DeleteQueue(credential,queueAddress1);
         }
         catch
         {}    
     
         try
         {
            QueueManagementClient.DeleteQueue(credential,queueAddress2);
         }
         catch
         {}     
      }

      static void SetServiceBusPassword(this ServiceHost host,string password)
      {
         string solution = host.Description.Endpoints[0].Address.Uri.Host.Split('.')[0];

         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();
         credential.CredentialType = TransportClientCredentialType.UserNamePassword;
         credential.Credentials.UserName.UserName = solution;
         credential.Credentials.UserName.Password = password;

         foreach(ServiceEndpoint endpoint in host.Description.Endpoints)
         {
            endpoint.Behaviors.Add(credential);
         }
      }

      static bool RouterExists(TransportClientEndpointBehavior credential,Uri routerUri)
      {
         try
         {
            RouterClient client = RouterManagementClient.GetRouter(credential,routerUri);
            client.GetPolicy();
            return true;
         }
         catch(EndpointNotFoundException)
         {
         }
         return false;
      }

      static bool QueueExists(TransportClientEndpointBehavior credential,Uri queueUri)
      {
         try
         {
            QueueClient client = QueueManagementClient.GetQueue(credential,queueUri);
            client.GetPolicy();
            return true;
         }
         catch(EndpointNotFoundException)
         {
         }
         return false;
      }
   }
}
