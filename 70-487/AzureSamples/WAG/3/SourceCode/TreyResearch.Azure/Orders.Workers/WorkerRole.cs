//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Diagnostics;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Orders.Shared.Helpers;
    using Orders.Workers.Jobs;

    public class WorkerRole : RoleEntryPoint
    {
        private readonly IEnumerable<IJob> jobs;
        private readonly List<Task> tasks;
        private bool keepRunning;

        public WorkerRole()
        {
            this.tasks = new List<Task>();
            this.jobs = this.CreateJobProcessors();
        }

        public override void Run()
        {
            this.keepRunning = true;

            // Start the jobs
            foreach (var job in this.jobs)
            {
                var t = Task.Factory.StartNew(job.Run);
                this.tasks.Add(t);
            }

            // Control and restart a faulted job
            while (this.keepRunning)
            {
                for (int i = 0; i < this.tasks.Count; i++)
                {
                    var task = this.tasks[i];
                    if (task.IsFaulted)
                    {
                        // Observe unhandled exception
                        if (task.Exception != null)
                        {
                            TraceHelper.TraceError("Job threw an exception: " + task.Exception.InnerException.Message);
                        }
                        else
                        {
                            TraceHelper.TraceError("Job Failed and no exception thrown.");
                        }

                        var jobToRestart = this.jobs.ElementAt(i);
                        this.tasks[i] = Task.Factory.StartNew(jobToRestart.Run);
                    }
                }

                Thread.Sleep(TimeSpan.FromSeconds(30));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Not needed in this code.")]
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            RoleEnvironment.Changing += RoleEnvironmentChanging;
            RoleEnvironment.Changed += RoleEnvironmentChanged;

            ConfigureTraceListener(RoleEnvironment.GetConfigurationSettingValue("TraceEventTypeFilter"));

            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
                                                                 configSetter(RoleEnvironment.GetConfigurationSettingValue(configName)));

            return base.OnStart();
        }

        public override void OnStop()
        {
            this.keepRunning = false;

            foreach (var job in this.jobs)
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
                Task.WaitAll(this.tasks.ToArray());
            }
            catch (AggregateException ex)
            {
                // Observe any unhandled exceptions.
                TraceHelper.TraceError("Finalizing exception thrown: {0} exceptions", ex.InnerExceptions.Count);
            }

            base.OnStop();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification = "Not needed for this code")]
        private static void ConfigureTraceListener(string traceEventTypeFilter)
        {
            SourceLevels sourceLevels;
            if (Enum.TryParse(traceEventTypeFilter, true, out sourceLevels))
            {
                TraceHelper.Configure(sourceLevels);
            }
        }

        private static void RoleEnvironmentChanging(object sender, RoleEnvironmentChangingEventArgs e)
        {
            if (e.Changes.Any(change => change is RoleEnvironmentConfigurationSettingChange))
            {
                e.Cancel = true;
            }
        }

        private static void RoleEnvironmentChanged(object sender, RoleEnvironmentChangedEventArgs e)
        {
            // configure trace listener for any changes to EnableTableStorageTraceListener 
            if (e.Changes.OfType<RoleEnvironmentConfigurationSettingChange>().Any(change => change.ConfigurationSettingName == "TraceEventTypeFilter"))
            {
                ConfigureTraceListener(RoleEnvironment.GetConfigurationSettingValue("TraceEventTypeFilter"));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This is disposed in OnStop.")]
        private IEnumerable<IJob> CreateJobProcessors()
        {
            return new IJob[]
                       {
                           new NewOrderJob(), 
                           new StatusUpdateJob()
                       };
        }
    }
}
