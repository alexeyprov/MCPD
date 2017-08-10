// © 2010 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Messaging;
using System.ServiceModel;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ServiceModelEx
{
   public class ServiceHost<T> : ServiceHost
   {
      public ServiceHost() : base(typeof(T))
      {}
      public ServiceHost(params string[] baseAddresses) : base(typeof(T),baseAddresses.Select((address)=>new Uri(address)).ToArray())
      {}
      public ServiceHost(params Uri[] baseAddresses) : base(typeof(T),baseAddresses)
      {}
      public ServiceHost(T singleton,params string[] baseAddresses) : base(singleton,baseAddresses.Select((address)=>new Uri(address)).ToArray())
      {}
      public ServiceHost(T singleton) : base(singleton)
      {}
      public ServiceHost(T singleton,params Uri[] baseAddresses) : base(singleton,baseAddresses)
      {}
      public virtual T Singleton
      {
         get
         {
            if(SingletonInstance == null)
            {
               return default(T);
            }
            Debug.Assert(SingletonInstance is T);
            return (T)SingletonInstance;
         }
      }
   }
}





