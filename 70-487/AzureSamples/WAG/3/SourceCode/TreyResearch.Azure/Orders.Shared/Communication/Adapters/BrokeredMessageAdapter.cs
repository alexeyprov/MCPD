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
    using System.Collections.Generic;
    using Microsoft.ServiceBus.Messaging;

    public class BrokeredMessageAdapter : IBrokeredMessageAdapter
    {
        private BrokeredMessage brokeredMessage;

        public BrokeredMessageAdapter(BrokeredMessage brokeredMessage)
        {
            this.brokeredMessage = brokeredMessage;
        }

        public int DeliveryCount
        {
            get { return this.brokeredMessage.DeliveryCount; }
        }

        public string ReplyTo
        {
            get { return this.brokeredMessage.ReplyTo; }
            set { this.brokeredMessage.ReplyTo = value; }
        }

        public IDictionary<string, object> Properties
        {
            get { return this.brokeredMessage.Properties; }
        }

        public string CorrelationId
        {
            get { return this.brokeredMessage.CorrelationId; }
            set { this.brokeredMessage.CorrelationId = value; }
        }

        public T GetBody<T>()
        {
            return this.brokeredMessage.GetBody<T>();
        }

        public IAsyncResult BeginAbandon(AsyncCallback callback, object state)
        {
            return this.brokeredMessage.BeginAbandon(callback, state);
        }

        public void EndAbandon(IAsyncResult result)
        {
            this.brokeredMessage.EndAbandon(result);
        }

        public IAsyncResult BeginComplete(AsyncCallback callback, object state)
        {
            return this.brokeredMessage.BeginComplete(callback, state);
        }

        public void EndComplete(IAsyncResult result)
        {
            this.brokeredMessage.EndComplete(result);
        }

        public IAsyncResult BeginDeadLetter(AsyncCallback callback, object state)
        {
            return this.brokeredMessage.BeginDeadLetter(callback, state);
        }

        public void EndDeadLetter(IAsyncResult result)
        {
            this.brokeredMessage.EndDeadLetter(result);
        }

        public object GetAdaptee()
        {
            return this.brokeredMessage;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.brokeredMessage != null)
                {
                    this.brokeredMessage.Dispose();
                    this.brokeredMessage = null;
                }
            }
        }
    }
}