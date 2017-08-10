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
    using System.Configuration;
    using System.Linq;
    using AExpense.QueueMessages;
    using AExpense.Queues;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using Model;

    public class ExpenseRepository
    {
        private readonly CloudStorageAccount account;
        private readonly string expenseDatabaseConnectionString;
        private readonly ExpenseReceiptStorage receiptStorage;
        private readonly TimeSpan sharedSignatureValiditySpan;
        private readonly Microsoft.Practices.TransientFaultHandling.RetryPolicy sqlCommandRetryPolicy;
        private readonly Microsoft.Practices.TransientFaultHandling.RetryPolicy storageRetryPolicy;

        public ExpenseRepository()
            : this(CloudConfiguration.GetConnectionString("aExpense"), TimeSpan.FromMinutes(20))
        {
        }

        public ExpenseRepository(TimeSpan sharedSignatureValiditySpan)
            : this(CloudConfiguration.GetConnectionString("aExpense"), sharedSignatureValiditySpan)
        {
        }

        public ExpenseRepository(string expenseDatabaseConnectionString, TimeSpan sharedSignatureValiditySpan)
        {
            this.expenseDatabaseConnectionString = expenseDatabaseConnectionString;
            this.account = CloudConfiguration.GetStorageAccount("DataConnectionString");
            this.receiptStorage = new ExpenseReceiptStorage();
            this.sharedSignatureValiditySpan = sharedSignatureValiditySpan;
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
            this.storageRetryPolicy = RetryPolicyFactory.GetDefaultAzureStorageRetryPolicy();
        }

        public void SaveExpense(Model.Expense expense)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var entity = expense.ToEntity();
                db.Expenses.InsertOnSubmit(entity);

                foreach (var detail in expense.Details)
                {
                    var detailEntity = detail.ToEntity(expense);
                    db.ExpenseDetails.InsertOnSubmit(detailEntity);

                    if (detail.Receipt != null && detail.Receipt.Length > 0)
                    {
                        this.receiptStorage.AddReceipt(detail.Id.ToString(), detail.Receipt, string.Empty);
                    }
                }

                this.sqlCommandRetryPolicy.ExecuteAction(() => db.SubmitChanges());

                var queue = new AzureQueueContext(this.account);
                expense.Details.ToList().ForEach(i => queue.AddMessage(new NewReceiptMessage { ExpenseItemId = i.Id.ToString() }));
            }
        }

        public IEnumerable<Model.Expense> GetAllExpenses()
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                return this.sqlCommandRetryPolicy.ExecuteAction<IEnumerable<Model.Expense>>(() =>
                        (from e in db.Expenses
                        select e.ToModel()).ToList());
            }
        }

        public IEnumerable<Model.Expense> GetExpensesForApproval(string approverName)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                return this.sqlCommandRetryPolicy.ExecuteAction<IEnumerable<Model.Expense>>(() =>
                        (from e in db.Expenses
                         where e.Approver == approverName
                         select e.ToModel()).ToList());
            }
        }

        public IEnumerable<Model.Expense> GetExpensesByUser(string userName)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                return this.sqlCommandRetryPolicy.ExecuteAction<IEnumerable<Model.Expense>>(() =>
                        (from e in db.Expenses
                        where e.UserName == userName
                        select e.ToModel()).ToList());
            }
        }

        public Model.Expense GetExpenseById(Guid expenseId)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var entity = this.sqlCommandRetryPolicy.ExecuteAction<Expense>(() =>
                        (from e in db.Expenses
                        where e.Id == expenseId
                        select e).SingleOrDefault());

                var expense = entity.ToModel();

                // Substract 5 minutes from the current time to avoid clock skew issues
                var policy = new SharedAccessPolicy
                {
                    Permissions = SharedAccessPermissions.Read,
                    SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5),
                    SharedAccessExpiryTime = DateTime.UtcNow + this.sharedSignatureValiditySpan
                };

                var client = this.account.CreateCloudBlobClient();
                var container = this.storageRetryPolicy.ExecuteAction(() => client.GetContainerReference(ExpenseReceiptStorage.ReceiptContainerName));
                foreach (var item in expense.Details)
                {
                    if (item.ReceiptUrl != null)
                    {
                        CloudBlob receiptBlob = this.storageRetryPolicy.ExecuteAction(() => container.GetBlobReference(item.ReceiptUrl.ToString()));
                        item.ReceiptUrl = new Uri(item.ReceiptUrl.AbsoluteUri + receiptBlob.GetSharedAccessSignature(policy));
                    }
                    else
                    {
                        item.ReceiptUrl = new Uri("/Styling/Images/no_receipt.png", UriKind.Relative);
                    }

                    if (item.ReceiptThumbnailUrl != null)
                    {
                        CloudBlob receiptThumbnailBlob = this.storageRetryPolicy.ExecuteAction(() => container.GetBlobReference(item.ReceiptThumbnailUrl.ToString()));
                        item.ReceiptThumbnailUrl = new Uri(item.ReceiptThumbnailUrl.AbsoluteUri + receiptThumbnailBlob.GetSharedAccessSignature(policy));
                    }
                    else
                    {
                        item.ReceiptThumbnailUrl = new Uri("/Styling/Images/no_receipt.png", UriKind.Relative);
                    }
                }

                return expense;
            }
        }

        public void UpdateExpenseItemImages(string expenseItemId, string imageUri, string thumbnailUri)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var detail = this.sqlCommandRetryPolicy.ExecuteAction<ExpenseDetail>(() =>
                        (from d in db.ExpenseDetails
                         where d.Id == Guid.Parse(expenseItemId)
                         select d).SingleOrDefault());

                detail.ReceiptUrl = imageUri;
                detail.ReceiptThumbnailUrl = thumbnailUri;

                this.sqlCommandRetryPolicy.ExecuteAction(() => db.SubmitChanges());
            }
        }

        public void UpdateApproved(Model.Expense expense)
        {
            using (var db = new ExpensesDataContext(this.expenseDatabaseConnectionString))
            {
                var expenseToUpdate = this.sqlCommandRetryPolicy.ExecuteAction<Expense>(() => db.Expenses.Single(e => e.Id == expense.Id));
                expenseToUpdate.Approved = expense.Approved;
                this.sqlCommandRetryPolicy.ExecuteAction(() => db.SubmitChanges());
            }
        }        
    }
}