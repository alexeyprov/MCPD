// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using ServiceModelEx.ServiceBus;
using System.Windows.Forms;

class Program
{
   static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(MyService));
      host.SetServiceBusPassword("123@abc");

      host.Open();

      Application.Run(new HostForm());
      
      host.Close();
   }
}
