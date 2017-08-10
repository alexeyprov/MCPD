using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Service.Api;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace TestDynamicRouter.cs
{
   public class ServiceProxy : ClientBase<IService>, IService
   {
      public ServiceProxy(Binding binding, EndpointAddress address)
         : base(binding, address)         
      {

      }

      public string DoWork(string work)
      {
         return Channel.DoWork(work);
      }
   }
}
