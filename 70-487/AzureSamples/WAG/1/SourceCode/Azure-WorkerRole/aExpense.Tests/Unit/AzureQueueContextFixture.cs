//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Tests.Unit
{
    using System;
    using System.Runtime.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Queues;

    [TestClass]
    public class AzureQueueContextFixture
    {       
        [TestMethod]
        public void AddAndGetMessageFromWindowsAzureQueue()
        {
            var queue = new AzureQueueContext();
            queue.Purge<MockMessage>();
            var originalMessage = new MockMessage
                                      {
                                          StringProperty = "String",
                                          DateTimeProperty = DateTime.Now,
                                          IntProperty = 10002,
                                          BoolProperty = true,
                                          UriProperty = new Uri("http://someuri")
                                      };

            queue.AddMessage(originalMessage);

            var retrievedMessage = queue.GetMessage<MockMessage>();

            Assert.IsNotNull(retrievedMessage);
            Assert.AreEqual(originalMessage.StringProperty, retrievedMessage.StringProperty);
            Assert.AreEqual(originalMessage.IntProperty, retrievedMessage.IntProperty);
            Assert.AreEqual(originalMessage.UriProperty, retrievedMessage.UriProperty);
            Assert.AreEqual(originalMessage.BoolProperty, retrievedMessage.BoolProperty);
            
            // date times are not equal because of serialization/deserialization (up to milliseconds works)
            Assert.AreEqual(originalMessage.DateTimeProperty.Year, retrievedMessage.DateTimeProperty.Year);
            Assert.AreEqual(originalMessage.DateTimeProperty.Month, retrievedMessage.DateTimeProperty.Month);
            Assert.AreEqual(originalMessage.DateTimeProperty.Day, retrievedMessage.DateTimeProperty.Day);
            Assert.AreEqual(originalMessage.DateTimeProperty.Hour, retrievedMessage.DateTimeProperty.Hour);
            Assert.AreEqual(originalMessage.DateTimeProperty.Minute, retrievedMessage.DateTimeProperty.Minute);
            Assert.AreEqual(originalMessage.DateTimeProperty.Second, retrievedMessage.DateTimeProperty.Second);
            Assert.AreEqual(originalMessage.DateTimeProperty.Millisecond, retrievedMessage.DateTimeProperty.Millisecond);
        }

        [TestMethod]
        public void AddAndGetMultipleMessagesFromWindowsAzureQueue()
        {
            var queue = new AzureQueueContext();
            queue.Purge<MockMessage2>();
            var originalMessages = new MockMessage2[] 
                                       {
                                           new MockMessage2
                                               {
                                                   StringProperty = "String",
                                               },
                                           new MockMessage2
                                               {
                                                   StringProperty = "String2",
                                               }
                                       };

            queue.AddMessage(originalMessages[0]);
            queue.AddMessage(originalMessages[1]);

            var retrievedMessages = queue.GetMultipleMessages<MockMessage2>(2);

            int i = 0;
            foreach (var retrievedMessage in retrievedMessages)
            {
                Assert.AreEqual(originalMessages[i].StringProperty, retrievedMessage.StringProperty);
                i++;
            }
        }

        [TestMethod]
        public void PurgeAddOneAndGetReturnOneMsgFromWindowsAzureQueue()
        {
            var queue = new AzureQueueContext();
            queue.Purge<MockMessage3>();
            queue.AddMessage(new MockMessage3 { StringProperty = "dummy" });
            var retrievedMessages = queue.GetMultipleMessages<MockMessage3>(2);

            Assert.IsNotNull(retrievedMessages);
            Assert.AreEqual(1, retrievedMessages.Length);
            Assert.AreEqual("dummy", retrievedMessages[0].StringProperty);
        }

        [TestMethod]
        public void DeleteMsgFromWindowsAzureQueue()
        {
            var queue = new AzureQueueContext();
            queue.Purge<MockMessage4>();
            queue.AddMessage(new MockMessage4 { StringProperty = "dummy" });
            var retrievedMessage = queue.GetMessage<MockMessage4>();
            queue.DeleteMessage(retrievedMessage); // we expect this to not throw

            Assert.IsNotNull(retrievedMessage);
            Assert.AreEqual("dummy", retrievedMessage.StringProperty);
        }

        [DataContract]
        internal class MockMessage : BaseQueueMessage
        {
            [DataMember]
            public string StringProperty { get; set; }
            [DataMember]
            public DateTime DateTimeProperty { get; set; }
            [DataMember]
            public int IntProperty { get; set; }
            [DataMember]
            public bool BoolProperty { get; set; }
            [DataMember]
            public Uri UriProperty { get; set; }
        }

        [DataContract]
        internal class MockMessage2 : MockMessage
        {
        }

        [DataContract]
        internal class MockMessage3 : MockMessage
        {
        }

        [DataContract]
        internal class MockMessage4 : MockMessage
        {
        }
    }
}