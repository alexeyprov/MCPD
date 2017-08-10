//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Workers.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AExpense.Jobs;
    using AExpense.Shared;
    using DataAccessLayer;
    using Model;

    public class ExpenseExportBuilderJob : JobProcessor
    {
        private readonly ExpenseExportRepository expenseExports;
        private readonly ExpenseExportStorage exportStorage;

        public ExpenseExportBuilderJob() : base(100000)
        {
            this.expenseExports = new ExpenseExportRepository();
            this.exportStorage = new ExpenseExportStorage();
        }

        protected override void RunCore()
        {
            DateTime jobDate = DateTime.UtcNow;
            string name = jobDate.ToExpenseExportKey();

            IEnumerable<ExpenseExport> exports = this.expenseExports.Retreive(jobDate);
            if (exports == null || exports.Count() == 0)
            {
                return;   
            }

            string text = this.exportStorage.GetExport(name);
            var exportText = new StringBuilder(text);
            foreach (ExpenseExport expenseExport in exports)
            {
                exportText.AppendLine(expenseExport.ToCsvLine());
            }

            this.exportStorage.AddExport(name, exportText.ToString(), "text/plain");

            // delete the exports
            foreach (ExpenseExport exportToDelete in exports)
            {
                try
                {
                    this.expenseExports.Delete(exportToDelete);
                }
                catch (InvalidOperationException ex)
                {
                    Log.Write(EventKind.Error, ex.TraceInformation());
                }    
            }
        }
    }
}