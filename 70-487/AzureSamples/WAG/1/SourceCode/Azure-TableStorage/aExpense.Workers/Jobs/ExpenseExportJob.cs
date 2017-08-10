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
    using System.Data.Services.Client;
    using System.Linq;
    using System.Net;
    using AExpense.DataAccessLayer;
    using AExpense.Jobs;
    using AExpense.Model;
    using AExpense.QueueMessages;
    using AExpense.Shared;
    using AExpense.Shared.Queues;

    [CLSCompliant(false)]
    public class ExpenseExportJob : BaseJobProcessor<ApprovedExpenseMessage>
    {
        private readonly ExpenseExportRepository expenseExports;
        private readonly ExpenseRepository expenses;

        public ExpenseExportJob()
            : base(2000, new AzureQueueContext())
        {
            this.expenses = new ExpenseRepository();
            this.expenseExports = new ExpenseExportRepository();
        }

        public override bool ProcessMessage(ApprovedExpenseMessage message)
        {
            try
            {
                Expense expense = this.expenses.GetExpenseById(new ExpenseKey(message.ExpenseId));

                if (expense == null)
                {
                    return false;
                }

                // if the expense was not updated but a message was persisted, we need to delete it
                if (!expense.Approved)
                {
                    return true;
                }

                double totalToPay = expense.Details.Sum(x => x.Amount);
                var export = new ExpenseExport
                                           {
                                               ApproveDate = message.ApproveDate,
                                               ApproverName = expense.ApproverName,
                                               CostCenter = expense.CostCenter,
                                               ExpenseId = expense.Id,
                                               ReimbursementMethod = expense.ReimbursementMethod,
                                               TotalAmount = totalToPay,
                                               UserName = expense.User.UserName
                                           };
                this.expenseExports.Save(export);
            }
            catch (InvalidOperationException ex)
            {
                var innerEx = ex.InnerException as DataServiceClientException;
                if (innerEx != null && innerEx.StatusCode == (int)HttpStatusCode.Conflict)
                {
                    // the data already exists so we can return true because we have processed this before
                    return true;
                }

                Log.Write(EventKind.Error, ex.TraceInformation());
                return false;
            }

            return true;
        }
    }
}