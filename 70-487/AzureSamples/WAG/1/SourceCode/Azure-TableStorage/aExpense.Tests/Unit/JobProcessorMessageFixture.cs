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
    using System.Runtime.Serialization;
    using System.Threading;
    using AExpense.Shared.Queues;
    using Jobs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JobProcessorMessageFixture
    {       
        [TestMethod]
        public void ShouldProcessJobWhenMessageIsAddedToJobQueue()
        {
            AutoResetEvent signal = new AutoResetEvent(false); 
            var queue = new AzureQueueContext();
            queue.Purge<JobMessage>();
            queue.AddMessage(new JobMessage());

            var job = new MockJobProcessor(1000, queue, signal);
            Thread thread = new Thread(() =>
                                           {
                                               job.Run();
                                           });
            thread.Start();
            
            signal.WaitOne(5000);
            job.Stop();

            // queue is empty and job was Processed
            Assert.IsNull(queue.GetMessage<JobMessage>());
            Assert.AreEqual(true, job.Processed);
        }

        internal class MockJobProcessor : BaseJobProcessor<JobMessage>
        {
            private AutoResetEvent signal;
            
            public MockJobProcessor(int sleepInterval, IQueueContext queue, AutoResetEvent signal)
                : base(sleepInterval, queue)
            {
                this.signal = signal;
            }
            
            public bool Processed { get; set; }
            
            public override bool ProcessMessage(JobMessage message)
            {
                this.Processed = true;
                this.signal.Set();

                return this.Processed;
            }
        }

        [DataContract]
        internal class JobMessage : BaseQueueMessage
        {
        }
    }
}