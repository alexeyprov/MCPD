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
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus.Messaging;
    using Orders.Shared.Communication.Adapters;
    using Orders.Shared.Communication.Exceptions;
    using Orders.Shared.Helpers;
    using Guard = Orders.Shared.Guard;

    public class ServiceBusReceiverHandler<T>
    {
        private readonly IMessageReceiverAdapter receiver;
        private Func<T, ServiceBusQueueDescription, string, Task> messageProcessingTask;

        public ServiceBusReceiverHandler(IMessageReceiverAdapter receiver)
        {
            Guard.CheckArgumentNull(receiver, "receiver");

            this.receiver = receiver;
        }

        public TimeSpan? MessagePollingInterval { get; set; }

        // The Func parameter (that returns the Task) allows the caller more control on the task result and the exception handling
        public void ProcessMessages(Func<T, ServiceBusQueueDescription, string, Task> taskForProcessingMessage, CancellationToken cancellationToken)
        {
            Guard.CheckArgumentNull(taskForProcessingMessage, "messageProcessingTask");

            this.messageProcessingTask = taskForProcessingMessage;

            this.ReceiveNextMessage(cancellationToken);
        }

        private void ReceiveNextMessage(CancellationToken cancellationToken)
        {
            if (this.MessagePollingInterval.HasValue)
            {
                Thread.Sleep(this.MessagePollingInterval.Value);
            }

            Task.Factory
                .FromAsync<TimeSpan, IBrokeredMessageAdapter>(this.receiver.BeginReceive, this.receiver.EndReceive, TimeSpan.FromSeconds(10), null, TaskCreationOptions.None)
                .ContinueWith(
                taskResult =>
                {
                    // Start receiving the next message as soon as we received the previous one.
                    // This will not cause a stack overflow because the call will be made from a new Task.
                    this.ReceiveNextMessage(cancellationToken);

                    if (taskResult.Exception != null)
                    {
                        TraceHelper.TraceError(taskResult.Exception.Message);
                    }

                    this.ProcessMessage(taskResult.Result);
                },
                cancellationToken);
        }

        private void ProcessMessage(IBrokeredMessageAdapter message)
        {
            if (message != null)
            {
                var token = string.Empty;
                if (message.Properties.ContainsKey("SimpleWebToken"))
                {
                    token = message.Properties["SimpleWebToken"].ToString();
                }

                var queueDescription = new ServiceBusQueueDescription
                {
                    QueueName = message.ReplyTo,
                };

                if (message.Properties.ContainsKey("ServiceBusNamespace"))
                {
                    queueDescription.Namespace = message.Properties["ServiceBusNamespace"].ToString();
                }

                if (message.Properties.ContainsKey("AcsNamespace"))
                {
                    queueDescription.SwtAcsNamespace = message.Properties["AcsNamespace"].ToString();
                }

               this.messageProcessingTask(message.GetBody<T>(), queueDescription, token)
                    .ContinueWith(
                        processingTaskResult =>
                        {
                            if (processingTaskResult.Exception != null)
                            {
                                if (message.DeliveryCount <= 3 && !(processingTaskResult.Exception.InnerException is InvalidTokenException))
                                {
                                    // If the abandon fails, the message will become visible anyway after the lock times out
                                    Task.Factory.FromAsync(message.BeginAbandon, message.EndAbandon, message, TaskCreationOptions.AttachedToParent)
                                                .ContinueWith(
                                                    taskResult =>
                                                    {
                                                        if (taskResult.Exception != null)
                                                        {
                                                            TraceHelper.TraceError("Error while message abandon: {0}", taskResult.Exception.InnerException.Message);
                                                        }

                                                        var msg = taskResult.AsyncState as BrokeredMessage;
                                                        if (msg != null)
                                                        {
                                                            msg.Dispose();
                                                        }
                                                    });
                                }
                                else
                                {
                                    Task.Factory.FromAsync(message.BeginDeadLetter, message.EndDeadLetter, message, TaskCreationOptions.AttachedToParent)
                                                .ContinueWith(
                                                    taskResult =>
                                                    {
                                                        if (taskResult.Exception != null)
                                                        {
                                                            TraceHelper.TraceError("Error while sending message to the DeadLetter queue: {0}", taskResult.Exception.InnerException.Message);
                                                        }

                                                        var msg = taskResult.AsyncState as BrokeredMessage;
                                                        if (msg != null)
                                                        {
                                                            msg.Dispose();
                                                        }
                                                    });

                                    TraceHelper.TraceError(processingTaskResult.Exception.TraceInformation());
                                }
                            }
                            else
                            {
                                Task.Factory
                                    .FromAsync(message.BeginComplete, message.EndComplete, message, TaskCreationOptions.AttachedToParent)
                                    .ContinueWith(
                                        taskResult =>
                                        {
                                            if (taskResult.Exception != null)
                                            {
                                                TraceHelper.TraceError("Error while executing message Complete: {0}", taskResult.Exception.InnerException.Message);
                                            }

                                            var msg = taskResult.AsyncState as BrokeredMessage;
                                            if (msg != null)
                                            {
                                                msg.Dispose();
                                            }
                                        });
                            }
                        });
                }
        }
    }
}
