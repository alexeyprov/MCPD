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
    using Orders.Shared.Communication;
    using TransportPartner.DataStores;

    /// <summary>
    /// The connector component will be used by those companies that wish to work with TreyResearch, but need the functionality to communicate through ServiceBus.
    /// </summary>
    public class Connector : OrderProcessor
    {
        private readonly IOrderStore orderStore;

        public Connector(
            string topicName, 
            string subscriptionName, 
            string transportPartnerDisplayName, 
            string transportPartnerName, 
            string key, 
            string acsPassword)
            : base(topicName, subscriptionName, transportPartnerDisplayName, transportPartnerName, key, acsPassword, transportPartnerDisplayName)
        {
            this.orderStore = new OrderStore();
        }

        public EventHandler<OrderProcessedEventArgs> OnOrderProcessed { get; set; }
        
        public void ShipOrder(Guid orderId, ServiceBusQueueDescription queueDescription)
        {
            // Get SWT from ACS.
            var token = this.GetToken(queueDescription);
            
            var message = string.Format("{0}: Order shipped", this.TransportPartnerDisplayName);
            this.SendOrderShipped(orderId, queueDescription, message, token);
        }

        protected override Guid ProcessOrder(Orders.Shared.Communication.Messages.NewOrderMessage message, ServiceBusQueueDescription queueDescription)
        {
            var processedOrder = this.orderStore.GetById(message.OrderId);

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
                Status = "received",
                SwtAcsNamespace = queueDescription.SwtAcsNamespace
            };

            this.orderStore.Add(activeOrder);

            if (this.OnOrderProcessed != null)
            {
                this.OnOrderProcessed(this, new OrderProcessedEventArgs { ActiveOrder = activeOrder });
            }

            // We assume successful order processing.
            return Guid.NewGuid();
        }
    }
}
