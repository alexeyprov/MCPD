//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Threading;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;

class Program
{
   static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(MyServiceB));
      host.SetServiceBusPassword("MyPassword");

      //Wait for the router policy to be in place
      bool routerReady = false;
      string address = host.Description.Endpoints[0].Address.Uri.AbsoluteUri;

      while(routerReady == false)
      {
         try
         {
            EventWaitHandle.OpenExisting(address).WaitOne();
            routerReady = true;
         }
         catch
         {}
      }     
      
      
      host.Open();

      Application.Run(new HostForm());

      host.Close();
   }
}
