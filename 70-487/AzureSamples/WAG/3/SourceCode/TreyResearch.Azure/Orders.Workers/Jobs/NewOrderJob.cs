//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.ServiceBus.Messaging;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Orders.Shared;
    using Orders.Shared.Communication;
    using Orders.Shared.Communication.Messages;
    using Orders.Shared.Helpers;
    using Orders.Workers.Stores;

    public class NewOrderJob : IJob
    {
        private ServiceBusTopic newOrderMessageSender;
        private IProcessStatusStore processStatusStore;
        private ITransportPartnerStore transportPartnerStore;
        private string replyQueueName;
        private string serviceBusNamespace;
        private string acsNamespace;
        private bool keepRunning;

        public void Run()
        {
            TraceHelper.TraceInformation("Initializing NewOrderJob...");

            this.keepRunning = true;
            this.transportPartnerStore = new TransportPartnerStore();
            this.processStatusStore = new ProcessStatusStore();

            this.serviceBusNamespace = CloudConfiguration.GetConfigurationSetting("serviceBusNamespace", string.Empty);
            this.acsNamespace = CloudConfiguration.GetConfigurationSetting("acsNamespace", string.Empty);
            var topicName = CloudConfiguration.GetConfigurationSetting("topicName", string.Empty);
            var issuer = CloudConfiguration.GetConfigurationSetting("newOrdersTopicIssuer", string.Empty);
            var defaultKey = CloudConfiguration.GetConfigurationSetting("newOrdersTopicKey", string.Empty);
            this.replyQueueName = CloudConfiguration.GetConfigurationSetting("replyQueueName", string.Empty);

            var serviceBusTopicDescription = new ServiceBusTopicDescription
            {
                Namespace = this.serviceBusNamespace,
                TopicName = topicName,
                Issuer = issuer,
                DefaultKey = defaultKey
            };

            this.newOrderMessageSender = new ServiceBusTopic(serviceBusTopicDescription);

            while (this.keepRunning)
            {
                this.Execute();
                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }

        public void Stop()
        {
            this.keepRunning = false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The brokered message is disposed."), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "The brokered message is disposed.")]
        private void Execute()
        {
            var batchId = this.processStatusStore.LockOrders(RoleEnvironment.CurrentRoleInstance.Id);
            var ordersToProcess = this.processStatusStore.GetLockedOrders(RoleEnvironment.CurrentRoleInstance.Id, batchId);

            foreach (var orderProcess in ordersToProcess)
            {
                if (orderProcess.LockedUntil < DateTime.UtcNow)
                {
                    // If the orderProcess expired, ignore it and let another worker role process it.
                    continue;
                }

                // Used for filtering subscriptions
                var transportPartnerName = this.transportPartnerStore.GetTransportPartnerName(orderProcess.Order.State);
                Func<BrokeredMessage> brokeredMessageFunc = () =>
                        {
                            // Send new order message
                            var msg = new NewOrderMessage
                            {
                                OrderId = orderProcess.Order.OrderId,
                                OrderDate = orderProcess.Order.OrderDate,
                                ShippingAddress = orderProcess.Order.Address,
                                Amount = Convert.ToDouble(orderProcess.Order.Total),
                                CustomerName = orderProcess.Order.UserName
                            };

                            var brokeredMessage = new BrokeredMessage(msg)
                            {
                                MessageId = msg.OrderId.ToString(),
                                Properties = { { "TransportPartnerName", transportPartnerName }, { "ServiceBusNamespace", this.serviceBusNamespace }, { "AcsNamespace", this.acsNamespace }, { "OrderAmount", orderProcess.Order.Total } },
                                ReplyTo = this.replyQueueName
                            };

                            return brokeredMessage;
                        };

                var objectState = new Dictionary<string, object>
                    {
                        { "orderId", orderProcess.OrderId }, 
                        { "transportPartner", transportPartnerName }
                    };

                this.newOrderMessageSender
                    .Send(
                       brokeredMessageFunc,
                        objectState,
                        obj =>
                        {
                            var objState = (IDictionary<string, object>)obj;
                            var orderId = (Guid)objState["orderId"];
                            var transportPartner = (string)objState["transportPartner"];

                            this.processStatusStore.SendComplete(orderId, transportPartner);
                        },
                        (exception, obj) =>
                        {
                            var objState = (IDictionary<string, object>)obj;
                            var orderId = (Guid)objState["orderId"];

                            this.processStatusStore.UpdateWithError(exception, orderId);
                        });
            }
        }
    }
}
