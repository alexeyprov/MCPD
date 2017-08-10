//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers.Stores
{
    using System;
    using System.Collections.Generic;
    using Orders.Workers.Models;

    public interface IProcessStatusStore
    {
        Guid LockOrders(string roleInstanceId);
        void SendComplete(Guid orderId, string transportPartner);
        void UpdateWithError(Exception exception, Guid orderId);
        IEnumerable<OrderProcessStatus> GetLockedOrders(string roleInstanceId, Guid batchId);
        OrderProcessStatus GetByOrderId(Guid orderId);
    }
}
