using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChatLibrary
{
    [DataContract(Namespace="http://example.org/chat")]
    public class ChatMessage
    {
        [DataMember]
        public string Username;
        [DataMember]
        public DateTime Timestamp;
        [DataMember]
        public string Text;
    }
    [ServiceContract(Namespace="http://example.org/chat")]
    public interface IChat
    {
        [OperationContract(IsOneWay=true)]
        void SendMessage(ChatMessage msg);
    }
}
