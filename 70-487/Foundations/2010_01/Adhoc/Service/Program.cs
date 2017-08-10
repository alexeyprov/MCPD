// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Diagnostics;
using ServiceModelEx;


class Program
{
   public static void Main()
   {
      Uri baseAddress = DiscoveryHelper.AvailableTcpBaseAddress;

      ServiceHost host = new ServiceHost(typeof(MyService),baseAddress);
      host.AddDefaultEndpoints();
      host.Open();

      //Can do blocking calls:
      Console.WriteLine("Press ENTER to shut down service.");
      Console.ReadLine();
      
      host.Close();
   }
}






