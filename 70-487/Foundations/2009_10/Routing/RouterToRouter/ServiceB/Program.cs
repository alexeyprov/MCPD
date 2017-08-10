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
      //Wait for the router policy to be in place
      bool routerReady = false;

      while(routerReady == false)
      {
         try
         {
            EventWaitHandle.OpenExisting("RouterToRouter").WaitOne();
            routerReady = true;
         }
         catch
         {}
      }   
  
      ServiceHost host = new ServiceHost(typeof(MyServiceB));
      host.SetServiceBusPassword("MyPassword");
      
      host.Open();

      Application.Run(new HostForm());

      host.Close();
   }
}