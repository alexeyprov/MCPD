using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Rashim.Discovery.Announcement.Common
{
    [ServiceContract]
    public interface IMessageServices
    {
        [OperationContract]
        string GetMessage(string submittedMessages);
    }
}
