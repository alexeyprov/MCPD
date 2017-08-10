using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rashim.Discovery.Announcement.Common;

namespace Rashim.Discovery.Announcement.Server
{
    public class MessageService :IMessageServices
    {
        public string GetMessage(string submittedMessages)
        {
            if(!string.IsNullOrEmpty(submittedMessages))
            {
                return "Reply from server :" + submittedMessages;
            }
            return string.Empty;
        }
    }
}
