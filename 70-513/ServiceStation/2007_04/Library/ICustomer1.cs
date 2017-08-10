using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Library
{
    [ServiceContract]
    public interface ICustomer1
    {
        [OperationContract(Action = "urn:add-customer")]
        void AddCustomer(Message msg);
    }
}
