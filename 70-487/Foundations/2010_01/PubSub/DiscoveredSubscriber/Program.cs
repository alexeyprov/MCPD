// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.ServiceModel.Discovery;
using ServiceModelEx;


class Program
{
   public static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(DiscoveredSubscriber));

      string address = DiscoveryHelper.AvailableTcpBaseAddress.AbsoluteUri + Guid.NewGuid();

      NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport,true);
      host.AddServiceEndpoint(typeof(IMyEvents),binding,address);

      host.AddServiceEndpoint(new UdpDiscoveryEndpoint());


      ServiceDiscoveryBehavior behavior = new ServiceDiscoveryBehavior();
      host.Description.Behaviors.Add(behavior);

      host.Open();

      Application.Run(new HostForm());
      
      host.Close();
   }
}






