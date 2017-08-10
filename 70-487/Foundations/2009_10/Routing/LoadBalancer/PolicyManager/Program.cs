using System;
using Microsoft.ServiceBus;
using System.Threading;
using System.ServiceModel;

class Program
{
   static void Main()
   {
      Uri routerAddress = new Uri(@"sb://MySolution.servicebus.windows.net/MyRouter/");
      string solutionName = "MySolution";
      string solutionPassword = "MyPassword";
      EventWaitHandle handle = new EventWaitHandle(false,EventResetMode.ManualReset,routerAddress.AbsoluteUri);

      TransportClientEndpointBehavior serviceBusCredential = new TransportClientEndpointBehavior();
      serviceBusCredential.CredentialType = TransportClientCredentialType.UserNamePassword;
      serviceBusCredential.Credentials.UserName.UserName = solutionName;
      serviceBusCredential.Credentials.UserName.Password = solutionPassword;

      if(RouterExists(serviceBusCredential,routerAddress))
      {
         RouterManagementClient.DeleteRouter(serviceBusCredential,routerAddress);
      }

      RouterPolicy routerPolicy = new RouterPolicy();
      routerPolicy.ExpirationInstant = DateTime.UtcNow + TimeSpan.FromMinutes(5);
      routerPolicy.TransportProtection = TransportProtectionPolicy.None;
      routerPolicy.MaxSubscribers = 4;
      routerPolicy.MessageDistribution = MessageDistributionPolicy.OneSubscriber;
      RouterManagementClient.CreateRouter(serviceBusCredential,routerAddress,routerPolicy);

      handle.Set();

      Console.WriteLine("Router is Ready");
      Console.ReadLine();

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