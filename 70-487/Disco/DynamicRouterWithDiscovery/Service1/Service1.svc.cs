using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Service.Api;
using System.Diagnostics;

namespace Service1
{   
   //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, 
   //   InstanceContextMode=InstanceContextMode.PerCall)]
   public class Service1 : IService
   {
      public string DoWork(string value)
      {
         Trace.WriteLine("Called From Service 1");
         return string.Format("You entered: {0}", value);
      }
   }
}
