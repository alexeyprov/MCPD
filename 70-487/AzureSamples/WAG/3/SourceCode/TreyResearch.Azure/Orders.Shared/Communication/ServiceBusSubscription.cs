//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Shared.Communication
{
    using System;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Orders.Shared.Communication.Adapters;

    public class ServiceBusSubscription
    {
        private readonly ServiceBusSubscriptionDescription description;
        private readonly TokenProvider tokenProvider;
        private readonly MessageReceiver receiver;

        public ServiceBusSubscription(ServiceBusSubscriptionDescription description)
        {
            Guard.CheckArgumentNull(description, "description");

            this.description = description;
            this.tokenProvider = TokenProvider.CreateSharedSecretTokenProvider(this.description.Issuer, this.description.DefaultKey);

            var runtimeUri = ServiceBusEnvironment.CreateServiceUri("sb", this.description.Namespace, string.Empty);
            var messagingFactory = MessagingFactory.Create(runtimeUri, this.tokenProvider);
            this.receiver = 
                messagingFactory.CreateMessageReceiver(
                    this.description.TopicName.ToLowerInvariant() + "/subscriptions/" +
                    this.description.SubscriptionName.ToLowerInvariant(),
                    ReceiveMode.PeekLock);
        }

        public void CreateIfNotExists()
        {
            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", this.description.Namespace, string.Empty);
            var namespaceClient = new NamespaceManager(serviceUri, this.tokenProvider);

            if (!namespaceClient.TopicExists(this.description.TopicName))
            {
                namespaceClient.CreateTopic(this.description.TopicName);
            }

            if (!string.IsNullOrEmpty(this.description.SubscriptionName) && !namespaceClient.SubscriptionExists(this.description.TopicName.ToLowerInvariant(), this.description.SubscriptionName.ToLowerInvariant()))
            {
                namespaceClient.CreateSubscription(this.description.TopicName.ToLowerInvariant(), this.description.SubscriptionName.ToLowerInvariant());
            }
        }

        public void CreateIfNotExists(string filterExpression)
        {
            Guard.CheckArgumentNull(filterExpression, "filterExpression");

            var filter = new SqlFilter(filterExpression);

            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", this.description.Namespace, string.Empty);
            var namespaceClient = new NamespaceManager(serviceUri, this.tokenProvider);

            if (!namespaceClient.TopicExists(this.description.TopicName))
            {
                namespaceClient.CreateTopic(this.description.TopicName);
            }

            if (!string.IsNullOrEmpty(this.description.SubscriptionName) && !namespaceClient.SubscriptionExists(this.description.TopicName.ToLowerInvariant(), this.description.SubscriptionName.ToLowerInvariant()))
            {
                namespaceClient.CreateSubscription(this.description.TopicName.ToLowerInvariant(), this.description.SubscriptionName.ToLowerInvariant(), filter);
            }
        }

        public IMessageReceiverAdapter GetReceiver()
        {
            return new MessageReceiverAdapter(this.receiver);  
        }
    }
}
