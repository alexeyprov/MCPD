﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Service.Api
{
   [ServiceContract]
   public interface IService
   {
      [OperationContract]
      string DoWork(string value);
   }
}
