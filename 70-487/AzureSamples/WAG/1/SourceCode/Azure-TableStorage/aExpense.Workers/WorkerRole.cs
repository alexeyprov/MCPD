//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Workers
{
    using System.Collections.Generic;
    using AExpense.Jobs;
    using AExpense.Shared;
    using AExpense.Workers.Jobs;

    public class WorkerRole : JobWorkerRole
    {
        public override bool OnStart()
        {
            ApplicationStorageInitializer.Initialize();
            return base.OnStart();
        }

        protected override IEnumerable<IJobProcessor> CreateJobProcessors()
        {
            return new IJobProcessor[]
                       {
                           new ReceiptThumbnailJob(), 
                           new ExpenseExportJob(),
                           new ExpenseExportBuilderJob()
                       };
        }
    }
}
