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

    public interface IBrokeredMessageAdapter : IDisposable
    {
        int DeliveryCount { get; }
        string ReplyTo { get; set; }
        IDictionary<string, object> Properties { get; }
        string CorrelationId { get; set; }
        T GetBody<T>();
        IAsyncResult BeginAbandon(AsyncCallback callback, object state);
        void EndAbandon(IAsyncResult result);
        IAsyncResult BeginComplete(AsyncCallback callback, object state);
        void EndComplete(IAsyncResult result);
        IAsyncResult BeginDeadLetter(AsyncCallback callback, object state);
        void EndDeadLetter(IAsyncResult result);
        object GetAdaptee();
    }
}