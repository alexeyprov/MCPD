//2008 IDesign Inc. 
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
      AppDomainHost host = new AppDomainHost(typeof(MyService));
      host.Open();
     
      Application.Run(new HostForm());

      host.Close();
   }
}