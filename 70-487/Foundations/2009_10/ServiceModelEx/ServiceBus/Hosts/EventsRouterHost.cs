//2009 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net


using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.ServiceBus;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;

namespace ServiceModelEx.ServiceBus
{
   public class EventsRouterHost : EventsHost
   {
      public EventsRouterHost(object singletonInstance,string baseAddress) : this(singletonInstance,new string[]{baseAddress})
      {}
      public EventsRouterHost(object singletonInstance,string[] baseAddresses) : base(singletonInstance,baseAddresses)
      {}
      public EventsRouterHost(Type serviceType,string baseAddress) : this(serviceType,new string[]{baseAddress})
      {}
      public EventsRouterHost(Type serviceType,string[] baseAddresses) : base(serviceType,baseAddresses)
      {}
 
      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void SetBinding(NetOnewayRelayBinding binding)
      {
         Debug.Assert(binding is NetEventRelayBinding == false);

         RelayBinding = binding;
      }

      [MethodImpl(MethodImplOptions.Synchronized)]
      public override void SetBinding(string bindingConfigName)
      {
         SetBinding(new NetOnewayRelayBinding(bindingConfigName));
      }
        
      protected override NetOnewayRelayBinding GetBinding()
      {
         return RelayBinding ?? new NetOnewayRelayBinding();
      }   
   }
}
