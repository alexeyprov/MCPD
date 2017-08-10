//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.ServiceBus.Samples.SessionMessages
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;
    using Microsoft.ServiceBus.Messaging;

    class Program
    {
        #region Fields
        // Delay to simulate processing time
        static TimeSpan senderDelay = TimeSpan.FromMilliseconds(100);

        // Credentials to access Service Bus
        static string serviceBusNamespace;
        static string serviceBusKeyName;
        static string serviceBusKey;
        #endregion

        static void Main(string[] args)
        {
            try
            {
                ParseArgs(args);
                Console.Title = "Message Sender";

                // Get credentials as Endpoint behavior
                TransportClientEndpointBehavior securityBehavior = new TransportClientEndpointBehavior();
                securityBehavior.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(serviceBusKeyName, serviceBusKey);

                // Create factory and channel using NetMessagingBinding
                NetMessagingBinding messagingBinding = new NetMessagingBinding("messagingBinding");
                EndpointAddress address = SampleManager.GetEndpointAddress(SampleManager.SessionlessQueueName, serviceBusNamespace);
                
                IChannelFactory<IOutputChannel> messagingChannelFactory = null;
                IOutputChannel messagingOutputChannel = null;
                try
                {
                    messagingChannelFactory = messagingBinding.BuildChannelFactory<IOutputChannel>(securityBehavior);
                    messagingChannelFactory.Open();
                    messagingOutputChannel = messagingChannelFactory.CreateChannel(address);
                    messagingOutputChannel.Open();

                    // Send messages to queue which does not require session
                    Console.WriteLine("Preparing to send messages to {0}...", SampleManager.SessionlessQueueName);
                    Thread.Sleep(3000);

                    SendMessages(messagingOutputChannel, messagingBinding);
                    messagingOutputChannel.Close();
                    messagingChannelFactory.Close();
                }
                catch (Exception)
                {
                    if (messagingOutputChannel != null)
                    {
                        messagingOutputChannel.Abort();
                    }
                    if (messagingChannelFactory != null)
                    {
                        messagingChannelFactory.Abort();
                    }
                    throw;
                }

                // Wait for all receivers to receive message
                Thread.Sleep(TimeSpan.FromSeconds(5.0d));
                Console.Clear();

                // Create factory and channel using custom binding
                CustomBinding customBinding = new CustomBinding("customBinding");
                address = SampleManager.GetEndpointAddress(SampleManager.SessionQueueName, serviceBusNamespace);
                
                IChannelFactory<IOutputChannel> customChannelFactory = null;
                IOutputChannel customOutputChannel = null;
                try
                {
                    customChannelFactory = customBinding.BuildChannelFactory<IOutputChannel>(securityBehavior);
                    customChannelFactory.Open();
                    customOutputChannel = customChannelFactory.CreateChannel(address);
                    customOutputChannel.Open();

                    // Send messages to queue which requires session
                    Console.Title = "Session MessageSender";
                    Console.WriteLine("Preparing to send messages to {0}...", SampleManager.SessionQueueName);
                    Thread.Sleep(3000);

                    SendMessages(customOutputChannel, customBinding);
                    customOutputChannel.Close();
                    customChannelFactory.Close();
                }
                catch (Exception)
                {
                    if (customOutputChannel != null)
                    {
                        customOutputChannel.Abort();
                    }
                    if (customChannelFactory != null)
                    {
                        customChannelFactory.Abort();
                    }
                    throw;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Occurred: {0}", exception);
                SampleManager.ExceptionOccurred = true;
            }

            // All messages sent
            Console.WriteLine("\nSender complete. Press [Enter] to exit.");
            Console.ReadLine();
        }

        static void SendMessages(IOutputChannel clientChannel, Binding binding)
        {
            // Send messages to queue:
            Console.WriteLine("Started sending messages...");
            Random rand = new Random();
            for (int i = 0; i < SampleManager.NumMessages; ++i)
            {
                string sessionName = rand.Next(SampleManager.NumSessions).ToString();

                // Creating BrokeredMessageProperty
                BrokeredMessageProperty property = new BrokeredMessageProperty();
                property.SessionId = sessionName;
                string soapBody = "Order_" + Guid.NewGuid().ToString().Substring(0, 5); 
                property.Label = soapBody;

                // Creating message and adding BrokeredMessageProperty to the properties bag
                Message message = Message.CreateMessage(binding.MessageVersion, "SoapAction", soapBody);
                message.Properties.Add(BrokeredMessageProperty.Name, property);

                // Sending message
                clientChannel.Send(message);
                SampleManager.OutputMessageInfo("Send", message);
                Thread.Sleep(senderDelay);
            }

            Console.WriteLine("Finished sending messages");
        }

        private static void ParseArgs(string[] args)
        {
            if (args.Length != 3)
            {
                throw new InvalidOperationException("Usage: Sender.exe <serviceNamespace> <keyName> <key>");
            }

            serviceBusNamespace = args[0];
            serviceBusKeyName = args[1];
            serviceBusKey = args[2];
        }
    }
}
