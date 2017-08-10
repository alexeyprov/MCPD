// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;
using System.ServiceModel.Description;


class Program
{
   public static void Main()
   {
      ServiceHost host = new DiscoverableServiceHost(typeof(MyService));
      
      //Address is dynamic
      host.AddServiceEndpoint(typeof(IMyContract),new NetTcpRelayBinding(),"sb://MyNamespace.servicebus.windows.net/MyService/" + Guid.NewGuid()); 
      host.AddServiceEndpoint(typeof(IMetadataExchange),new NetTcpRelayBinding(),"sb://MyNamespace.servicebus.windows.net/MEX/" + Guid.NewGuid());

      host.SetServiceBusCredentials("E7IstfL+9E...UYzdFFkZTW4=");

      host.Open();

      //Can do blocking calls:
      Console.WriteLine("Press ENTER to shut down service.");
      Console.ReadLine();
      
      host.Close();
   }
}






