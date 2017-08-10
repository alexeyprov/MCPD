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

    public class MessageReceiverAdapter : IMessageReceiverAdapter
    {
        private readonly MessageReceiver receiver;

        public MessageReceiverAdapter(MessageReceiver receiver)
        {
            this.receiver = receiver;
        }

        public IAsyncResult BeginReceive(TimeSpan serverWaitTime, AsyncCallback callback, object state)
        {
            return this.receiver.BeginReceive(serverWaitTime, callback, state);
        }

        public IBrokeredMessageAdapter EndReceive(IAsyncResult result)
        {
            var message = this.receiver.EndReceive(result);
            if (message != null)
            {
                return new BrokeredMessageAdapter(message);
            }

            return null;
        }
    }
}