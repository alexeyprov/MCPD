using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Routing;

namespace DynamicRouter
{
   //This class is a custom derivative of ServiceHost
   //that can automatically enabled metadata generation
   //for any service it hosts.
   class RouterServiceHost : ServiceHost
   {

      public RouterServiceHost(Type serviceType, params Uri[] baseAddresses)
         : base(serviceType, baseAddresses)
      {
        
      }

      //Overriding ApplyConfiguration() allows us to 
      //alter the ServiceDescription prior to opening
      //the service host. 
      protected override void ApplyConfiguration()
      {
         //First, we call base.ApplyConfiguration()
         //to read any configuration that was provided for
         //the service we're hosting. After this call,
         //this.ServiceDescription describes the service
         //as it was configured.
         base.ApplyConfiguration();

         // add the routing configuration 
         Description.Behaviors.Add(new RoutingBehavior(new RoutingConfiguration()));
         
         // add the discovery behavior
         Description.Behaviors.Add(new DiscoveryRouterBehavior());
      }
   }
}
