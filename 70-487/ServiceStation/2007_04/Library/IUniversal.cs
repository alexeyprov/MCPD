using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Library
{
    [ServiceContract]
    public interface IUniversalOneWay
    {
        [OperationContract(Action = "*")]
        void ProcessMessage(Message msg);
    }
    [ServiceContract]
    public interface IUniversalRequestReply
    {
        [OperationContract(Action = "*", ReplyAction = "*")]
        Message ProcessMessage(Message msg);
    }
}
