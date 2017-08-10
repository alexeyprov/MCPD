//---------------------------------------------------------------------------------
// Microsoft (R)  Windows Azure AppFabric SDK
// Software Development Kit
// 
// Copyright (c) Microsoft Corporation. All rights reserved.  
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
//---------------------------------------------------------------------------------

namespace Microsoft.Samples.AsyncMessaging
{
    using System;
    using System.Threading;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    public class AsyncReceiver
    {
        const string QueueName = "IssueTrackingQueue";
        static string ServiceBusConnectionString;

        static void Main(string[] args)
        {
            GetUserCredentials();
 
            MessagingFactory factory = null;

            try
            {
                //*****************************************************************************************************
                //                                   Management Operations
                //*****************************************************************************************************           
                NamespaceManager namespaceClient = NamespaceManager.CreateFromConnectionString(ServiceBusConnectionString);
                if (namespaceClient == null)
                {
                    Console.WriteLine("\nUnexpected Error: NamespaceManager is NULL");
                    return;
                }

                //Retreive details of the queue 
                QueueDescription queueDescription = namespaceClient.GetQueue(AsyncReceiver.QueueName);
                if (queueDescription == null)
                {
                    Console.WriteLine("\nUnexpected Error: QueueDescription is NULL");
                    return;
                }

                //*****************************************************************************************************
                //                                   Runtime Operations
                //*****************************************************************************************************
                factory = MessagingFactory.CreateFromConnectionString(ServiceBusConnectionString);

                QueueClient myQueueClient = factory.CreateQueueClient(AsyncReceiver.QueueName, ReceiveMode.PeekLock);

                //*****************************************************************************************************
                //                                   Receiving messages from a Queue
                //*****************************************************************************************************

                Console.WriteLine("\nReceiving messages from Queue '{0}'...", AsyncReceiver.QueueName);

                // Retreive the number of messages currently in the Queue
                long messageCount = queueDescription.MessageCount;

                // Initiate the Asynchronous Receive using BeginReceive() call to receive the messages. 
                for (long count = 0; count < messageCount; count++)
                {
                    myQueueClient.BeginReceive(TimeSpan.FromSeconds(30), OnMessageReceive, myQueueClient);
                }

                Console.WriteLine("\nAfter all messages are received, press ENTER to exit.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception {0}", e.ToString());
                throw;
            }
            finally
            {
                // Closing factory close all entities created from the factory.
                if (factory != null)
                {
                    factory.Close(); 
                }
            }
        }

        static void GetUserCredentials()
        {
            Console.Write("Please provide a connection string to Service Bus (/? for help):\n ");
            ServiceBusConnectionString = Console.ReadLine();

            if ((String.Compare(ServiceBusConnectionString, "/?") == 0) || (ServiceBusConnectionString.Length == 0))
            {
                Console.Write("To connect to the Service Bus cloud service, go to the Windows Azure portal and select 'View Connection String'.\n");
                Console.Write("To connect to the Service Bus for Windows Server, use the get-sbClientConfiguration PowerShell cmdlet.\n\n");
                Console.Write("A Service Bus connection string has the following format: \nEndpoint=sb://<namespace>.servicebus.windows.net/;SharedAccessKeyName=<keyName>;SharedAccessKey=<key>");

                ServiceBusConnectionString = Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public static void OnMessageReceive(IAsyncResult result)
        {
            QueueClient queueClient = (QueueClient) result.AsyncState ;

            try
            {
                //Receive the message with the EndReceive call
                BrokeredMessage message = queueClient.EndReceive(result);

                if (message != null)
                {
                    Console.WriteLine("\nMessage Received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>());

                    // The following is necessary in PeekLock Receive mode only.
                    // Complete the message asynchronously. 
                    message.BeginComplete(OnMessageComplete, message);
                }

                else
                {
                    Console.WriteLine("OnMessageReceive: Unexpected Error, Did not receive any messages");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OnMessageReceive: Unexpected exception in {0}", e.ToString());
                throw;
            }
        }

        public static void OnMessageComplete(IAsyncResult result)
        {
            BrokeredMessage message = (BrokeredMessage) result.AsyncState ;

            // Signal the Completion of the Asynchronous Message Receive.
            try
            {
                message.EndComplete(result);
                Console.WriteLine("\nAsynchronous Message Receive Completed for Id = {0}", message.MessageId);
            }
            catch (Exception e)
            {
                Console.WriteLine("OnMessageComplete: Unexpected exception {0}", e.ToString());
                throw;
            }            
        }
    }
}
