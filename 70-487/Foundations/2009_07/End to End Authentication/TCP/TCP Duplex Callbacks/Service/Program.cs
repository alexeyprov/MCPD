// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;

class Program
{
   static void Main()
   {
      ServiceBusHost host = new ServiceBusHost(typeof(MyService));
      host.SetServiceBusPassword("MyPassword");
      host.ConfigureAnonymousMessageSecurity("MyServiceCert");

      HostForm form = new HostForm();

      host.Open();

      Application.Run(form);
       
      host.Close();
   }
}
