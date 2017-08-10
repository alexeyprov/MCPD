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
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Permissions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.ServiceRuntime;

    public abstract class JobWorkerRole : RoleEntryPoint
    {
        private bool keepRunning;
        protected IEnumerable<IJobProcessor> Processors { get; set; }
        protected List<Task> Tasks { get; set; }

        public override void Run()
        {
            this.keepRunning = true;
            this.Processors = this.CreateJobProcessors();
            this.Tasks = new List<Task>();

            foreach (var processor in this.Processors)
            {
                var t = Task.Factory.StartNew(processor.Run);
                this.Tasks.Add(t);
            }

            // Control and restart a faulted job
            while (this.keepRunning)
            {
                for (int i = 0; i < this.Tasks.Count; i++)
                {
                    var task = this.Tasks[i];
                    if (task.IsFaulted)
                    {
                        // Observe unhandled exception
                        if (task.Exception != null)
                        {
                            Log.Write(EventKind.Error, "Job threw an exception: " + task.Exception.InnerException.Message);
                        }
                        else
                        {
                            Log.Write(EventKind.Error, "Job Failed and no exception thrown.");
                        }

                        var jobToRestart = this.Processors.ElementAt(i);
                        this.Tasks[i] = Task.Factory.StartNew(jobToRestart.Run);
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand)]
        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;
            RoleEnvironment.Changing += this.RoleEnvironmentChanging;

            return base.OnStart();
        }

        public override void OnStop()
        {
            this.keepRunning = false;

            foreach (var job in this.Processors)
            {
                job.Stop();
                var disposable = job as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            try
            {
                Task.WaitAll(this.Tasks.ToArray());
            }
            catch (AggregateException ex)
            {
                // Observe any unhandled exceptions.
                Log.Write(EventKind.Error, "Finalizing exception thrown: {0} exceptions", ex.InnerExceptions.Count);
            }
        }

        protected abstract IEnumerable<IJobProcessor> CreateJobProcessors();

        private void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                e.Cancel = true;
            }
        }
    }
}