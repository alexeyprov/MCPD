using System;
using System.Collections.Generic;
using System.Text;

namespace ChatLibrary
{
    public class ChatService : IChat
    {
        #region IChatService Members

        public void SendMessage(ChatMessage msg)
        {
            Console.WriteLine("{0} - {1} says {2}",
                msg.Timestamp, msg.Username, msg.Text);
        }

        #endregion
    }
}
