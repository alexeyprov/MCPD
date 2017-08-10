// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Windows.Forms;
using ServiceModelEx.ServiceBus;

class Program
{
   static void Main()
   {
      Uri bufferAddress = new Uri(@"https://myservicenamespace.servicebus.windows.net/MyBuffer");
      string secret = "**********  Enter Your Secret Here  **********";

      ServiceHost host = new BufferedServiceBusHost<MyService>(secret,bufferAddress);

      host.Open();

      Application.Run(new HostForm());

      host.Close();
   }
}
