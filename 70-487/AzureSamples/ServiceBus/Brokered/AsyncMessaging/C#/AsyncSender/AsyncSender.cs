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
    using System.Collections.Generic;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    public class AsyncSender
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

                Console.WriteLine("\nCreating Queue '{0}'...", QueueName);

                // Delete if exists
                if (namespaceClient.QueueExists(AsyncSender.QueueName))
                {
                    namespaceClient.DeleteQueue(AsyncSender.QueueName);
                }

                namespaceClient.CreateQueue(QueueName);

                //*****************************************************************************************************
                //                                   Runtime Operations
                //*****************************************************************************************************
                factory = MessagingFactory.CreateFromConnectionString(ServiceBusConnectionString);

                QueueClient myQueueClient = factory.CreateQueueClient(AsyncSender.QueueName);

                //*****************************************************************************************************
                //                                   Sending messages to a Queue
                //*****************************************************************************************************
                List<BrokeredMessage> messageList = new List<BrokeredMessage>();

                messageList.Add(CreateIssueMessage("1", "First Package"));
                messageList.Add(CreateIssueMessage("2", "Second Package"));
                messageList.Add(CreateIssueMessage("3", "Third Package"));

                Console.WriteLine("\nSending messages to Queue...");

                foreach (BrokeredMessage message in messageList)
                {
                    // Initiate the asynchronous send 
                    myQueueClient.BeginSend(message, OnSendComplete, new Tuple<QueueClient, string>(myQueueClient, message.MessageId));
                    Console.WriteLine(string.Format("Asynchronous Message Send Begin: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                }

                Console.WriteLine("\nAfter all messages are sent, press ENTER to clean up and exit.\n");
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
                if(factory != null)
                    factory.Close();
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

        private static BrokeredMessage CreateIssueMessage(string issueId, string issueBody)
        {
            BrokeredMessage message = new BrokeredMessage(issueBody);
            message.MessageId = issueId;
            return message;
        }

        public static void OnSendComplete(IAsyncResult result)
        {
            Tuple<QueueClient, string> stateInfo = (Tuple<QueueClient, string>) result.AsyncState ;

            QueueClient queueClient = stateInfo.Item1;
            string messageId = stateInfo.Item2;

            try
            {
                // Complete Asynchronous Message Send process
                queueClient.EndSend(result);
                Console.WriteLine("Asynchronous Message Send for Id = {0} Successful", messageId);
            }
            catch (Exception e)
            {
                Console.WriteLine("OnSendComplete: Asynchronous Message Send for Id = {0} Failed with Exception = {1}", messageId, e.ToString());
            }
        }
    }
}

