using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Service.Api;
using System.Diagnostics;

namespace Service2
{
   // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
   public class Service2 : IService
   {
      public string DoWork(string value)
      {
         Trace.WriteLine("Called From Service 2");
         return string.Format("You entered: {0}", value);
      }
   }
}
