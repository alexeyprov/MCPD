//2008 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Windows.Forms;
using System.ServiceModel;

static class Program
{
   static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(MyService));
      host.Open();

      Application.Run(new HostForm());

      host.Close();
   }
}