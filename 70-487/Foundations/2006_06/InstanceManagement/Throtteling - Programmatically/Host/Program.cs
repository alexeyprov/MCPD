//2006 IDesign Inc.  
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ServiceModel;
using MyNamespace;
using System.ServiceModel.Description;


namespace Host
{
   static class Program
   {
      static void Main()
      {
         ServiceHost host = new ServiceHost(typeof(MyService));
         //Make sure there is no throttle in the config file
         ServiceThrottlingBehavior throttle = host.Description.Behaviors.Find<ServiceThrottlingBehavior>();
         if(throttle == null)
         {
            throttle = new ServiceThrottlingBehavior();
            throttle.MaxConcurrentCalls   = 12;
            throttle.MaxConnections       = 34;
            throttle.MaxInstances         = 2;
            host.Description.Behaviors.Add(throttle);
         }

         host.Open();

         Application.EnableVisualStyles();
         Application.Run(new HostForm());

         host.Close();
      }
   }
}