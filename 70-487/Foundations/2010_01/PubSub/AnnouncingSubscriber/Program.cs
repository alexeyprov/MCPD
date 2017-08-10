// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.ServiceModel.Discovery;
using System.Threading;
using ServiceModelEx;

class Program
{
   public static void Main()
   {
      //For demo purpose - let the pub/sub service opne first
      Thread.Sleep(6000); 

      ServiceHost host = new ServiceHost(typeof(AnnouncingSubscriber));

      string address = DiscoveryHelper.AvailableTcpBaseAddress.AbsoluteUri + Guid.NewGuid();

      NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport,true);
      host.AddServiceEndpoint(typeof(IMyEvents),binding,address);

      ServiceDiscoveryBehavior behavior = new ServiceDiscoveryBehavior();
      behavior.AnnouncementEndpoints.Add(new UdpAnnouncementEndpoint());

      host.Description.Behaviors.Add(behavior);

      host.Open();

      Application.Run(new HostForm());
      
      host.Close();
   }
}




 




