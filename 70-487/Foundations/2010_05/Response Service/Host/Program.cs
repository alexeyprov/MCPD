// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel;
using ServiceModelEx.ServiceBus;

static class Program
{
   static void Main()
   {
      string secret = "**********  Enter Your Secret Here  **********";
      Uri serviceQeueue = new Uri(@"https://myservicenamespace.servicebus.windows.net/MyCalculatorQueue/");
      
      ServiceHost host = new BufferedServiceBusHost<MyCalculator>(secret,serviceQeueue);

      host.Open();

      Application.Run(new HostForm());

      host.Close();
   }
}