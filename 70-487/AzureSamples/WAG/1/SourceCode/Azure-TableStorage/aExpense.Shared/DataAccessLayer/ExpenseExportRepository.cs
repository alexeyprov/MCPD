//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using Model;

    public class ExpenseExportRepository
    {
        private readonly CloudStorageAccount account;
        private readonly Microsoft.Practices.TransientFaultHandling.RetryPolicy storageRetryPolicy;

        public ExpenseExportRepository()
        {
            this.account = CloudConfiguration.GetStorageAccount("DataConnectionString");
            this.storageRetryPolicy = RetryPolicyFactory.GetDefaultAzureStorageRetryPolicy();
        }

        public void Save(ExpenseExport expenseExport)
        {
            var context = new ExpenseDataContext(this.account);
            ExpenseExportRow expenseRow = expenseExport.ToTableEntity();

            context.AddObject(ExpenseDataContext.ExpenseExportTable, expenseRow);
            this.storageRetryPolicy.ExecuteAction(() => context.SaveChanges());
        }

        public IEnumerable<ExpenseExport> Retreive(DateTime jobDate)
        {
            var context = new ExpenseDataContext(this.account);
            string compareDate = jobDate.ToExpenseExportKey();
            var query = (from export in context.ExpenseExport
                         where export.PartitionKey.CompareTo(compareDate) <= 0
                         select export).AsTableServiceQuery();

            var val = this.storageRetryPolicy.ExecuteAction(() => query.Execute());
            return val.Select(e => e.ToModel()).ToList();
        }

        public void Delete(ExpenseExport expenseExport)
        {
            var context = new ExpenseDataContext(this.account);
            var query = (from export in context.ExpenseExport
                         where export.PartitionKey.CompareTo(expenseExport.ApproveDate.ToExpenseExportKey()) == 0 && export.RowKey.CompareTo(expenseExport.ExpenseId.ToString()) == 0
                         select export).AsTableServiceQuery();
            ExpenseExportRow row = this.storageRetryPolicy.ExecuteAction(() => query.Execute()).SingleOrDefault();
            if (row == null)
            {
                return;
            }

            context.DeleteObject(row);
            this.storageRetryPolicy.ExecuteAction(() => context.SaveChanges());
        }
    }
}