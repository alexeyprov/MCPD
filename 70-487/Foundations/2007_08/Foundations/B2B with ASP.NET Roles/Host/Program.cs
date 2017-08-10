//IDesign Inc. 2007 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceModel;
using MyNamespace;
using System.Diagnostics;
using System.Web.Security;
using System.ServiceModel.Security;

namespace Host
{
   static class Program
   {
      static void Main()
      {
         ServiceHost host = new ServiceHost(typeof(MyService),new Uri("http://localhost:8000/"));
         host.Open();

         Application.EnableVisualStyles();
         Application.Run(new HostForm());

         host.Close();
      }
   }
}