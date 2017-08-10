//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Workers.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using AExpense.DataAccessLayer;
    using AExpense.Model;
    using AExpense.QueueMessages;
    using AExpense.Shared;
    using AExpense.Shared.Queues;
    using Jobs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.StorageClient;

    [TestClass]
    public class ReceiptThumbnailJobFixture
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            ApplicationStorageInitializer.Initialize();
            var queue = new AzureQueueContext();
            queue.Purge<NewReceiptMessage>();
        }

        [TestMethod]
        [DeploymentItem(@".\aExpense.Workers.Tests\Functional\receipt1.png")]
        public void NewReceiptMessageShouldCreateThumbnailsOutOfReceiptImage()
        {
            // arrange
            var expenseId = new ExpenseKey("0000000000000000023");
            var expenseItemId = new Guid("A5F45728-5C3A-48f9-8F0E-000000000001");
            var receipt = File.ReadAllBytes("receipt1.png");
            CleanupExpenseDataFromAllTablesAndBlobs(expenseId, expenseItemId);
            CreateExpense(expenseId, expenseItemId, new Guid("A65FF4A8-5465-4798-8B46-3EDBBA1DE36E"), receipt);

            // act
            var worker = new Thread(() =>
            {
                var job = new ReceiptThumbnailJob();
                job.Run();
            });

            // assert
            var signal = new AutoResetEvent(false);
            byte[] thumb = null;
            byte[] large = null;
            byte[] original = null;

            var check = new Thread(() =>
            {
                while (true)
                {
                    var storage = new ExpenseReceiptStorage();
                    thumb = storage.GetReceipt(Path.Combine("thumbnails", expenseItemId.ToString().ToLowerInvariant() + ".jpg"));
                    large = storage.GetReceipt(expenseItemId.ToString().ToLowerInvariant() + ".jpg");
                    original = storage.GetReceipt(expenseItemId.ToString().ToLowerInvariant());
                    if (thumb != null && large != null && original == null)
                    {
                        signal.Set();
                        break;
                    }

                    Thread.Sleep(1000);
                }
            });

            check.Start();
            worker.Start();

            signal.WaitOne(10000);

            Assert.IsNull(original, "The original image should have been deleted");
            Assert.IsNotNull(thumb, "If we got here in null, the job is not doing its job");
            Assert.IsTrue(thumb.Length > 0);
            Assert.IsNotNull(large);
            Assert.IsTrue(large.Length > 0);
        }

        private static void CreateExpense(ExpenseKey expenseId, Guid item1, Guid item2, byte[] receipt)
        {
            var stubUser = new User { UserName = "Domain\\UserName" };
            var expense = new Expense
            {
                Id = expenseId,
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = stubUser,
                ApproverName = "ApproverName"
            };
            expense.Details.Add(new ExpenseItem { Id = item1, Receipt = receipt });
            expense.Details.Add(new ExpenseItem { Id = item2 });

            var repository = new ExpenseRepository();
            repository.SaveExpense(expense);
        } 
       
        private static void CleanupExpenseDataFromAllTablesAndBlobs(ExpenseKey expenseKey, Guid item1)
        {
            var account = CloudConfiguration.GetStorageAccount("DataConnectionString");
            var context = new ExpenseDataContext(account);
            AzureStorageHelper.DeleteExpenseAndItemsById(context, expenseKey);

            var storage = new ExpenseReceiptStorage();
            storage.DeleteReceipt("thumbnails/" + item1.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(item1.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(item1.ToString().ToLowerInvariant());
        }

        private static class AzureStorageHelper
        {
            public static IEnumerable<ExpenseAndExpenseItemRow> GetExpenseAndExpenseItemsById(ExpenseDataContext context, ExpenseKey expenseId)
            {
                char charAfterSeparator = Convert.ToChar((Convert.ToInt32('_') + 1));
                var nextId = expenseId.ToString() + charAfterSeparator;

                var query = (from expenseItem in context.ExpensesAndExpenseItems
                             where
                                expenseItem.RowKey.CompareTo(expenseId.ToString()) >= 0 &&
                                expenseItem.RowKey.CompareTo(nextId) < 0
                             select expenseItem).AsTableServiceQuery();

                return query.Execute();
            }

            public static IExpenseRow GetExpenseById(ExpenseDataContext context, ExpenseKey expenseId)
            {
                var query = (from expenseItem in context.ExpensesAndExpenseItems
                             where
                                expenseItem.RowKey.CompareTo(expenseId.ToString()) == 0
                             select expenseItem).AsTableServiceQuery();

                return query.Execute().SingleOrDefault();
            }

            public static void DeleteExpenseAndItemsById(ExpenseDataContext context, ExpenseKey id)
            {
                var expenseAndExpenseItemRows = GetExpenseAndExpenseItemsById(context, id);
                expenseAndExpenseItemRows.ToList().ForEach(e => context.DeleteObject(e));
                context.SaveChanges();
            }

            private static IEnumerable<IExpenseItemRow> GetExpenseItemsByExpenseId(ExpenseDataContext context, ExpenseKey expenseId)
            {
                char charAfterSeparator = Convert.ToChar((Convert.ToInt32('_') + 1));
                var nextId = expenseId.ToString() + charAfterSeparator;

                var query = (from expenseItem in context.ExpensesAndExpenseItems
                             where
                                expenseItem.RowKey.CompareTo(expenseId.ToString()) > 0 &&
                                expenseItem.RowKey.CompareTo(nextId) < 0
                             select expenseItem).AsTableServiceQuery();

                return query.Execute();
            }
        }
    }
}