using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace Library
{
    // message contract version
    [MessageContract(IsWrapped=false)]
    public class AddCustomerRequest
    {
        [MessageHeader(Name="ContextId", Namespace="http://example.org/customHeaders")]
        public Guid ContextId;

        [MessageBodyMember]
        public Customer customer;
    }

    [ServiceContract]
    public interface ICustomer3
    {
        [OperationContract(Action = "urn:add-customer")]
        void AddCustomer(AddCustomerRequest request);
    }
}
