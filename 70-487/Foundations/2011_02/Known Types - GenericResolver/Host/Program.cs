//2011 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using ServiceModelEx;


static class Program
{
   static void Main()
   {
      ServiceHost host = new ServiceHost(typeof(ContactManager));
      host.AddGenericResolver();

      //ServiceHost host = new ServiceHost<ContactManager>();
 
      host.Open();
      
      Application.Run(new HostForm());

      host.Close();
   }
}