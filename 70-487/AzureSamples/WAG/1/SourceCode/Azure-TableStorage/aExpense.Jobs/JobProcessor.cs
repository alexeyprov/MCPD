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
    using System.Threading;

    public abstract class JobProcessor : IJobProcessor
    {
        private bool keepRunning;

        protected JobProcessor(int sleepInterval)
        {
            if (sleepInterval <= 0)
            {
                throw new ArgumentOutOfRangeException("sleepInterval");
            }

            this.SleepInterval = sleepInterval;
        }

        protected int SleepInterval { get; set; }

        public void Run()
        {
            this.keepRunning = true;
            while (this.keepRunning)
            {
                Thread.Sleep(this.SleepInterval);
                this.RunCore();
            }
        }

        public void Stop()
        {
            this.keepRunning = false;
        }

        protected abstract void RunCore();
    }
}