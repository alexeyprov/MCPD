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
    using Jobs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shared.Queues;

    [TestClass]
    public class JobProcessorEmptyFixture
    {       
        [TestMethod]
        public void ShouldNotProcessJobIfThereAreNoJobMessagesInQueue()
        {
            AutoResetEvent signal = new AutoResetEvent(false);
            var queue = new AzureQueueContext();            

            var job = new MockJobProcessor(1000, queue, signal);
            Thread thread = new Thread(() =>
                                           {
                                               job.Run();
                                           });
            thread.Start();

            signal.WaitOne(5000);
            job.Stop();
            
            // no job Processed
            Assert.AreEqual(false, job.Processed);
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