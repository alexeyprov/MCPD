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
    using System.Linq;
    using DataAccessLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using Expense = DataAccessLayer.Expense;
    using ExpensesDataContext = DataAccessLayer.ExpensesDataContext;

    [TestClass]
    public class ExpenseRepositoryFixture
    {
        private const string DatabaseName = "aExpense.FixtureTest";
        private const string TestDatabaseConnectionString = @"Data Source=WAGSqlAlias;Initial Catalog=" + DatabaseName + ";Integrated Security=True";

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            using (var db = new ExpensesDataContext(TestDatabaseConnectionString))
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
            using (var db = new ExpensesDataContext(TestDatabaseConnectionString))
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
        public void SaveExpense()
        {
            Guid expenseIdToSave = new Guid("{00000000-0000-0000-0000-000000000000}");
            Assert.IsNull(DatabaseHelper.GetExpenseById(expenseIdToSave));
            var stubUser = new User { UserName = "user name" };
            var expenseToSave = new Model.Expense
                                    {
                                        Id = expenseIdToSave,
                                        Date = new DateTime(1900, 01, 01),
                                        Title = "Title",
                                        Description = "Description",
                                        Total = 2000,
                                        ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                                        CostCenter = "CostCenter",
                                        Approved = true,
                                        User = stubUser,
                                        ApproverName = "ApproverName",
                                        Details = CreateDetails()
                                    };

            var repository = new ExpenseRepository(TestDatabaseConnectionString, TimeSpan.FromMinutes(20));
            repository.SaveExpense(expenseToSave);

            var expenseEntity = DatabaseHelper.GetExpenseById(expenseIdToSave);
            Assert.IsNotNull(expenseEntity);
            Assert.AreEqual(expenseToSave.Total, Convert.ToDouble(expenseEntity.Amount));
            Assert.AreEqual(expenseToSave.Approved, expenseEntity.Approved);
            Assert.AreEqual(expenseToSave.CostCenter, expenseEntity.CostCenter);
            Assert.AreEqual(expenseToSave.Date, expenseEntity.Date);
            Assert.AreEqual(expenseToSave.Id, expenseEntity.Id);
            Assert.AreEqual(expenseToSave.ReimbursementMethod, Enum.Parse(typeof(ReimbursementMethod), expenseEntity.ReimbursementMethod));
            Assert.AreEqual(expenseToSave.Title, expenseEntity.Title);
            Assert.AreEqual(expenseToSave.User.UserName, expenseEntity.UserName);
            Assert.AreEqual(expenseToSave.ApproverName, expenseEntity.Approver);
        }

        [TestMethod]
        public void GetAllExpenses()
        {
            var expected = new Model.Expense
                               {
                                   Id = Guid.NewGuid(),
                                   Date = new DateTime(1900, 01, 01),
                                   Title = "Title",
                                   Description = "Description",
                                   Total = 2000,
                                   ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                                   CostCenter = "CostCenter",
                                   Approved = true,
                                   User = new User { UserName = "user name" },
                                   ApproverName = "ApproverName",
                                   Details = CreateDetails()
                               };
            DatabaseHelper.CreateExpense(expected);

            var repository = new ExpenseRepository(TestDatabaseConnectionString, TimeSpan.FromMinutes(20));
            var expenses = repository.GetAllExpenses();

            Assert.IsNotNull(expenses);
            var actual = expenses.ToList().Where(e => e.Id == expected.Id).FirstOrDefault();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Total, actual.Total);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CostCenter, actual.CostCenter);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.ReimbursementMethod, actual.ReimbursementMethod);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.User.UserName, actual.User.UserName);
        }

        [TestMethod]
        public void GetAllExpensesForApproval()
        {
            var expected = new Model.Expense
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                Description = "Description",
                Total = 2000,
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "user Name" },
                ApproverName = "ExpectedApproverName",
                Details = CreateDetails()
            };

            var anotherExpense = new Model.Expense
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                Description = "Description",
                Total = 2000,
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "another user vName" },
                ApproverName = "ExpectedApproverName",
                Details = CreateDetails()
            };

            var notForMeExpense = new Model.Expense
            {
                Id = Guid.NewGuid(),
                Date = new DateTime(1900, 01, 01),
                Title = "Title",
                Description = "Description",
                Total = 2000,
                ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                CostCenter = "CostCenter",
                Approved = true,
                User = new User { UserName = "another user vName" },
                ApproverName = "Another ApproverName",
                Details = CreateDetails()
            };
            DatabaseHelper.CreateExpense(expected);
            DatabaseHelper.CreateExpense(anotherExpense);
            DatabaseHelper.CreateExpense(notForMeExpense);

            var repository = new ExpenseRepository(TestDatabaseConnectionString, TimeSpan.FromMinutes(20));
            var expenses = repository.GetExpensesForApproval("ExpectedApproverName");

            Assert.IsNotNull(expenses);
            Assert.AreEqual(2, expenses.Count());
        }

        [TestMethod]
        public void GetAllExpensesByUser()
        {
            var expected = new Model.Expense
                               {
                                   Id = Guid.NewGuid(),
                                   Date = new DateTime(1900, 01, 01),
                                   Title = "Title",
                                   Description = "Description",
                                   Total = 2000,
                                   ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                                   CostCenter = "CostCenter",
                                   Approved = true,
                                   User = new User { UserName = "user name" },
                                   ApproverName = "ApproverName",
                                   Details = CreateDetails()
                               };

            DatabaseHelper.CreateExpense(expected);

            var repository = new ExpenseRepository(TestDatabaseConnectionString, TimeSpan.FromMinutes(20));
            var expenses = repository.GetExpensesByUser("user name");

            Assert.IsNotNull(expenses);
            var actual = expenses.ToList().Where(e => e.Id == expected.Id).FirstOrDefault();
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Total, actual.Total);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CostCenter, actual.CostCenter);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.ReimbursementMethod, actual.ReimbursementMethod);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.User.UserName, actual.User.UserName);
        }

        [TestMethod]
        public void ShouldGetZeroExpensesFromNonExistingUser()
        {
            var expected = new Model.Expense
                               {
                                   Id = Guid.NewGuid(),
                                   Date = new DateTime(1900, 01, 01),
                                   Title = "Title",
                                   Description = "Description",
                                   Total = 2000,
                                   ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                                   CostCenter = "CostCenter",
                                   Approved = true,
                                   User = new User { UserName = "user name" },
                                   ApproverName = "ApproverName",
                                   Details = CreateDetails()
                               };
            DatabaseHelper.CreateExpense(expected);

            var repository = new ExpenseRepository(TestDatabaseConnectionString, TimeSpan.FromMinutes(20));
            var expenses = repository.GetExpensesByUser("no existing user name");

            Assert.AreEqual(0, expenses.Count());
        }

        [TestMethod]
        public void ApproveExpense()
        {
            var expected = new Model.Expense
                               {
                                   Id = Guid.NewGuid(),
                                   Date = new DateTime(1900, 01, 01),
                                   Title = "Title",
                                   Description = "Description",
                                   Total = 2000,
                                   ReimbursementMethod = ReimbursementMethod.DirectDeposit,
                                   CostCenter = "CostCenter",
                                   Approved = false,
                                   User = new User { UserName = "user name" },
                                   ApproverName = "ApproverName",
                                   Details = CreateDetails()
                               };
            DatabaseHelper.CreateExpense(expected);

            var repository = new ExpenseRepository(TestDatabaseConnectionString, TimeSpan.FromMinutes(20));
            repository.UpdateApproved(expected);

            var actual = DatabaseHelper.GetExpenseById(expected.Id);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Approved, actual.Approved);
        }

        private static System.Collections.Generic.List<ExpenseItem> CreateDetails()
        {
            return new System.Collections.Generic.List<ExpenseItem> { new ExpenseItem { Amount = 2000, Id = Guid.NewGuid(), Description = "detail" } };
        }

        private static class DatabaseHelper
        {
            public static Expense GetExpenseById(Guid expenseId)
            {
                using (var db = new ExpensesDataContext(TestDatabaseConnectionString))
                {
                    return db.Expenses.SingleOrDefault(e => e.Id == expenseId);
                }
            }

            public static void CreateExpense(Model.Expense expense)
            {
                using (var db = new ExpensesDataContext(TestDatabaseConnectionString))
                {
                    db.Expenses.InsertOnSubmit(expense.ToEntity());

                    foreach (var d in expense.Details)
                    {
                        db.ExpenseDetails.InsertOnSubmit(d.ToEntity(expense));
                    }

                    db.SubmitChanges();
                }
            }
        }
    }
}