using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Service.Api
{
   [DataContract]
   public class CompositeType
   {
      bool boolValue = true;
      string stringValue = "Hello ";

      [DataMember]
      public bool BoolValue
      {
         get { return boolValue; }
         set { boolValue = value; }
      }

      [DataMember]
      public string StringValue
      {
         get { return stringValue; }
         set { stringValue = value; }
      }
   }



}
