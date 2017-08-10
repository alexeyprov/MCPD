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
    using System.Data.Services.Client;
    using System.Globalization;
    using System.Linq;
    using AExpense.Model;
    using AExpense.QueueMessages;
    using AExpense.Shared.Queues;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public class ExpenseRepository
    {
        private readonly CloudStorageAccount account;
        private readonly ExpenseReceiptStorage receiptStorage;
        private readonly TimeSpan sharedSignatureValiditySpan;
        private readonly Microsoft.Practices.TransientFaultHandling.RetryPolicy storageRetryPolicy;

        public ExpenseRepository()
            : this(TimeSpan.FromMinutes(20))
        {
        }

        public ExpenseRepository(TimeSpan sharedSignatureValiditySpan)
        {
            this.account = CloudConfiguration.GetStorageAccount("DataConnectionString");
            this.receiptStorage = new ExpenseReceiptStorage();
            this.sharedSignatureValiditySpan = sharedSignatureValiditySpan;
            this.storageRetryPolicy = RetryPolicyFactory.GetDefaultAzureStorageRetryPolicy();
        }

        public static string EncodePartitionAndRowKey(string key)
        {
            if (key == null)
            {
                return null;
            }

            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(key));
        }

        public static string DecodePartitionAndRowKey(string encodedKey)
        {
            if (encodedKey == null)
            {
                return null;
            }

            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedKey));
        }

        public void SaveExpense(Expense expense)
        {
            var context = new ExpenseDataContext(this.account);
            IExpenseRow expenseRow = expense.ToTableEntity();
            expenseRow.PartitionKey = ExpenseRepository.EncodePartitionAndRowKey(expenseRow.UserName);
            expenseRow.RowKey = expense.Id.ToString();
            context.AddObject(ExpenseDataContext.ExpenseTable, expenseRow);

            foreach (var expenseItem in expense.Details)
            {
                // create row
                var expenseItemRow = expenseItem.ToTableEntity();
                expenseItemRow.PartitionKey = expenseRow.PartitionKey;
                expenseItemRow.RowKey = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", expense.Id, expenseItemRow.ItemId);
                context.AddObject(ExpenseDataContext.ExpenseTable, expenseItemRow);

                // save receipt image if any
                if (expenseItem.Receipt != null && expenseItem.Receipt.Length > 0)
                {
                    this.receiptStorage.AddReceipt(expenseItemRow.ItemId.ToString(), expenseItem.Receipt, string.Empty);
                }
            }

            // Save expense header and items header in the same batch transaction
            this.storageRetryPolicy.ExecuteAction(() => context.SaveChanges(SaveChangesOptions.Batch));

            var queue = new AzureQueueContext(this.account);
            expense.Details.ToList().ForEach(i => queue.AddMessage(new NewReceiptMessage { ExpenseItemId = i.Id.ToString() }));
        }

        public IEnumerable<Expense> GetExpensesForApproval(string approverName)
        {
            var context = new ExpenseDataContext(this.account) { MergeOption = MergeOption.NoTracking };

            var query = (from expense in context.ExpensesAndExpenseItems
                         where expense.ApproverName.CompareTo(approverName) == 0
                         select expense).AsTableServiceQuery();

            try
            {
                return this.storageRetryPolicy.ExecuteAction(() => query.Execute()).Select(e => (e as IExpenseRow).ToModel()).ToList();
            }
            catch (InvalidOperationException)
            {
                Log.Write(EventKind.Error, "By calling ToList(), this exception can be handled inside the repository.");
                throw;
            }
        }

        public IEnumerable<Expense> GetExpensesByUser(string userName)
        {
            var context = new ExpenseDataContext(this.account) { MergeOption = MergeOption.NoTracking };

            /// The Take(10) is not intended as a paging mechanism.
            /// It was added to improve the performance of the application.
            /// Using the partition key in the query will improve the performance
            /// because the partition key is indexed.
            var query = (from expense in context.ExpensesAndExpenseItems
                         where
                            expense.UserName.CompareTo(userName) == 0 &&
                            expense.PartitionKey.CompareTo(EncodePartitionAndRowKey(userName)) == 0
                         select expense).Take(10).AsTableServiceQuery();

            try
            {
                return this.storageRetryPolicy.ExecuteAction(() => query.Execute()).Select(e => (e as IExpenseRow).ToModel()).ToList();
            }
            catch (InvalidOperationException)
            {
                Log.Write(EventKind.Error, "By calling ToList(), this exception can be handled inside the repository.");
                throw;
            }
        }

        public Expense GetExpenseById(ExpenseKey expenseId)
        {
            var context = new ExpenseDataContext(this.account) { MergeOption = MergeOption.NoTracking };

            char charAfterSeparator = Convert.ToChar((Convert.ToInt32('_') + 1));
            var nextId = expenseId.ToString() + charAfterSeparator;

            var query = (from expenseItem in context.ExpensesAndExpenseItems
                         where
                            expenseItem.RowKey.CompareTo(expenseId.ToString()) >= 0 &&
                            expenseItem.RowKey.CompareTo(nextId) < 0
                         select expenseItem).AsTableServiceQuery();

            var expenseAndItemRows = this.storageRetryPolicy.ExecuteAction(() => query.Execute()).ToList();
            IExpenseRow expenseRow = expenseAndItemRows.SingleOrDefault(e => e.Kind == TableRows.Expense.ToString());

            if (expenseRow == null)
            {
                return null;
            }

            var expense = expenseRow.ToModel();

            expenseAndItemRows
                .Where(e => e.Kind == TableRows.ExpenseItem.ToString())
                .Select(e => (e as IExpenseItemRow).ToModel())
                .ToList().ForEach(e => expense.Details.Add(e));

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

        public void UpdateApproved(Expense expense)
        {
            var context = new ExpenseDataContext(this.account);

            IExpenseRow expenseRow = this.GetExpenseRowById(context, expense.Id);
            expenseRow.Approved = expense.Approved;

            var queue = new AzureQueueContext(this.account);
            this.storageRetryPolicy.ExecuteAction(() => queue.AddMessage(new ApprovedExpenseMessage { ExpenseId = expense.Id.ToString(), ApproveDate = DateTime.UtcNow }));

            context.UpdateObject(expenseRow);
            this.storageRetryPolicy.ExecuteAction(() => context.SaveChanges());
        }

        public void UpdateExpenseItemImages(string expenseItemId, string imageUri, string thumbnailUri)
        {
            var context = new ExpenseDataContext(this.account);

            // this query would work faster if we specify PartitionKey and RowKey
            // For simplicity we'll just do a table scan by expense item id
            var query = (from expenseItemRow in context.ExpensesAndExpenseItems
                         where expenseItemRow.ItemId == new Guid(expenseItemId)
                         select expenseItemRow).AsTableServiceQuery();

            var item = this.storageRetryPolicy.ExecuteAction(() => query.Execute()).SingleOrDefault();

            item.ReceiptUrl = imageUri;
            item.ReceiptThumbnailUrl = thumbnailUri;

            context.UpdateObject(item);
            this.storageRetryPolicy.ExecuteAction(() => context.SaveChanges());
        }

        private IExpenseRow GetExpenseRowById(ExpenseDataContext context, ExpenseKey expenseId)
        {
            var query = (from expense in context.ExpensesAndExpenseItems
                         where expense.RowKey == expenseId.ToString()
                         select expense).AsTableServiceQuery();

            return this.storageRetryPolicy.ExecuteAction(() => query.Execute()).SingleOrDefault();
        }

        private void DeleteExpenseById(ExpenseDataContext context, ExpenseKey id)
        {
            IExpenseRow expenseRow = this.GetExpenseRowById(context, id);

            if (expenseRow != null)
            {
                context.DeleteObject(expenseRow);
            }

            this.storageRetryPolicy.ExecuteAction(() => context.SaveChanges());
        }
    }
}