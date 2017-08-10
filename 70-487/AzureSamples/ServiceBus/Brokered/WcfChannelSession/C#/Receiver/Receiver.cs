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
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    class Program
    {
        #region Fields
        static string serviceBusNamespace;
        static string serviceBusKeyName;
        static string serviceBusKey;

        // Timeout values
        static readonly TimeSpan receiveMessageTimeout = TimeSpan.FromSeconds(10);
        static readonly TimeSpan receiveSessionMessageTimeout = TimeSpan.FromSeconds(10);
        static readonly TimeSpan acceptSessionReceiverTimeout = TimeSpan.FromSeconds(10);
        #endregion

        static void Main(string[] args)
        {
            try
            {
                ParseArgs(args);

                // Create MessageReceiver for queue which does not require session
                Console.Title = "Message Receiver";
                Console.WriteLine("Ready to receive messages from {0}...", SampleManager.SessionlessQueueName);
                ReceiveMessages();

                // Create SessionReceiver for queue requiring session
                Console.Clear();
                Console.Title = "Session Message Receiver";
                Console.WriteLine("Ready to receive messages from {0}...", SampleManager.SessionQueueName);

                ReceiveSessionMessages();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception Occurred: {0}", exception);
                SampleManager.ExceptionOccurred = true;
            }

            Console.WriteLine("\nReceiver complete. Press [Enter] to exit.");
            Console.ReadLine();
        }

        private static void ReceiveMessages()
        {
            // Read messages from queue until queue is empty
            Console.WriteLine("Reading messages from queue {0}...", SampleManager.SessionlessQueueName);
            Console.WriteLine("Receiver Type: Receive and Delete");
            
            // Create channel listener and channel using NetMessagingBinding
            NetMessagingBinding messagingBinding = new NetMessagingBinding("messagingBinding");
            EndpointAddress address = SampleManager.GetEndpointAddress(SampleManager.SessionlessQueueName, serviceBusNamespace);
            TransportClientEndpointBehavior securityBehavior = new TransportClientEndpointBehavior();
            securityBehavior.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(serviceBusKeyName, serviceBusKey);

            IChannelListener<IInputChannel> inputChannelListener = null;
            IInputChannel inputChannel = null;
            try
            {
                inputChannelListener = messagingBinding.BuildChannelListener<IInputChannel>(address.Uri, securityBehavior);
                inputChannelListener.Open();
                inputChannel = inputChannelListener.AcceptChannel();
                inputChannel.Open();

                while (true)
                {
                    try
                    {
                        // Receive message from queue. If no more messages available, the operation throws a TimeoutException.
                        Message receivedMessage = inputChannel.Receive(receiveMessageTimeout);
                        SampleManager.OutputMessageInfo("Receive", receivedMessage);

                        // Since the message body contains a serialized string one can access it like this:
                        //string soapBody = receivedMessage.GetBody<string>();
                    }
                    catch (TimeoutException)
                    {
                        break;
                    }
                }

                // Close
                inputChannel.Close();
                inputChannelListener.Close();
            }
            catch (Exception)
            {
                if (inputChannel != null)
                {
                    inputChannel.Abort();
                }
                if (inputChannelListener != null)
                {
                    inputChannelListener.Abort();
                }
                throw;
            }
        }

        static void ReceiveSessionMessages()
        {
            // Read messages from queue until queue is empty:
            Console.WriteLine("Reading messages from queue {0}...", SampleManager.SessionQueueName);
            Console.WriteLine("Receiver Type: PeekLock");

            // Create listener and channel using custom binding
            CustomBinding customBinding = new CustomBinding("customBinding");
            EndpointAddress address = SampleManager.GetEndpointAddress(SampleManager.SessionQueueName, serviceBusNamespace);
            TransportClientEndpointBehavior securityBehavior = new TransportClientEndpointBehavior();
            securityBehavior.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(serviceBusKeyName, serviceBusKey);
            customBinding.GetProperty<IReceiveContextSettings>(new BindingParameterCollection()).Enabled = true;

            IChannelListener<IInputSessionChannel> inputSessionChannelListener = null;
            try
            {
                inputSessionChannelListener = customBinding.BuildChannelListener<IInputSessionChannel>(address.Uri, securityBehavior);
                inputSessionChannelListener.Open();

                while (true)
                {
                    IInputSessionChannel inputSessionChannel = null;
                    try
                    {
                        // Create a new session channel for every new session available. If no more sessions available, 
                        // then the operation throws a TimeoutException.
                        inputSessionChannel = inputSessionChannelListener.AcceptChannel(acceptSessionReceiverTimeout);
                        inputSessionChannel.Open();

                        // TryReceive operation returns true if message is available otherwise it returns false.
                        Message receivedMessage;
                        while (inputSessionChannel.TryReceive(receiveSessionMessageTimeout, out receivedMessage))
                        {
                            if (receivedMessage != null)
                            {
                                SampleManager.OutputMessageInfo("Receive", receivedMessage);

                                // Since the message body contains a serialized string one can access it like this:
                                //string soapBody = receivedMessage.GetBody<string>();

                                // Since the binding has ReceiveContext enabled, a manual complete operation is mandatory 
                                // if the message was processed successfully.
                                ReceiveContext rc;
                                if (ReceiveContext.TryGet(receivedMessage, out rc))
                                {
                                    rc.Complete(TimeSpan.FromSeconds(10.0d));
                                }
                                else
                                {
                                    throw new InvalidOperationException("Receiver is in peek lock mode but receive context is not available!");
                                }
                            }
                            else
                            {
                                // This IInputSessionChannel doesn't have any more messages
                                break;
                            }
                        }

                        // Close session channel
                        inputSessionChannel.Close();
                        inputSessionChannel = null;
                    }
                    catch (TimeoutException)
                    {
                        break;
                    }
                    finally
                    {
                        if (inputSessionChannel != null)
                        {
                            inputSessionChannel.Abort();
                        }
                    }
                }

                // Close channel listener
                inputSessionChannelListener.Close();
            }
            catch (Exception)
            {
                if (inputSessionChannelListener != null)
                {
                    inputSessionChannelListener.Abort();
                }
                throw;
            }
        }

        private static void ParseArgs(string[] args)
        {
            if (args.Length != 3)
            {
                throw new InvalidOperationException("Usage: Receiver.exe <serviceNamespace> <keyName> <key>");
            }

            serviceBusNamespace = args[0];
            serviceBusKeyName = args[1];
            serviceBusKey = args[2];
        }
    }
}
