// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;
using Microsoft.ServiceBus;

class Program
{
   public static void Main()
   {
      ServiceHost host = new DiscoverableServiceHost(typeof(MyService));

      //Address is dynamic
      host.AddServiceEndpoint(typeof(IMyContract),new NetTcpRelayBinding(),"sb://MyNamespace.servicebus.windows.net/MyService/" + Guid.NewGuid()); 
      host.SetServiceBusCredentials("E7Ist...kZTW4=");

      host.Open();

      Application.Run(new HostForm());
      
      host.Close();
   }
}






