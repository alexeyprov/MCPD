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

    public interface IMessageReceiverAdapter
    {
        IAsyncResult BeginReceive(TimeSpan serverWaitTime, AsyncCallback callback, object state);

        IBrokeredMessageAdapter EndReceive(IAsyncResult result);
    }
}