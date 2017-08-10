// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;

class Program
{
   static void Main()
   {
      string password = "MyPassword";

      ServiceBusHost host = new ServiceBusHost(typeof(MyService));
      host.SetServiceBusPassword(password);
      host.ConfigureMessageSecurity("MyServiceCert",false,null);

      host.Open();

      Console.WriteLine("Press ENTER to shut down service.");
      Console.ReadLine();
      
      host.Close();
   }
}
