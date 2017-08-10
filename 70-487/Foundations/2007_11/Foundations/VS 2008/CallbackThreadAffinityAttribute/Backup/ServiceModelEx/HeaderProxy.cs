//2008 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ServiceModelEx;
using System.Reflection;

namespace ServiceModelEx
{
   public abstract partial class HeaderClientBase<T,H> : ClientBase<T> where T : class
   {
      protected H Header;

      public HeaderClientBase() : this(default(H))
      {}

      public HeaderClientBase(string endpointConfigurationName) : this(default(H),endpointConfigurationName)
      {}

      public HeaderClientBase(string endpointConfigurationName,string remoteAddress) : this(default(H),endpointConfigurationName,remoteAddress)
      {}

      public HeaderClientBase(string endpointConfigurationName,EndpointAddress remoteAddress) : this(default(H),endpointConfigurationName,remoteAddress)
      {}

      public HeaderClientBase(Binding binding,EndpointAddress remoteAddress) : this(default(H),binding,remoteAddress)
      {}

      public HeaderClientBase(H header)
      {
         Header = header;
      }

      public HeaderClientBase(H header,string endpointConfigurationName) : base(endpointConfigurationName)
      {
         Header = header;
      }

      public HeaderClientBase(H header,string endpointConfigurationName,string remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {
         Header = header;
      }

      public HeaderClientBase(H header,string endpointConfigurationName,EndpointAddress remoteAddress) : base(endpointConfigurationName,remoteAddress)
      {
         Header = header;
      }

      public HeaderClientBase(H header,Binding binding,EndpointAddress remoteAddress) : base(binding,remoteAddress)
      {
         Header = header;
      }
     
      protected virtual object Invoke(string operation,params object[] args)
      {
         using(OperationContextScope contextScope = new OperationContextScope(InnerChannel))
         {
            GenericContext<H>.Current = new GenericContext<H>(Header);

            Type contract = typeof(T);
            MethodInfo methodInfo = contract.GetMethod(operation);//Does not support contract hierarchy or overloading 
            return methodInfo.Invoke(Channel,args);
        }
      }
   }
}