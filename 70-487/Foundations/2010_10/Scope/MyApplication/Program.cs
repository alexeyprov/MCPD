// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;


class Program
{
   public static void Main()
   {
      ServiceHost host = new DiscoverableServiceHost(typeof(MyService),new Uri("sb://MyNamespace.servicebus.windows.net/MyService/" + Guid.NewGuid()));
 
      host.SetServiceBusCredentials("E7IstfL...FkZTW4=");

      host.Open();

      //Can do blocking calls:
      Console.WriteLine("MyApplication");
      Console.ReadLine();
      
      host.Close();
   }
}






