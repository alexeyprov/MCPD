using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace DynamicRouter
{
   public class DynamicRouterServiceHostFactory : ServiceHostFactory
   {
      protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
      {
         //All the custom factory does is return a new instance
         //of our custom host class. The bulk of the custom logic should
         //live in the custom host (as opposed to the factory) for maximum
         //reuse value.
         return new RouterServiceHost(serviceType, baseAddresses);
      }
   }

}