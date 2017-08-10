using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Library
{
    [ServiceContract]
    public interface ICustomer2
    {
        [OperationContract(Action = "urn:add-customer")]
        void AddCustomer(Customer request);
    }
}
