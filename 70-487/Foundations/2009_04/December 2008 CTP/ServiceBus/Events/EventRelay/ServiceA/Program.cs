//2009 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;


class Program
{
   static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(MyService));

      host.Open();

      Console.WriteLine("Service A listening...");
      Console.ReadLine();
      
      host.Close();
   }
}
