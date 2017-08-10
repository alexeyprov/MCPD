//2008 IDesign Inc.
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Collections.ObjectModel;

namespace ServiceModelEx
{
   [AttributeUsage(AttributeTargets.Class)]
   public class ThreadPoolBehaviorAttribute : Attribute,IContractBehavior,IServiceBehavior
   {
      string m_PoolName;
      uint m_PoolSize;
      Type m_ServiceType;

      protected string PoolName
      {
         get
         {
            return m_PoolName;
         }
         set
         {
            m_PoolName = value;
         }
      }
      protected uint PoolSize
      {
         get
         {
            return m_PoolSize;
         }
         set
         {
            m_PoolSize = value;
         }
      }
      protected Type ServiceType
      {
         get
         {
            return m_ServiceType;
         }
         set
         {
            m_ServiceType = value;
         }
      }

      public ThreadPoolBehaviorAttribute(uint poolSize,Type serviceType) : this(poolSize,serviceType,null)
      {}
      public ThreadPoolBehaviorAttribute(uint poolSize,Type serviceType,string poolName)
      {
         PoolName = poolName;
         ServiceType = serviceType;
         PoolSize = poolSize;
      }
      protected virtual ThreadPoolSynchronizer ProvideSynchronizer()
      {
         if(ThreadPoolHelper.HasSynchronizer(ServiceType) == false)
         {
            return new ThreadPoolSynchronizer(PoolSize,PoolName);
         }
         else
         {
            return ThreadPoolHelper.GetSynchronizer(ServiceType);
         }
      }
      void IContractBehavior.AddBindingParameters(ContractDescription description,ServiceEndpoint endpoint,BindingParameterCollection parameters)
      {}

      void IContractBehavior.ApplyClientBehavior(ContractDescription description,ServiceEndpoint endpoint,ClientRuntime proxy)
      {}

      void IContractBehavior.ApplyDispatchBehavior(ContractDescription description,ServiceEndpoint endpoint,DispatchRuntime dispatchRuntime)
      {
         PoolName = PoolName ?? "Pool executing endpoints of " + ServiceType;
         ThreadPoolHelper.ApplyDispatchBehavior(ProvideSynchronizer(),PoolSize,ServiceType,PoolName,dispatchRuntime);
      }
      void IContractBehavior.Validate(ContractDescription description,ServiceEndpoint endpoint)
      {}

      void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters)
      {}

      void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase)
      {}

      void IServiceBehavior.Validate(ServiceDescription description,ServiceHostBase serviceHostBase)
      {
         serviceHostBase.Closed += delegate
                                   {
                                      ThreadPoolHelper.CloseThreads(ServiceType);
                                   };
      }
   }
}