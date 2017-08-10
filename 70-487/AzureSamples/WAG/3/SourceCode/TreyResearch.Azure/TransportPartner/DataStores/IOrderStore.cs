//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TransportPartner.DataStores
{
    using System;
    using System.Collections.Generic;

    public interface IOrderStore
    {
        IEnumerable<ActiveOrder> FindAll();
        ActiveOrder GetById(Guid orderId);
        void Add(ActiveOrder order);
        void Remove(ActiveOrder order);
    }
}
