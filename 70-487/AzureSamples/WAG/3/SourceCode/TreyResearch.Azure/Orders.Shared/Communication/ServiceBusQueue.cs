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
    using System.Threading.Tasks;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Orders.Shared.Communication.Adapters;
    using Orders.Shared.Helpers;
    using Guard = Orders.Shared.Guard;

    public class ServiceBusQueue
    {
        private readonly ServiceBusQueueDescription description;
        private readonly TokenProvider tokenProvider;
        private readonly IMessageSenderAdapter senderAdapter;
        private readonly MessageReceiver receiver;

        public ServiceBusQueue(ServiceBusQueueDescription description)
        {
            Guard.CheckArgumentNull(description, "description");

            this.description = description;
            this.tokenProvider = TokenProvider.CreateSharedSecretTokenProvider(this.description.Issuer, this.description.DefaultKey);

            var runtimeUri = ServiceBusEnvironment.CreateServiceUri("sb", this.description.Namespace, string.Empty);
            var messagingFactory = MessagingFactory.Create(runtimeUri, this.tokenProvider);

            var sender = messagingFactory.CreateMessageSender(this.description.QueueName.ToLowerInvariant());
            this.senderAdapter = new MessageSenderAdapter(sender);

            var rec = messagingFactory.CreateMessageReceiver(this.description.QueueName.ToLowerInvariant(), ReceiveMode.PeekLock);
            this.receiver = rec;
        }

        public ServiceBusQueue()
        {
        }

        public void CreateIfNotExists()
        {
            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", this.description.Namespace, string.Empty);
            var namespaceClient = new NamespaceManager(serviceUri, this.tokenProvider);

            if (!namespaceClient.QueueExists(this.description.QueueName))
            {
                namespaceClient.CreateQueue(this.description.QueueName);
            }
        }

        public void Send(IBrokeredMessageAdapter message)
        {
            Guard.CheckArgumentNull(message, "message");

            this.Send(message, this.senderAdapter); 
        }

        public void Send(IBrokeredMessageAdapter message, IMessageSenderAdapter sender)
        {
            Guard.CheckArgumentNull(message, "message");
            Guard.CheckArgumentNull(sender, "sender");

            // We don't use the Transient Fault Handling Application Block here because we need to create a receive-send 
            // message loop by using the TaskCreationOptions.AttachedToParent option
            Task.Factory
                .FromAsync(sender.BeginSend, sender.EndSend, message, null, TaskCreationOptions.AttachedToParent)
                .ContinueWith(
                    taskResult =>
                        {
                            try
                            {
                                if (taskResult.Exception != null)
                                {
                                    TraceHelper.TraceError(taskResult.Exception.ToString());
                                }
                            }
                            finally
                            {
                                message.Dispose();
                            }
                        });
        }

        public MessageReceiver GetReceiver()
        {
            return this.receiver;
        }
    }
}
