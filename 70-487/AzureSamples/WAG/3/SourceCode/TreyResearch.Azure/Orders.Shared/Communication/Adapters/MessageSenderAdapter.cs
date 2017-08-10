//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Shared.Communication.Adapters
{
    using System;
    using Microsoft.ServiceBus.Messaging;

    public class MessageSenderAdapter : IMessageSenderAdapter
    {
        private readonly MessageSender sender;

        public MessageSenderAdapter(MessageSender sender)
        {
            this.sender = sender;
        }

        public IAsyncResult BeginSend(IBrokeredMessageAdapter message, AsyncCallback callback, object state)
        {
            Guard.CheckArgumentNull(message, "message");

            return this.sender.BeginSend((BrokeredMessage)message.GetAdaptee(), callback, state);
        }

        public void EndSend(IAsyncResult result)
        {
            this.sender.EndSend(result);
        }
    }
}