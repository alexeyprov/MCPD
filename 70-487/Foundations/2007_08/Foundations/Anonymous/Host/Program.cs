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
using ServiceModelEx;
using System.Security.Cryptography.X509Certificates;

namespace Host
{
   static class Program
   {
      static void Main()
      {
         ServiceHost host = new ServiceHost(typeof(MyService),new Uri("http://localhost:8000/"));
         
         //SecurityBehavior securityBehavior = new SecurityBehavior(ServiceSecurity.Anonymous,"MyServiceCert");
         //host.Description.Behaviors.Add(securityBehavior);


         //ServiceHost<MyService> host = new ServiceHost<MyService>(new Uri("http://localhost:8000/"));
         //host.SetSecurityBehavior(ServiceSecurity.Anonymous,"MyServiceCert");

         host.Open();

         Application.Run(new HostForm());

         host.Close();
      }
   }
}