//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace TransportPartner.Connectivity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Orders.Shared.Communication;
    using TransportPartner.TransportServices;

    /// <summary>
    /// The Adatper component will serve as an abstraction between TreyResearch and the Transport partners that do not use ServiceBus to communicate.
    /// </summary>
    public class Adapter : OrderProcessor
    {
        private readonly IList<ActiveOrder> activeOrders;
        private readonly ITransportServiceWrapper transportServiceWrapper;

        public Adapter(
            string topicName,
            string subscriptionName,
            string transportPartnerDisplayName,
            string transportPartnerName,
            string key,
            ITransportServiceWrapper transportServiceWrapper,
            string acsPassword)
            : base(topicName, subscriptionName, transportPartnerDisplayName, transportPartnerName, key, acsPassword, transportPartnerDisplayName)
        {
            this.activeOrders = new List<ActiveOrder>();
            this.transportServiceWrapper = transportServiceWrapper;
        }

        public EventHandler<OrderProcessedEventArgs> OnOrderProcessed { get; set; }

        protected override Guid ProcessOrder(Orders.Shared.Communication.Messages.NewOrderMessage message, ServiceBusQueueDescription queueDescription)
        {
            var processedOrder = this.activeOrders.SingleOrDefault(o => o.OrderId == message.OrderId);

            if (processedOrder != null)
            {
                // This order has been received for processing more than once, and will be discarded.
                return Guid.Empty;
            }

            var activeOrder = new ActiveOrder
                {
                    OrderId = message.OrderId,
                    ShippingAddress = message.ShippingAddress,
                    Amount = message.Amount,
                    ReplyTo = queueDescription.QueueName,
                    ReplyToNamespace = queueDescription.Namespace,
                    SwtAcsNamespace = queueDescription.SwtAcsNamespace
                };

            this.activeOrders.Add(activeOrder);

            // Call the transport partner service and retrieve a tracking id.
            var trackingId = this.transportServiceWrapper.RequestShipment(activeOrder);

            if (this.OnOrderProcessed != null)
            {
                this.OnOrderProcessed(this, new OrderProcessedEventArgs { ActiveOrder = activeOrder });
            }

            // if tracking id received, delivery request is acknowledged, it is safe to update the status queue with the "Order Received" status.
            return trackingId;
        }
    }
}
