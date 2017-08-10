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
    using System.IO;
    using System.Threading;
    using AExpense.DataAccessLayer;
    using AExpense.Model;
    using AExpense.QueueMessages;
    using AExpense.Queues;
    using Jobs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReceiptThumbnailJobFixture
    {
        private const string DatabaseName = "aExpense.Workers.FixtureTest";
        private static string testDatabaseConnectionString;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            testDatabaseConnectionString = CloudConfiguration.GetConnectionString("aExpense");

            ApplicationStorageInitializer.Initialize();
            var queue = new AzureQueueContext();
            queue.Purge<NewReceiptMessage>();
            
            using (var db = new ExpensesDataContext(testDatabaseConnectionString))
            {
                if (db.DatabaseExists())
                {
                    db.DeleteDatabase();
                }

                db.CreateDatabase();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            using (var db = new ExpensesDataContext(testDatabaseConnectionString))
            {
                if (db.DatabaseExists())
                {
                    var cmd = string.Format(System.Globalization.CultureInfo.InvariantCulture, "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", DatabaseName);
                    db.ExecuteCommand(cmd);
                    db.DeleteDatabase();
                }
            }
        }

        [TestMethod]
        [DeploymentItem(@".\aExpense.Workers.Tests\Functional\receipt1.png")]
        public void NewReceiptMessageShouldCreateThumbnailsOutOfReceiptImage()
        {
            // arrange
            var expenseId = new Guid("{00000000-0000-0000-0000-000000000000}");
            var expenseItemId = new Guid("A5F45728-5C3A-48f9-8F0E-000000000001");
            var receipt = File.ReadAllBytes("receipt1.png");
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

        private static void CreateExpense(Guid expenseId, Guid item1, Guid item2, byte[] receipt)
        {
            var stubUser = new User { UserName = "Domain\\UserName" };
            var expense = new Model.Expense
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

            expense.Details.Add(new ExpenseItem { Id = item1, Description = "description", Amount = 200, Receipt = receipt });
            expense.Details.Add(new ExpenseItem { Id = item2, Description = "description", Amount = 200 });

            var repository = new ExpenseRepository(testDatabaseConnectionString, TimeSpan.FromMinutes(20));
            repository.SaveExpense(expense);
        } 
       
        private static void CleanupExpenseDataFromAllTablesAndBlobs(ExpenseKey expenseKey, Guid item1)
        {
            var account = CloudConfiguration.GetStorageAccount("DataConnectionString");

            var storage = new ExpenseReceiptStorage();
            storage.DeleteReceipt("thumbnails/" + item1.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(item1.ToString().ToLowerInvariant() + ".jpg");
            storage.DeleteReceipt(item1.ToString().ToLowerInvariant());
        }
    }
}