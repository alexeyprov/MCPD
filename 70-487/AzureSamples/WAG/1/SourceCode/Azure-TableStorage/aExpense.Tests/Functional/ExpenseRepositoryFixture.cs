//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using AExpense.DataAccessLayer;
    using AExpense.Model;
    using AExpense.Shared;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using QueueMessages;
    using Shared.Queues;

    [TestClass]
    public class ExpenseRepositoryFixture
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            ApplicationStorageInitializer.Initialize();
        }

        [TestMethod]
        public void SaveExpense()
       { 
            var expenseContext = AzureStorageHelper.GetContext();

            ExpenseKey expenseIdToSave = new ExpenseKey("0000000000000000001");
            Guid expenseItemId1ToSave = new Guid("{A059D3C0-608A-45f7-B2C0-000000000001}");
            Guid expenseItemId2ToSave = new Guid("{A059D3C0-608A-45f7-B2C0-000000000002}");
            
            var stubUser = new User { UserName = "Domain\\UserName" };
            var expenseToSave = new Expense
                                    {
                                        Id = expenseIdToSave,
                                        Date = new DateTime(1900, 01, 01),
                                        Title = "Title",
                                        ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                                        CostCenter = "CostCenter",
                                        Approved = true,
                                        User = stubUser,
                                        ApproverName = "ApproverName"
                                    };
            expenseToSave.Details.Add(new ExpenseItem { Id = expenseItemId1ToSave, Description = "Description1", Amount = 1.1d, ReceiptThumbnailUrl = new Uri("http://ThumbnailReceiptUrl1"), ReceiptUrl = new Uri("http://ReceiptUrl1") });
            expenseToSave.Details.Add(new ExpenseItem { Id = expenseItemId2ToSave });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expenseIdToSave);

            var repository = new ExpenseRepository();
            repository.SaveExpense(expenseToSave);

            var expenseRow = AzureStorageHelper.GetExpenseById(expenseContext, expenseIdToSave);
            Assert.AreEqual(expenseToSave.Approved, expenseRow.Approved);
            Assert.AreEqual(expenseToSave.CostCenter, expenseRow.CostCenter);
            Assert.AreEqual(expenseToSave.Date, expenseRow.Date);
            Assert.AreEqual(expenseToSave.Id.ToString(), expenseRow.Id);
            Assert.AreEqual(expenseToSave.ReimbursementMethod, Enum.Parse(typeof(ReimbursementMethod), expenseRow.ReimbursementMethod));
            Assert.AreEqual(expenseToSave.Title, expenseRow.Title);
            Assert.AreEqual(stubUser.UserName, expenseRow.UserName);
            Assert.AreEqual(expenseToSave.ApproverName, expenseRow.ApproverName);
            Assert.AreEqual(TableRows.Expense.ToString(), expenseRow.Kind);
            Assert.AreEqual(expenseToSave.Id.ToString(), expenseRow.RowKey);
            Assert.AreEqual("Domain\\UserName", ExpenseRepository.DecodePartitionAndRowKey(expenseRow.PartitionKey));

            IEnumerable<IExpenseItemRow> expenseItemRowDetails = AzureStorageHelper.GetExpenseItemsByExpenseId(expenseContext, expenseIdToSave);
            Assert.AreEqual(2, expenseItemRowDetails.Count());
            var expenseItemRow1 = expenseItemRowDetails.Single(e => e.ItemId == expenseItemId1ToSave);
            Assert.AreEqual("Description1", expenseItemRow1.Description);
            Assert.AreEqual(1.1, expenseItemRow1.Amount);
            Assert.AreEqual("http://ThumbnailReceiptUrl1", expenseItemRow1.ReceiptThumbnailUrl);
            Assert.AreEqual("http://ReceiptUrl1", expenseItemRow1.ReceiptUrl);
            Assert.AreEqual("Domain\\UserName", ExpenseRepository.DecodePartitionAndRowKey(expenseItemRow1.PartitionKey));
            string expectedExpemseItemRow1Key = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", expenseIdToSave, expenseItemId1ToSave);
            Assert.AreEqual(expectedExpemseItemRow1Key, expenseItemRow1.RowKey);
            Assert.AreEqual(TableRows.ExpenseItem.ToString(), expenseItemRow1.Kind);
        }

        [TestMethod]
        public void GetAllExpensesForApproval()
        {
            var expenseContext = AzureStorageHelper.GetContext();

            var expected = new Expense
            {
                Id = new ExpenseKey("0000000000000000002"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "UserName" },
                ApproverName = "ExpectedApproverName"
            };
            expected.Details.Add(new ExpenseItem { Description = "Description", Amount = 1.0d, Id = new Guid("{865E14AF-39E0-47c5-8B66-000000000001}") });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expected.Id);

            var anotherExpense = new Expense
            {
                Id = new ExpenseKey("0000000000000000003"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "another user vName" },
                ApproverName = "ExpectedApproverName"
            };
            anotherExpense.Details.Add(new ExpenseItem { Description = "Description", Amount = 1.0d, Id = new Guid("{F0C04A70-5CA1-4c14-A54E-000000000001}") });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, anotherExpense.Id);

            var notForMeExpense = new Expense
            {
                Id = new ExpenseKey("0000000000000000004"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "another user vName" },
                ApproverName = "Another ApproverName"
            };
            notForMeExpense.Details.Add(new ExpenseItem { Description = "Description", Amount = 1.0d, Id = new Guid("{8499B90F-A7AA-45d9-B191-000000000001}") });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, notForMeExpense.Id);

            var repositoryHelper = new ExpenseRepository();
            repositoryHelper.SaveExpense(expected);
            repositoryHelper.SaveExpense(anotherExpense);
            repositoryHelper.SaveExpense(notForMeExpense);

            var repository = new ExpenseRepository();
            var expenses = repository.GetExpensesForApproval("ExpectedApproverName");

            Assert.IsNotNull(expenses);
            Assert.AreEqual(2, expenses.Count());
        }

        [TestMethod]
        public void GetAllExpensesByUser()
        {
            var expenseContext = AzureStorageHelper.GetContext();

            var expected = new Expense
                               {
                                   Id = new ExpenseKey("0000000000000000005"),
                                   Date = new DateTime(1900, 01, 01),
                                   Title = "Title",
                                   ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                                   CostCenter = "CostCenter",
                                   Approved = true,
                                   User = new User { UserName = "ExpectedUserName" },
                                   ApproverName = "ApproverName"
                               };
            expected.Details.Add(new ExpenseItem { Description = "Description", Amount = 1.0d, Id = new Guid("{AA53F859-17E3-48a0-B6F0-000000000001}") });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expected.Id);
            var repositoryHelper = new ExpenseRepository();
            repositoryHelper.SaveExpense(expected);

            var repository = new ExpenseRepository();
            var expenses = repository.GetExpensesByUser("ExpectedUserName");

            Assert.AreEqual(1, expenses.Count());
            var actual = expenses.Single(e => e.Id == expected.Id);
            Assert.AreEqual("ExpectedUserName", actual.User.UserName);
        }

        [TestMethod]
        public void ShouldGetZeroExpensesFromNonExistingUser()
        {
            var expenseContext = AzureStorageHelper.GetContext();

            var expected = new Expense
            {
                Id = new ExpenseKey("0000000000000000006"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "UserName" },
                ApproverName = "ApproverName"
            };
            expected.Details.Add(new ExpenseItem { Description = "Description", Amount = 1.0d, Id = new Guid("{496D3D59-C1FC-4f0b-A6D1-000000000001}") });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expected.Id);
            var repositoryHelper = new ExpenseRepository();
            repositoryHelper.SaveExpense(expected);

            var repository = new ExpenseRepository();
            var expenses = repository.GetExpensesByUser("non existing user name");

            Assert.AreEqual(0, expenses.Count());
        }

        [TestMethod]
        public void ApproveExpense()
        {
            var expenseContext = AzureStorageHelper.GetContext();

            var expected = new Expense
            {
                Id = new ExpenseKey("0000000000000000007"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = false,
                User = new User { UserName = "UserName" },
                ApproverName = "ApproverName"
            };

            expected.Details.Add(new ExpenseItem { Description = "Description", Amount = 1.0d, Id = new Guid("{912EEDD7-F0F6-4dd5-9021-000000000001}") });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expected.Id);
            var repositoryHelper = new ExpenseRepository();
            repositoryHelper.SaveExpense(expected);

            var repository = new ExpenseRepository();
            repository.UpdateApproved(expected);

            var actual = AzureStorageHelper.GetExpenseById(expenseContext, expected.Id);
            Assert.AreEqual(expected.Approved, actual.Approved);
        }

        [TestMethod]
        [DeploymentItem(@".\aExpense.Tests\Functional\receipt1.png")]
        public void GetExpenseById()
        {
            var expenseContext = AzureStorageHelper.GetContext();

            var expected = new Expense
            {
                Id = new ExpenseKey("0000000000000000008"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "UserName" },
                ApproverName = "ApproverName"
            };
            byte[] receiptBytes = File.ReadAllBytes("receipt1.png");
            expected.Details.Add(new ExpenseItem { Id = new Guid("{0BDBE400-2DC4-4e94-91ED-000000000001}"), Description = "Description1", Amount = 1.1d, ReceiptThumbnailUrl = new Uri("http://ThumbnailReceiptUrl1"), ReceiptUrl = new Uri("http://ReceiptUrl1"), Receipt = receiptBytes });
            expected.Details.Add(new ExpenseItem { Id = new Guid("{0BDBE400-2DC4-4e94-91ED-000000000002}"), ReceiptThumbnailUrl = new Uri("http://ThumbnailReceiptUrl1"), ReceiptUrl = new Uri("http://ReceiptUrl1"), Receipt = receiptBytes });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expected.Id);

            var anotherExpense = new Expense
            {
                Id = new ExpenseKey("0000000000000000009"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "another user vName" },
                ApproverName = "ApproverName"
            };
            anotherExpense.Details.Add(new ExpenseItem { Description = "Description", Amount = 1.0d, Id = new Guid("{125FB718-3317-4328-BE1D-000000000001}") });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, anotherExpense.Id);
            
            var queue = new AzureQueueContext();
            queue.Purge<NewReceiptMessage>();
            var repositoryHelper = new ExpenseRepository();
            repositoryHelper.SaveExpense(expected);
            repositoryHelper.SaveExpense(anotherExpense);
            var receiptThumbnailJob = new AExpense.Workers.Jobs.ReceiptThumbnailJob();
            var message = new NewReceiptMessage();
            message.ExpenseItemId = new Guid("{0BDBE400-2DC4-4e94-91ED-000000000001}").ToString();
            receiptThumbnailJob.ProcessMessage(message);
            receiptThumbnailJob = new AExpense.Workers.Jobs.ReceiptThumbnailJob();
            message = new NewReceiptMessage();
            message.ExpenseItemId = new Guid("{0BDBE400-2DC4-4e94-91ED-000000000002}").ToString();
            receiptThumbnailJob.ProcessMessage(message);

            var repository = new ExpenseRepository();
            var actual = repository.GetExpenseById(expected.Id);

            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CostCenter, actual.CostCenter);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.ReimbursementMethod, actual.ReimbursementMethod);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.User.UserName, actual.User.UserName);
            Assert.AreEqual(expected.ApproverName, actual.ApproverName);

            Assert.AreEqual(2, actual.Details.Count());
            var expenseItemRow1 = actual.Details.Single(e => e.Id == new Guid("{0BDBE400-2DC4-4e94-91ED-000000000001}"));
            Assert.AreEqual("Description1", expenseItemRow1.Description);
            Assert.AreEqual(1.1, expenseItemRow1.Amount);
        }

        [TestMethod]
        [DeploymentItem(@".\aExpense.Tests\Functional\receipt1.png")]
        public void GetExpenseByIdReturnsAValidReceiptThumbprintUrlInTheExpenseItem()
        {
            var expenseContext = AzureStorageHelper.GetContext();

            var expected = new Expense
            {
                Id = new ExpenseKey("0000000000000000010"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "UserName" },
                ApproverName = "ApproverName"
            };
            byte[] receiptBytes = File.ReadAllBytes("receipt1.png");
            expected.Details.Add(new ExpenseItem { Id = new Guid("{5D931ABF-74CA-4f20-94FA-000000000001}"), Description = "Description1", Amount = 1.1d, Receipt = receiptBytes });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expected.Id);
            var storage = new ExpenseReceiptStorage();
            storage.DeleteReceipt("thumbnails/" + expected.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(expected.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(expected.ToString().ToLowerInvariant());

            var queue = new AzureQueueContext();
            queue.Purge<NewReceiptMessage>();
            var repositoryHelper = new ExpenseRepository();
            repositoryHelper.SaveExpense(expected);
            var receiptThumbnailJob = new AExpense.Workers.Jobs.ReceiptThumbnailJob();
            var message = new NewReceiptMessage();
            message.ExpenseItemId = new Guid("{5D931ABF-74CA-4f20-94FA-000000000001}").ToString();
            receiptThumbnailJob.ProcessMessage(message);

            var repository = new ExpenseRepository();
            var actual = repository.GetExpenseById(expected.Id);
            
            var expenseItem1 = actual.Details.ElementAt(0);
            var response = WebRequest.Create(expenseItem1.ReceiptThumbnailUrl).GetResponse();
            Assert.AreEqual("image/jpeg", response.ContentType);
        }

        [TestMethod]
        [DeploymentItem(@".\aExpense.Tests\Functional\receipt1.png")]
        public void GetExpenseByIdReturnsAValidReceiptUrlInTheExpenseItem()
        {
            var expenseContext = AzureStorageHelper.GetContext();

            var expected = new Expense
            {
                Id = new ExpenseKey("0000000000000000011"),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "UserName" },
                ApproverName = "ApproverName"
            };
            byte[] receiptBytes = File.ReadAllBytes("receipt1.png");
            expected.Details.Add(new ExpenseItem { Id = new Guid("{6CE719B2-17D3-4b20-A85A-000000000001}"), Description = "Description1", Amount = 1.1d, Receipt = receiptBytes });
            AzureStorageHelper.DeleteExpenseAndItemsById(expenseContext, expected.Id);
            var storage = new ExpenseReceiptStorage();
            storage.DeleteReceipt("thumbnails/" + expected.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(expected.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(expected.ToString().ToLowerInvariant());

            var queue = new AzureQueueContext();
            queue.Purge<NewReceiptMessage>();
            var repositoryHelper = new ExpenseRepository();
            repositoryHelper.SaveExpense(expected);
            var receiptThumbnailJob = new AExpense.Workers.Jobs.ReceiptThumbnailJob();
            var message = new NewReceiptMessage();
            message.ExpenseItemId = new Guid("{6CE719B2-17D3-4b20-A85A-000000000001}").ToString();
            receiptThumbnailJob.ProcessMessage(message);

            var repository = new ExpenseRepository();
            var actual = repository.GetExpenseById(expected.Id);

            var expenseItem1 = actual.Details.ElementAt(0);
            var response = WebRequest.Create(expenseItem1.ReceiptUrl).GetResponse();
            Assert.AreEqual("image/jpeg", response.ContentType);
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

            public static ExpenseDataContext GetContext()
            {
                CloudStorageAccount account = CloudConfiguration.GetStorageAccount("DataConnectionString");
                return new ExpenseDataContext(account);
            }

            public static IEnumerable<IExpenseItemRow> GetExpenseItemsByExpenseId(ExpenseDataContext context, ExpenseKey expenseId)
            {
                char charAfterSeparator = Convert.ToChar((Convert.ToInt32('_') + 1));
                var nextId = expenseId.ToString() + charAfterSeparator;
                var query = (from expenseItem in context.ExpensesAndExpenseItems
                             where
                                expenseItem.RowKey.CompareTo(expenseId.ToString() + "_") >= 0 &&
                                expenseItem.RowKey.CompareTo(nextId) < 0
                             select expenseItem).AsTableServiceQuery();

                return query.Execute().ToList();
            }

            private static IEnumerable<ExpenseExportRow> GetExpenseReportsbyExpenseId(ExpenseDataContext context, ExpenseKey expenseId)
            {   
                var query = (from exportRow in context.ExpenseExport
                             where
                                exportRow.RowKey.CompareTo(expenseId.ToString()) == 0
                             select exportRow).AsTableServiceQuery();

                return query.Execute().ToList();
            }
        }
    }
}