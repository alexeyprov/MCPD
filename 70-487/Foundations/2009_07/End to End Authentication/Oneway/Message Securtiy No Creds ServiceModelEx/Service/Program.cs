// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;


class Program
{
   static void Main()
   {
      ServiceBusHost host = new ServiceBusHost(typeof(MyService));
      host.SetServiceBusPassword("MyPassword");
      host.ConfigureAnonymousMessageSecurity("MyServiceCert");

      host.Open();

      Console.WriteLine("Press ENTER to shut down service.");
      Console.ReadLine();
      
      host.Close();
   }
}
