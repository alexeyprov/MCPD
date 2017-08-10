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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;
    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;
    using WinFrms = System.Windows.Forms;    

    public class SampleManager
    {
        #region Fields
        // Credentials to access Service Bus
        static string serviceBusNamespace;
        static string serviceBusKeyName;
        static string serviceBusKey;

        // Object for service bus management operations
        static NamespaceManager namespaceClient;

        static List<Process> receiverProcs = new List<Process>();
        static List<Process> senderProcs = new List<Process>();
        const int MAX_SESSIONS = 7;
        static int numSessions = 7;
        static int numSenders = 1;
        static int numReceivers = 4;        
        static int numMessages = 100;
        static bool exceptionOccurred = false;

        static bool displayVertical = true;

        static string baseQueueName = "OrderQueue";
        static string sessionlessQueueName = baseQueueName + "_NoSession";
        static string sessionQueueName = baseQueueName + "_Session";

        // Used to identify sender messages with different colors
        static ConsoleColor[] colors = new ConsoleColor[] { 
            ConsoleColor.Red, 
            ConsoleColor.Green, 
            ConsoleColor.Yellow, 
            ConsoleColor.Cyan,
            ConsoleColor.Magenta,
            ConsoleColor.Blue,             
            ConsoleColor.White};

        // constants for imported Win32 functions
        private static IntPtr HWND_TOP = new IntPtr(0);
        //private const int SW_MINIMIZE = 6;
        #endregion

        #region Imports for sample display purpose only
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        #endregion

        static void Main(string[] args)
        {
            try
            {
                #region Setup
                Console.Title = "Sample Manager";
                
                GetUserCredentials();
                
                numSessions = Math.Min(numSessions, MAX_SESSIONS);
                #endregion

                #region Create Queues
                Console.WriteLine("Creating Queues...");
                CreateNamespaceManager();
                QueueDescription sessionlessQueue = CreateQueue(false);
                Console.WriteLine("Created {0}, Queue.RequiresSession = false", sessionlessQueue.Path);
                QueueDescription sessionQueue = CreateQueue(true);
                Console.WriteLine("Created {0}, Queue.RequiresSession = true", sessionQueue.Path);
                #endregion

                #region Launch Senders and Receivers
                Console.WriteLine("\nLaunching senders and receivers...");
                StartSenders();
                StartReceivers();
                Thread.Sleep(TimeSpan.FromSeconds(5.0d)); // waiting for all senders and receivers to start

                // If exception has occured notify user
                if (SampleManager.ExceptionOccurred)
                {
                    Console.WriteLine("An exception has occured. Please ensure that ServiceBus credentials that were entered are correct.");
                }
                #endregion

                #region Cleanup
                Console.WriteLine("\nPress [Enter] to exit.");
                Console.ReadLine();

                // Cleanup:
                namespaceClient.DeleteQueue(sessionlessQueue.Path);
                namespaceClient.DeleteQueue(sessionQueue.Path);
                StopSenders();
                StopReceivers();
                #endregion;
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has occured.");
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
        }

        #region HelperFunctions
        static void GetUserCredentials()
        {
            // User namespace
            Console.Write("Please provide the namespace: ");
            serviceBusNamespace = Console.ReadLine();

            // Issuer name
            Console.Write("Please provide the key name (e.g., \"RootManageSharedAccessKey\"): ");
            serviceBusKeyName = Console.ReadLine();

            // Issuer key
            Console.Write("Please provide the key: ");
            serviceBusKey = Console.ReadLine();
        }

        // Create the NamespaceManager for management operations (queue)
        static void CreateNamespaceManager()
        {
            // Create SharedSecretCredential object for access control service
            TokenProvider credentials = TokenProvider.CreateSharedAccessSignatureTokenProvider(serviceBusKeyName, serviceBusKey);

            // Create the management Uri
            Uri managementUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceBusNamespace, string.Empty);
            namespaceClient = new NamespaceManager(managementUri, credentials);
        }

        // Create the entity (queue)
        static QueueDescription CreateQueue(bool session)
        {
            string queueName = (session ? sessionQueueName : sessionlessQueueName);
            QueueDescription queueDescription = new QueueDescription(queueName) { RequiresSession = session };

            // Try deleting the queue before creation. Ignore exception if queue does not exist.
            try
            {
                namespaceClient.DeleteQueue(queueDescription.Path);
            }
            catch (MessagingEntityNotFoundException)
            {
            }

            return namespaceClient.CreateQueue(queueDescription);
        }

        private static void StartSenders()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "Sender.exe";
            startInfo.Arguments = CreateArgs();
            for (int i = 0; i < numSenders; ++i)
            {
                Process process = Process.Start(startInfo);
                if (!process.HasExited)
                {
                    senderProcs.Add(process);
                }
                else
                {
                    SampleManager.ExceptionOccurred = true;
                }
            }

            Thread.Sleep(500);
        }

        private static void StopSenders()
        {
            foreach (Process proc in senderProcs)
            {
                proc.CloseMainWindow();
            }
        }

        static void StartReceivers()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "Receiver.exe";
            startInfo.Arguments = CreateArgs();
            for (int i = 0; i < numReceivers; ++i)
            {
                Process process = Process.Start(startInfo);
                if (!process.HasExited)
                {
                    receiverProcs.Add(process);
                }
                else
                {
                    SampleManager.ExceptionOccurred = true;
                }
            }

            Thread.Sleep(500);
            ArrangeWindows();
        }

        static void StopReceivers()
        {
            foreach (Process proc in receiverProcs)
            {
                proc.CloseMainWindow();
            }
        }

        static string CreateArgs()
        {
            string args = serviceBusNamespace + " " + serviceBusKeyName + " " + serviceBusKey;
            return args;
        }

        /// <summary>
        /// This function is only used for visual asthetics and does not provide any additional value.
        /// </summary>
        static void ArrangeWindows()
        {  
            int screenWidth = WinFrms.Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = WinFrms.Screen.PrimaryScreen.WorkingArea.Height;
           
            int maxHeight = screenHeight / 3;
            int maxWidth = screenWidth / 2;

            
            int senderWidth = screenWidth / (numSenders + 1);            
            int senderHeight = maxHeight;
            int managerWidth = senderWidth;
            int managerHeight = senderHeight;
            int receiverWidth = screenWidth / (numReceivers);
            int receiverHeight = screenHeight / 2;
            if (displayVertical)
            {
                senderWidth = screenWidth / 3;
                senderHeight = Math.Min(maxHeight, screenHeight / (numSenders + 1));
                managerWidth = maxWidth;
                managerHeight = senderHeight;
                receiverWidth = screenWidth / 3;
                receiverHeight = Math.Min(maxHeight, screenHeight / (numReceivers));
            }

            Console.Title = "Manager";
            IntPtr mainHandle = Process.GetCurrentProcess().MainWindowHandle;
            SetWindowPos(mainHandle, HWND_TOP, 0, 0, managerWidth, managerHeight, 0);
            
            for (int i = 0; i < senderProcs.Count; ++i)
            {
                IntPtr handle = senderProcs[i].MainWindowHandle;
                if (displayVertical)
                {
                    SetWindowPos(handle, HWND_TOP, 0, senderHeight * (i + 1), senderWidth, senderHeight, 0);
                }
                else
                {
                    SetWindowPos(handle, HWND_TOP, senderWidth * (i + 1), 0, senderWidth, senderHeight, 0);
                }
            }

            for (int i = 0; i < receiverProcs.Count; ++i)
            {
                IntPtr handle = receiverProcs[i].MainWindowHandle;
                if (displayVertical)
                {
                    SetWindowPos(handle, HWND_TOP, screenWidth - receiverWidth, receiverHeight * i, receiverWidth, receiverHeight, 0);
                }
                else
                {
                    SetWindowPos(handle, HWND_TOP, receiverWidth * i, screenHeight / 2, receiverWidth, receiverHeight, 0);
                }
            }
        }
        #endregion

        #region PublicHelpers
        // Public helper functions and accessors

        public static String SessionQueueName
        {
            get { return sessionQueueName; }
            set { sessionQueueName = value; }
        }

        public static String SessionlessQueueName
        {
            get { return sessionlessQueueName; }
            set { sessionlessQueueName = value; }
        }

        public static bool ExceptionOccurred
        {
            get { return exceptionOccurred; }
            set { exceptionOccurred = value; }
        }

        public static int NumSessions
        {
            get { return numSessions; }
            set { numSessions = value; }
        }

        public static int NumMessages
        {
            get { return numMessages; }
            set { numMessages = value; }
        }

        public static EndpointAddress GetEndpointAddress(string queueName, string serviceBusNamespace)
        {
            return new EndpointAddress(ServiceBusEnvironment.CreateServiceUri("sb", serviceBusNamespace, queueName));
        }

        public static void OutputMessageInfo(string action, Message message, string additionalText = "")
        {
            lock (typeof(SampleManager))
            {
                BrokeredMessageProperty property = (BrokeredMessageProperty)message.Properties[BrokeredMessageProperty.Name];
                Console.ForegroundColor = colors[int.Parse(property.SessionId)];
                Console.WriteLine("{0}: {1} - Group {2}. {3}", action, property.Label, property.SessionId, additionalText);
                Console.ResetColor();
            }
        }
        #endregion
    }
}
