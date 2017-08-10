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

    public interface IMessageSenderAdapter
    {
        IAsyncResult BeginSend(IBrokeredMessageAdapter message, AsyncCallback callback, object state);

        void EndSend(IAsyncResult result);
    }
}