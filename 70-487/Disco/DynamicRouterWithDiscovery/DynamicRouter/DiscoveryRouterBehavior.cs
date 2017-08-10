using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DynamicRouter
{
   /// <summary>
   /// Add the discovery entension to the router.
   /// </summary>
   public class DiscoveryRouterBehavior : BehaviorExtensionElement, IServiceBehavior
   {
      public DiscoveryRouterBehavior()
      {

      }
      void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
      {

      }

      void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
      {
         ServiceDiscoveryExtension discoveryExtension = new ServiceDiscoveryExtension();
         serviceHostBase.Extensions.Add(discoveryExtension);
      }
      
      void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
      {

      }


      public override Type BehaviorType
      {
         get { return typeof(DiscoveryRouterBehavior); }
      }

      protected override object CreateBehavior()
      {
         return new DiscoveryRouterBehavior();
      }
   }

}