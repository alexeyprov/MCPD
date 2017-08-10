//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Shared.Queues
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using AExpense;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public class AzureQueueContext : IQueueContext
    {
        private readonly CloudQueueClient queue;
        private readonly Microsoft.Practices.TransientFaultHandling.RetryPolicy storageRetryPolicy;
        private readonly ICollection<string> ensuredQueues;

        public AzureQueueContext()
            : this(CloudConfiguration.GetStorageAccount("DataConnectionString"))
        {
        }

        public AzureQueueContext(CloudStorageAccount accountInfo)
        {
            if (accountInfo == null)
            {
                throw new ArgumentNullException("accountInfo");
            }

            this.queue = accountInfo.CreateCloudQueueClient();
            this.ensuredQueues = new Collection<string>();
            this.storageRetryPolicy = RetryPolicyFactory.GetDefaultAzureStorageRetryPolicy();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "we want queue names to be lower case. We control the letters on that queue, hence we will never have culture problems")]
        public static string ResolveQueueName(MemberInfo messageType)
        {
            return messageType.Name.ToLowerInvariant();
        }

        public void AddMessage(BaseQueueMessage message)
        {
            var queueName = ResolveQueueName(message.GetType());

            var json = Serialize(message.GetType(), message);
            var cloudQueue = this.storageRetryPolicy.ExecuteAction(() => this.queue.GetQueueReference(queueName));
            this.storageRetryPolicy.ExecuteAction(() => cloudQueue.AddMessage(new CloudQueueMessage(json)));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "we are already providing a non-generic version. Why it is not detecting that?")]
        public T GetMessage<T>() where T : BaseQueueMessage
        {
            return (T)this.GetMessage(typeof(T));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "we are already providing")]
        public T[] GetMultipleMessages<T>(int maxMessages) where T : BaseQueueMessage
        {
            return this.GetMultipleMessages(typeof(T), maxMessages).Cast<T>().ToArray();
        }

        public object GetMessage(Type messageType)        
        {
            var queueName = ResolveQueueName(messageType);

            var cloudQueue = this.queue.GetQueueReference(queueName);
            var originalMessage = cloudQueue.GetMessage();
            if (originalMessage != null)
            {
                var message = Deserialize(messageType, originalMessage.AsString) as BaseQueueMessage;
                message.SetContext(originalMessage);
                return message;
            }

            return null;
        }

        public object[] GetMultipleMessages(Type messageType, int maxMessages)
        {
            var queueName = ResolveQueueName(messageType);

            var cloudQueue = this.queue.GetQueueReference(queueName);
            var originalMessages = cloudQueue.GetMessages(maxMessages);
            if (originalMessages != null)
            {
                var messages = new List<object>();
                foreach (var originalMessage in originalMessages)
                {
                    var message = Deserialize(messageType, originalMessage.AsString) as BaseQueueMessage;
                    message.SetContext(originalMessage);
                    messages.Add(message);
                }

                return messages.ToArray();
            }

            return null;
        }

        public void DeleteMessage(BaseQueueMessage message)
        {
            var queueName = ResolveQueueName(message.GetType());
            
            var originalMessage = message.GetContext() as CloudQueueMessage;
            if (originalMessage != null)
            {
                var cloudQueue = this.storageRetryPolicy.ExecuteAction(() => this.queue.GetQueueReference(queueName));
                this.storageRetryPolicy.ExecuteAction(() => cloudQueue.DeleteMessage(originalMessage));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "we are already providing a non-generic version")]
        public void Purge<T>() where T : BaseQueueMessage
        {
            this.Purge(typeof(T));
        }

        public void Purge(Type messageType)
        {
            var queueName = ResolveQueueName(messageType);
            this.EnsureQueueExists(queueName);
            var cloudQueue = this.queue.GetQueueReference(queueName);
            cloudQueue.Clear();
        }

        private static string Serialize(Type messageType, object obj)
        {
            var serializer = new DataContractJsonSerializer(messageType);
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        private static object Deserialize(Type messageType, string json)
        {
            var obj = Activator.CreateInstance(messageType);
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType());
                obj = serializer.ReadObject(ms);
            }

            return obj;
        }

        private void EnsureQueueExists(string queueName)
        {
            if (!this.ensuredQueues.Contains(queueName))
            {
                this.storageRetryPolicy.ExecuteAction(() => this.queue.GetQueueReference(queueName).CreateIfNotExist());
                this.ensuredQueues.Add(queueName);
            }
        }       
    }
}