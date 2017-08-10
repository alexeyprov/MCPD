// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel;
using ServiceModelEx;

static class Program
{
   static void Main()
   {
      ServiceHost host = DiscoveryPublishService<IMyEvents>.CreateHost<MyPublishService>();
      host.Open();

      Application.Run(new HostForm());

      host.Close();
   }
}
