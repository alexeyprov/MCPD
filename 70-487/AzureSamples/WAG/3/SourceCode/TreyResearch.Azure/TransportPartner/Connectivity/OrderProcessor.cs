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
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.ServiceBus.Messaging;
    using Orders.Shared.Communication;
    using Orders.Shared.Communication.Adapters;
    using Orders.Shared.Communication.Messages;

    /// <summary>
    /// Base component that performs the necessary operations against ServiceBus for TreyResearch
    /// </summary>
    public abstract class OrderProcessor
    {
        private readonly ServiceBusSubscriptionDescription serviceBusSubscriptionDescription;
        private readonly IDictionary<string, ServiceBusQueue> statusUpdateQueueDictionary = new Dictionary<string, ServiceBusQueue>();
        private readonly ServiceBusQueueDescription serviceBusQueueDescription;
        private readonly string acsPassword;
        private readonly string acsServiceIdentity;
        private CancellationTokenSource tokenSource;

        protected OrderProcessor(
            string topicName, 
            string subscriptionName, 
            string transportPartnerDisplayName, 
            string transportPartnerName, 
            string key, 
            string acsPassword, 
            string acsServiceIdentity)
        {
            this.serviceBusSubscriptionDescription = new ServiceBusSubscriptionDescription
            {
                TopicName = topicName,
                SubscriptionName = subscriptionName,
                Issuer = transportPartnerName,
                DefaultKey = key
            };

            this.serviceBusQueueDescription = new ServiceBusQueueDescription
            {
                Issuer = transportPartnerName,
                DefaultKey = key
            };

            this.TransportPartnerDisplayName = transportPartnerDisplayName;
            this.TransportPartnerName = transportPartnerName;

            this.tokenSource = new CancellationTokenSource();

            // ACS values for retrieving SWT token
            this.acsServiceIdentity = acsServiceIdentity;
            this.acsPassword = acsPassword;
        }

        protected internal string TransportPartnerDisplayName { get; set; }
        protected internal string TransportPartnerName { get; set; }

        public static string GetTokenFromAcs(string acsNamespace, string serviceIdentity, string password, string relyingPartyRealm)
        {
            // request a token from ACS
            var client = new WebClient { BaseAddress = acsNamespace };

            var values = new NameValueCollection
                {
                    { "wrap_name", serviceIdentity }, 
                    { "wrap_password", password }, 
                    { "wrap_scope", relyingPartyRealm }
                };

            byte[] responseBytes = client.UploadValues("WRAPv0.9/", "POST", values);

            string response = Encoding.UTF8.GetString(responseBytes);

            return HttpUtility.UrlDecode(
                response
                .Split('&')
                .Single(value => value.StartsWith("wrap_access_token=", StringComparison.OrdinalIgnoreCase))
                .Split('=')[1]);
        }

        public void Run()
        {
            var serviceBusNamespaces = ConfigurationManager.AppSettings["serviceBusNamespaces"].Split(',').ToList();
            this.tokenSource = new CancellationTokenSource();
            
            // The current context is passed to start the task so that the code runs in this thread, avoiding  
            // cross-thread exceptions when updating UI controls.
            var context = TaskScheduler.FromCurrentSynchronizationContext();

            foreach (var serviceBusNamespace in serviceBusNamespaces)
            {
                this.serviceBusSubscriptionDescription.Namespace = serviceBusNamespace;
                var serviceBusSubscription = new ServiceBusSubscription(this.serviceBusSubscriptionDescription);
                var receiverHandler = new ServiceBusReceiverHandler<NewOrderMessage>(serviceBusSubscription.GetReceiver())
                {
                    MessagePollingInterval = TimeSpan.FromSeconds(2)
                };

                // MessagePollingInterval should be configured taking into consideration variables such as CPU processing power, 
                // expected volume of orders to process and number of worker role instances
                receiverHandler.ProcessMessages(
                    (message, queueDescription, token) =>
                    {
                        return Task.Factory.StartNew(
                            () => this.ProcessMessage(message, queueDescription),
                            this.tokenSource.Token,
                            TaskCreationOptions.None,
                            context);
                    },
                    this.tokenSource.Token);
            }
        }

        public void Cancel()
        {
            this.tokenSource.Cancel();
        }
        
        protected void SendOrderShipped(Guid orderId, ServiceBusQueueDescription queueDescription, string message, string swt)
        {
            this.SendToUpdateStatusQueue(orderId, Guid.Empty, message, queueDescription, swt);
        }

        protected abstract Guid ProcessOrder(NewOrderMessage message, ServiceBusQueueDescription queueDescription);

        protected string GetToken(ServiceBusQueueDescription queueDescription)
        {
            var realm = string.Format("urn:{0}/{1}", queueDescription.QueueName, HttpUtility.UrlEncode(this.acsServiceIdentity));

            var token = GetTokenFromAcs(string.Format("https://{0}.accesscontrol.windows.net/", queueDescription.SwtAcsNamespace), this.acsServiceIdentity, this.acsPassword, realm);

            return token;
        }

        protected virtual void ProcessMessage(NewOrderMessage message, ServiceBusQueueDescription queueDescription)
        {
            var trackingId = this.ProcessOrder(message, queueDescription);

            if (trackingId != Guid.Empty)
            {
                // Get SWT from ACS.
                var token = this.GetToken(queueDescription);

                var statusMessage = string.Format("{0}: Order Received", this.TransportPartnerDisplayName);
                this.SendOrderReceived(message, queueDescription, statusMessage, trackingId, token);
            }
        }

        protected void SendOrderReceived(NewOrderMessage message, ServiceBusQueueDescription queueDescription, string statusMessage, Guid trackingId, string swt)
        {
            this.SendToUpdateStatusQueue(message.OrderId, trackingId, statusMessage, queueDescription, swt);
        }

        protected void SendOrderReceived(NewOrderMessage message, ServiceBusQueueDescription queueDescription, string statusMessage, Guid trackingId)
        {
            this.SendToUpdateStatusQueue(message.OrderId, trackingId, statusMessage, queueDescription, null);            
        }

        private void SendToUpdateStatusQueue(Guid orderId, Guid trackingId, string orderStatus, ServiceBusQueueDescription queueDescription, string swt)
        {
            var updateStatusMessage =
                new BrokeredMessage(
                    new OrderStatusUpdateMessage
                        {
                            OrderId = orderId, 
                            Status = orderStatus, 
                            TrackingId = trackingId, 
                            TransportPartnerName = this.TransportPartnerDisplayName,
                        });

            updateStatusMessage.Properties.Add("SimpleWebToken", swt);

            ServiceBusQueue replyQueue;
            if (this.statusUpdateQueueDictionary.ContainsKey(queueDescription.Namespace))
            {
                replyQueue = this.statusUpdateQueueDictionary[queueDescription.Namespace];
            }
            else
            {
                var description = new ServiceBusQueueDescription
                {
                    Namespace = queueDescription.Namespace,
                    QueueName = queueDescription.QueueName,
                    DefaultKey = this.serviceBusQueueDescription.DefaultKey,
                    Issuer = this.serviceBusQueueDescription.Issuer
                };

                replyQueue = new ServiceBusQueue(description);
                this.statusUpdateQueueDictionary.Add(queueDescription.Namespace, replyQueue);
            }

            var brokeredMessageAdapter = new BrokeredMessageAdapter(updateStatusMessage);
            replyQueue.Send(brokeredMessageAdapter);
        }
    }
}
