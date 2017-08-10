//2009 IDesign Inc. 
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

      HostForm form = new HostForm();

      host.Open();

      Application.Run(form);
       
      host.Close();
   }
}
