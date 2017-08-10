//2007 IDesign Inc. 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;
using ServiceModelEx;

namespace Host
{
   static class Program
   {
      static void Main()
      {
         ServiceHost host = new ServiceHost(typeof(MyCalculator));
         foreach(ServiceEndpoint endpoint in host.Description.Endpoints)
         {
            QueuedServiceHelper.VerifyQueue(endpoint);
         }

         host.Open();

         Application.Run(new HostForm());

         host.Close();
      }
   }
}