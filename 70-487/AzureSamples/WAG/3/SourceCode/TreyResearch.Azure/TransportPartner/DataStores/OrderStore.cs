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
    using System.Linq;

    public class OrderStore : IOrderStore
    {
        private readonly IList<ActiveOrder> activeOrders;

        public OrderStore()
        {
            this.activeOrders = new List<ActiveOrder>();
        }

        public ActiveOrder GetById(Guid orderId)
        {
            return this.activeOrders.SingleOrDefault(o => o.OrderId == orderId);
        }

        public void Add(ActiveOrder order)
        {
            this.activeOrders.Add(order);
        }

        public void Remove(ActiveOrder order)
        {
            this.activeOrders.Remove(order);
        }

        public IEnumerable<ActiveOrder> FindAll()
        {
            return this.activeOrders;
        }
    }
}
