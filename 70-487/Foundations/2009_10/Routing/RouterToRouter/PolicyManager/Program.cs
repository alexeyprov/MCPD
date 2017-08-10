using System;
using Microsoft.ServiceBus;
using System.Threading;
using System.ServiceModel;

class Program
{
   static void Main()
   {
      Console.WriteLine("Creaing routers......");

      Uri topRouterAddress = new Uri(@"sb://MySolution.servicebus.windows.net/MyTopRouter/");
      Uri subRouterAddress = new Uri(@"sb://MySolution.servicebus.windows.net/MySubRouter/");

      string solutionName = "MySolution";
      string solutionPassword = "MyPassword";
      EventWaitHandle handle = new EventWaitHandle(false,EventResetMode.ManualReset,"RouterToRouter");

      TransportClientEndpointBehavior serviceBusCredential = new TransportClientEndpointBehavior();
      serviceBusCredential.CredentialType = TransportClientCredentialType.UserNamePassword;
      serviceBusCredential.Credentials.UserName.UserName = solutionName;
      serviceBusCredential.Credentials.UserName.Password = solutionPassword;


      RouterPolicy topRouterPolicy = new RouterPolicy();
      topRouterPolicy.ExpirationInstant = DateTime.UtcNow + TimeSpan.FromMinutes(5);
      topRouterPolicy.MaxSubscribers = 4;
      topRouterPolicy.MessageDistribution = MessageDistributionPolicy.AllSubscribers;
      topRouterPolicy.Discoverability = DiscoverabilityPolicy.Public;
      topRouterPolicy.TransportProtection = TransportProtectionPolicy.None;

      //Create main router:
      if(RouterExists(serviceBusCredential,topRouterAddress))
      {
         RouterManagementClient.DeleteRouter(serviceBusCredential,topRouterAddress);
      }
      RouterClient mainRouterClient = RouterManagementClient.CreateRouter(serviceBusCredential,topRouterAddress,topRouterPolicy);
      
            
      RouterPolicy subRouterPolicy = new RouterPolicy();
      subRouterPolicy.ExpirationInstant = DateTime.UtcNow + TimeSpan.FromMinutes(5);
      subRouterPolicy.MaxSubscribers = 4;
      subRouterPolicy.MessageDistribution = MessageDistributionPolicy.OneSubscriber;
      subRouterPolicy.Discoverability = DiscoverabilityPolicy.Public;
      subRouterPolicy.TransportProtection = TransportProtectionPolicy.None;

      //Create sub-router:
      if(RouterExists(serviceBusCredential,subRouterAddress))
      {
         RouterManagementClient.DeleteRouter(serviceBusCredential,subRouterAddress);
      }
      RouterClient subRouterClient = RouterManagementClient.CreateRouter(serviceBusCredential,subRouterAddress,subRouterPolicy);
      

      //The heart of this demo
      RouterSubscriptionClient subscription = subRouterClient.SubscribeToRouter(mainRouterClient,TimeSpan.MaxValue);

      
      handle.Set();

      Console.WriteLine("Routers are ready");
      Console.ReadLine();

      //Just cleaner, not required
      subscription.Unsubscribe(serviceBusCredential);

      handle.Close();
   }
   static bool RouterExists(TransportClientEndpointBehavior serviceBusCredential,Uri routerUri)
   {
      try
      {
         RouterClient client = RouterManagementClient.GetRouter(serviceBusCredential,routerUri);
         client.GetPolicy();
         return true;
      }
      catch(EndpointNotFoundException)
      {}
      return false;
   }
}
