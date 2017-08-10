//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Timers;
using System.ServiceModel.Description;
using System.Collections.Generic;


namespace ServiceModelEx.ServiceBus
{
   public static partial class ServiceBusHelper
   {
      internal const float NearExpiration = 0.9F;
      const int MaxSubscribers = 50;

      //Router to router
      public static void SubscribeToRouter(this ServiceHost host,string subRouterAddress)
      {
         for(int index = 0;index < host.Description.Endpoints.Count;index++)
         {
            host.Description.Endpoints[index].ListenUri = new Uri(subRouterAddress);
         }
      }

      //Events management 
      public static RouterClient[] CreateEventRouters(string baseAddress,Type contractType)
      {
         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();

         return CreateEventRouters(baseAddress,contractType,credential);
      }
      public static RouterClient[] CreateEventRouters(string baseAddress,Type contractType,string password)
      {
         string solution = ExtractSolution(new Uri(baseAddress));

         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();
         credential.CredentialType = TransportClientCredentialType.UserNamePassword;
         credential.Credentials.UserName.UserName = solution;
         credential.Credentials.UserName.Password = password;

         return CreateEventRouters(baseAddress,contractType,credential);
      }
      public static RouterClient[] CreateEventRouters(string baseAddress,Type contractType,object findValue,StoreLocation location,StoreName storeName,X509FindType findType)
      {
         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();
         credential.CredentialType = TransportClientCredentialType.X509Certificate;
         credential.Credentials.ClientCertificate.SetCertificate(location,storeName,findType,findValue);

         return CreateEventRouters(baseAddress,contractType,credential);
      }
      public static RouterClient[] CreateEventRouters(string baseAddress,string subjectName,Type contractType) 
      {
         return CreateEventRouters(baseAddress,contractType,subjectName,StoreLocation.LocalMachine,StoreName.My,X509FindType.FindBySubjectName);
      }
      
      internal static RouterClient[] CreateEventRouters(string baseAddress,Type contractType,TransportClientEndpointBehavior credential)
      {
         Debug.Assert(contractType.IsInterface);
         Debug.Assert(contractType.GetCustomAttributes(typeof(ServiceContractAttribute),false).Length == 1);

         string[] operations = EventsHost.GetOperations(contractType);

         List<RouterClient> clients = new List<RouterClient>();

         foreach(string operationName in operations)
         {
            RouterClient client = CreateEventRouter(baseAddress + contractType + "/" + operationName+ "/",credential);
            clients.Add(client);
         }
         return clients.ToArray();
      }
      static RouterClient CreateEventRouter(string routerAddress,TransportClientEndpointBehavior credential)
      {
         if(routerAddress.EndsWith("/") == false)
         {
            routerAddress += "/";
         }         
         RouterPolicy policy = CreateEventRouterPolicy();

         return CreateRouter(routerAddress,policy,credential);
      }
      static RouterPolicy CreateEventRouterPolicy()
      {
         RouterPolicy policy = new RouterPolicy();

         //For events:
         policy.MaxSubscribers = MaxSubscribers;
         policy.MessageDistribution = MessageDistributionPolicy.AllSubscribers;
         policy.Discoverability = DiscoverabilityPolicy.Public;
         policy.TransportProtection = TransportProtectionPolicy.None;

         return policy;
      }

      //Load balancing
      public static void CreateLoadBalancingRouter(string balancerAddress)
      {
         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();

         CreateLoadBalancingRouter(balancerAddress,credential);
      }
      public static void CreateLoadBalancingRouter(string balancerAddress,string password)
      {
         string solution = ExtractSolution(new Uri(balancerAddress));

         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();
         credential.CredentialType = TransportClientCredentialType.UserNamePassword;
         credential.Credentials.UserName.UserName = solution;
         credential.Credentials.UserName.Password = password;

         CreateLoadBalancingRouter(balancerAddress,credential);
      }
      public static void CreateLoadBalancingRouter(string balancerAddress,object findValue,StoreLocation location,StoreName storeName,X509FindType findType)
      {
         TransportClientEndpointBehavior credential = new TransportClientEndpointBehavior();
         credential.CredentialType = TransportClientCredentialType.X509Certificate;
         credential.Credentials.ClientCertificate.SetCertificate(location,storeName,findType,findValue);

         CreateLoadBalancingRouter(balancerAddress,credential);
      }

      static void CreateLoadBalancingRouter(string balancerAddress,TransportClientEndpointBehavior credential)
      {
         RouterPolicy policy = CreateBalancerPolicy();
                  
         CreateRouter(balancerAddress,policy,credential);
      }
      static RouterPolicy CreateBalancerPolicy()
      {
         RouterPolicy policy = new RouterPolicy();
         policy.MaxSubscribers = MaxSubscribers;
         policy.Discoverability = DiscoverabilityPolicy.Public;

         return policy;
      }

      //Helpers
      internal static bool RouterExists(string routerAddress,TransportClientEndpointBehavior credential)
      {
         return RouterExists(new Uri(routerAddress),credential);
      }
      internal static bool RouterExists(Uri routerUri,TransportClientEndpointBehavior credential)
      {
         try
         {
            RouterClient client = RouterManagementClient.GetRouter(credential,routerUri);
            client.GetPolicy();
            return true;
         }
         catch(EndpointNotFoundException)
         {}
         return false;
      }
      static RouterClient CreateRouter(string routerAddress,RouterPolicy routerPolicy,TransportClientEndpointBehavior credential)
      {
         if(routerAddress.EndsWith("/") == false)
         {
            routerAddress += "/";
         }         
         
         Uri address = new Uri(routerAddress);

         if(RouterExists(address,credential))
         {
            //Verify a few properties
            RouterPolicy policy = RouterManagementClient.GetRouterPolicy(credential,address);

            Debug.Assert(policy.MaxSubscribers == routerPolicy.MaxSubscribers);
            Debug.Assert(policy.MessageDistribution == routerPolicy.MessageDistribution);
            Debug.Assert(policy.Overflow == routerPolicy.Overflow);
            Debug.Assert(policy.TransportProtection == routerPolicy.TransportProtection);

            return RouterManagementClient.GetRouter(credential,address);
         }  

         //Renew expiration as needed       
         TimeSpan lease = routerPolicy.ExpirationInstant - DateTime.UtcNow;
         Timer timer = new Timer(lease.TotalMilliseconds*NearExpiration);
         timer.Elapsed += (sender,args)=>
                          {
                             RouterManagementClient.RenewRouter(credential,address,lease);
                             timer.Stop();
                             timer.Start();
                          };
         timer.Start();
 
         return RouterManagementClient.CreateRouter(credential,address,routerPolicy);
      }
   }
}






