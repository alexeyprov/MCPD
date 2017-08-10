//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Jobs
{
    using System;
    using System.Globalization;
    using Queues;

    [CLSCompliant(false)]
    public abstract class BaseJobProcessor<T> : JobProcessor where T : BaseQueueMessage
    {
        protected BaseJobProcessor(int sleepInterval, IQueueContext queue) : base(sleepInterval)
        {
            if (queue == null)
            {
                throw new ArgumentNullException("queue");
            }

            this.Queue = queue;
        }

        public event EventHandler OnIdle;

        protected IQueueContext Queue { get; set; }

        protected int MessagesProcessed { get; set; }

        protected bool RetrieveMultiple { get; set; }

        protected int RetrieveMultipleMaxMessages { get; set; }

        public abstract bool ProcessMessage(T message);

        protected override void RunCore()
        {
            if (this.RetrieveMultiple)
            {
                var messages = this.Queue.GetMultipleMessages<T>(this.RetrieveMultipleMaxMessages);
                if (messages != null)
                {
                    foreach (var message in messages)
                    {
                        this.ProcessMessageCore(message);
                    }
                }
                else
                {
                    this.OnEmptyQueue();
                }
            }
            else
            {
                var message = this.Queue.GetMessage<T>();
                if (message != null)
                {
                    this.ProcessMessageCore(message);
                }
                else
                {
                    this.OnEmptyQueue();
                }
            }
        }

        protected void WriteToLog(EventKind level, string message, params object[] args)
        {
            Log.Write(level, message, args);
        }

        private void OnEmptyQueue()
        {
            if (this.MessagesProcessed > 0)
            {
                this.WriteToLog(EventKind.Information, string.Format(CultureInfo.InvariantCulture, "* {0} * Processed messages: {1}", this.GetType().Name, this.MessagesProcessed));
                this.MessagesProcessed = 0;
            }

            if (this.OnIdle != null)
            {
                this.OnIdle(this, EventArgs.Empty);
            }
        }

        private void ProcessMessageCore(T message)
        {
            var processed = this.ProcessMessage(message);
            if (processed)
            {
                this.Queue.DeleteMessage(message);
                this.MessagesProcessed++;
            }
        }
    }
}