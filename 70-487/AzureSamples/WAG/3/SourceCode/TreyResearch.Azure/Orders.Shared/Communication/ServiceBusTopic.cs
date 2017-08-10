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
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using Orders.Shared.Helpers;
    using Guard = Orders.Shared.Guard;

    public class ServiceBusTopic
    {
        private readonly ServiceBusTopicDescription description;
        private readonly TokenProvider tokenProvider;
        private readonly MessageSender sender;
        private readonly RetryPolicy serviceBusRetryPolicy;

        public ServiceBusTopic(ServiceBusTopicDescription description)
        {
            Guard.CheckArgumentNull(description, "description");

            this.description = description;
            this.tokenProvider = TokenProvider.CreateSharedSecretTokenProvider(
                this.description.Issuer, this.description.DefaultKey);

            var runtimeUri = ServiceBusEnvironment.CreateServiceUri("sb", this.description.Namespace, string.Empty);
            var messagingFactory = MessagingFactory.Create(runtimeUri, this.tokenProvider);
            this.sender = messagingFactory.CreateMessageSender(this.description.TopicName.ToLowerInvariant());

            // this policy is defined in the configuration file
            this.serviceBusRetryPolicy = RetryPolicyFactory.GetDefaultAzureServiceBusRetryPolicy();
            this.serviceBusRetryPolicy.Retrying += (sender, args) => TraceHelper.TraceWarning("Retry in ServiceBusTopic - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
        }

        public void CreateIfNotExists()
        {
            Uri serviceUri = ServiceBusEnvironment.CreateServiceUri("sb", this.description.Namespace, string.Empty);
            var namespaceClient = new NamespaceManager(serviceUri, this.tokenProvider);

            if (!namespaceClient.TopicExists(this.description.TopicName))
            {
                namespaceClient.CreateTopic(this.description.TopicName);
            }
        }

        public void Send(
            Func<BrokeredMessage> createMessage, 
            object objectState, 
            Action<object> afterSendComplete, 
            Action<Exception, object> processError)
        {
            Guard.CheckArgumentNull(objectState, "objectState");
            Guard.CheckArgumentNull(afterSendComplete, "afterSendComplete");
            Guard.CheckArgumentNull(processError, "processError");

            this.serviceBusRetryPolicy.ExecuteAction<BrokeredMessage>(
                ac =>
                {
                    var message = createMessage();
                    var dictionary = objectState as Dictionary<string, object>;
                    
                    if (dictionary.ContainsKey("message"))
                    {
                        dictionary["message"] = message;
                    }
                    else
                    {
                        dictionary.Add("message", message);
                    }

                   this.sender.BeginSend(message, ac, objectState);
                },
                ar =>
                {
                    this.sender.EndSend(ar);
                    return (ar.AsyncState as Dictionary<string, object>)["message"] as BrokeredMessage;
                },
                (message) =>
                {
                    try
                    {
                        afterSendComplete(objectState);
                    }
                    catch (Exception ex)
                    {
                        TraceHelper.TraceError(ex.Message);
                    }
                    finally
                    {
                        message.Dispose();
                    }
                },
                e =>
                {
                    processError(e, objectState);
                    var message = (objectState as Dictionary<string, object>)["message"] as BrokeredMessage;
                    message.Dispose();
                });
        }
    }
}