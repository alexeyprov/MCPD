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
    using System.Linq;
    using System.Threading;
    using AExpense.Shared;
    using AExpense.Shared.Queues;
    using DataAccessLayer;
    using Jobs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.StorageClient;
    using Model;
    using QueueMessages;

    [TestClass]
    public class ExpenseExportJobFixture
    {
        private const string CostCenter = "CostCenter";
        private const string UserName = "Domain\\UserName";
        private const string ApproverName = "ApproverName";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            ApplicationStorageInitializer.Initialize();
            var queue = new AzureQueueContext();
            queue.Purge<ApprovedExpenseMessage>();
        }

        [TestMethod]
        public void ApprovedExpenseShouldCreateExpenseExport()
        {
            // arrange
            var expenseId = new ExpenseKey("0000000000000000022");
            CleanupExpenseDataFromAllTables(expenseId);
            CreateExpense(expenseId, new Guid("A58704F8-B9AF-45b1-886D-74098C84C2EE"), new Guid("E0E07D0F-6401-4357-A64A-11D1B3B43B0A"), true);

            // act
            var worker = new Thread(() =>
            {
                var job = new ExpenseExportJob();
                job.Run();
            });

            // assert
            var signal = new AutoResetEvent(false);
            ExpenseExport export = null;

            var check = new Thread(() =>
            {
                while (true)
                {
                    var storage = new ExpenseExportRepository();
                    IEnumerable<ExpenseExport> expenseExports = storage.Retreive(DateTime.UtcNow);
                    export = expenseExports.Where(x => x.ExpenseId == expenseId).FirstOrDefault();
                    if (export != null)
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

            Assert.IsNotNull(export);
            Assert.AreEqual(ApproverName, export.ApproverName);
            Assert.AreEqual(CostCenter, export.CostCenter);
            Assert.AreEqual(expenseId, export.ExpenseId);
            Assert.AreEqual(UserName, export.UserName);
            Assert.AreEqual(2, export.TotalAmount);
        }

        [TestMethod]
        public void NotApprovedExpenseShouldNotCreateExpenseExportAndDeleteMessage()
        {
            // arrange
            var expenseId = new ExpenseKey("0000000000000000021");
            CleanupExpenseDataFromAllTables(expenseId);
            CreateExpense(expenseId, new Guid("EB3EB91B-E8EE-4091-8EEF-0BC5D8EC75E3"), new Guid("2ED269D0-0156-4af3-8BD5-F7A198C4FAD9"), false);
            var queue = new AzureQueueContext();
            DateTime approveDate = DateTime.UtcNow;
            queue.AddMessage(new ApprovedExpenseMessage { ExpenseId = "1", ApproveDate = approveDate });

            // act
            var worker = new Thread(() =>
            {
                var job = new ExpenseExportJob();
                job.Run();
            });

            // assert
            var signal = new AutoResetEvent(false);

            var check = new Thread(() =>
            {
                while (true)
                {
                    var message = queue.GetMessage<ApprovedExpenseMessage>();
                    if (message == null)
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

            ExpenseExportRow row = AzureStorageHelper.GetExpenseExportByExpenseId(expenseId);
            Assert.IsNull(row);
        }

        private static void CleanupExpenseDataFromAllTables(ExpenseKey expenseKey)
        {
            var account = CloudConfiguration.GetStorageAccount("DataConnectionString");
            var context = new ExpenseDataContext(account);
            AzureStorageHelper.DeleteExpenseAndItemsById(context, expenseKey);
            AzureStorageHelper.DeleteExpenseExports(context, expenseKey);
        }

        private static void CreateExpense(ExpenseKey expenseKey, Guid itemId1, Guid itemId2, bool approved)
        {
            var stubUser = new User { UserName = UserName };
            var expense = new Expense
            {
                Id = expenseKey,
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = CostCenter,
                Approved = approved,
                User = stubUser,
                ApproverName = ApproverName
            };
            expense.Details.Add(new ExpenseItem { Id = itemId1, Amount = 1 });
            expense.Details.Add(new ExpenseItem { Id = itemId2, Amount = 1 });

            var repository = new ExpenseRepository();
            repository.SaveExpense(expense);
            if (approved)
            {
                repository.UpdateApproved(expense);
            }
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

            public static IEnumerable<IExpenseItemRow> GetExpenseItemsByExpenseId(ExpenseDataContext context, ExpenseKey expenseId)
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

            public static void DeleteExpenseExports(ExpenseDataContext context, ExpenseKey id)
            {
                var query = (from expenseExport in context.ExpenseExport
                             where expenseExport.RowKey.CompareTo(id.ToString()) == 0
                             select expenseExport).AsTableServiceQuery();

                IEnumerable<ExpenseExportRow> exportRows = query.Execute();
                foreach (ExpenseExportRow row in exportRows)
                {
                    context.DeleteObject(row);
                    context.SaveChanges();
                }
            }

            public static ExpenseExportRow GetExpenseExportByExpenseId(ExpenseKey expenseId)
            {
                var account = CloudConfiguration.GetStorageAccount("DataConnectionString");
                var context = new ExpenseDataContext(account);

                var query = (from export in context.ExpenseExport
                             where export.RowKey.CompareTo(expenseId.ToString()) == 0
                             select export).AsTableServiceQuery();

                var val = query.Execute();
                return val.FirstOrDefault();
            }
        }
    }
}