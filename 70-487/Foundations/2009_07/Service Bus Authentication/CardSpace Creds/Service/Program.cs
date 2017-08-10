// 2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.Windows.Forms;

class Program
{
   static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(MyService));

      host.Open();
      
      Application.Run(new HostForm());
      
      host.Close();
   }
}
