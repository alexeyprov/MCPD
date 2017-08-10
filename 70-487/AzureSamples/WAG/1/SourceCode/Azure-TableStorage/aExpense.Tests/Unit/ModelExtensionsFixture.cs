//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Tests.Unit
{
    using System;
    using DataAccessLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    [TestClass]
    public class ModelExtensionsFixture
    {
        [TestMethod]
        public void IdIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual("0000000000000000000", expenseEntity.Id);
        }

        [TestMethod]
        public void ApprovedIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                Approved = true,
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual(true, expenseEntity.Approved);
        }

        [TestMethod]
        public void CostCenterIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                CostCenter = "Cost Center",
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual("Cost Center", expenseEntity.CostCenter);
        }

        [TestMethod]
        public void DateIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expectedDate = new DateTime(2000, 1, 1);
            var expense = new Expense
            {
                Date = expectedDate,
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual(expectedDate, expenseEntity.Date);
        }

        [TestMethod]
        public void ReimbursementMethodIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                ReimbursementMethod = ReimbursementMethod.Cash,
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual(Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.Cash), expenseEntity.ReimbursementMethod);
        }

        [TestMethod]
        public void TitleIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                Title = "Title",
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual("Title", expenseEntity.Title);
        }

        [TestMethod]
        public void UserNameIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                User = new User { UserName = "User Name" },
                Id = new ExpenseKey("0000000000000000000")
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual("User Name", expenseEntity.UserName);
        }

        [TestMethod]
        public void ApproverIsCopiedWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                ApproverName = "Approver Name",
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual("Approver Name", expenseEntity.ApproverName);
        }

        [TestMethod]
        public void KindIsSetWhenConvertingAnExpenseToAnExpenseRow()
        {
            var expense = new Expense
            {
                Id = new ExpenseKey("0000000000000000000"),
                User = new User()
            };

            var expenseEntity = expense.ToTableEntity();

            Assert.AreEqual(TableRows.Expense.ToString(), expenseEntity.Kind);
        }

        [TestMethod]
        public void IdIsCopiedWhenConvertingAnExpenseEntityToAnExpense()
        {
            var expectedId = new ExpenseKey("0000000000000000000");
            IExpenseRow expenseEntity = new ExpenseAndExpenseItemRow
            {
                Id = expectedId.ToString(),
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.NotSet)
            };

            var expense = expenseEntity.ToModel();

            Assert.AreEqual(expectedId, expense.Id);
        }

        [TestMethod]
        public void ApprovedIsCopiedWhenConvertingAnExpenseEntityToAnExpense()
        {
            IExpenseRow expenseEntity = new ExpenseAndExpenseItemRow
            {
                Approved = true,
                Id = "0000000000000000000",
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.NotSet)
            };

            var expense = expenseEntity.ToModel();

            Assert.AreEqual(true, expense.Approved);
        }

        [TestMethod]
        public void CostCenterIsCopiedWhenConvertingAnExpenseEntityToAnExpense()
        {
            IExpenseRow expenseEntity = new ExpenseAndExpenseItemRow
            {
                CostCenter = "Cost Center",
                Id = "0000000000000000000",
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.NotSet)
            };

            var expense = expenseEntity.ToModel();

            Assert.AreEqual("Cost Center", expense.CostCenter);
        }

        [TestMethod]
        public void DateIsCopiedWhenConvertingAnExpenseEntityToAnExpense()
        {
            var expectedDate = new DateTime(2000, 1, 1);
            IExpenseRow expenseEntity = new ExpenseAndExpenseItemRow
            {
                Date = expectedDate,
                Id = "0000000000000000000",
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.NotSet)
            };

            var expense = expenseEntity.ToModel();

            Assert.AreEqual(expectedDate, expense.Date);
        }

        [TestMethod]
        public void ReimbursementMethodIsCopiedWhenConvertingAnExpenseEntityToAnExpense()
        {
            IExpenseRow expenseEntity = new ExpenseAndExpenseItemRow
            {
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.Cash),
                Id = "0000000000000000000"
            };

            var expense = expenseEntity.ToModel();

            Assert.AreEqual(ReimbursementMethod.Cash, expense.ReimbursementMethod);
        }

        [TestMethod]
        public void TitleIsCopiedWhenConvertingAnExpenseEntityToAnExpense()
        {
            IExpenseRow expenseEntity = new ExpenseAndExpenseItemRow
            {
                Title = "Title",
                Id = "0000000000000000000",
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.NotSet)
            };

            var expense = expenseEntity.ToModel();

            Assert.AreEqual("Title", expense.Title);
        }

        [TestMethod]
        public void UserNameIsCopiedWhenConvertingAnExpenseEntityToAnExpense()
        {
            IExpenseRow expenseEntity = new ExpenseAndExpenseItemRow
            {
                UserName = "UserName",
                Id = "0000000000000000000",
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), ReimbursementMethod.NotSet)
            };

            var expense = expenseEntity.ToModel();

            Assert.AreEqual("UserName", expense.User.UserName);
        }

        [TestMethod]
        public void AmountIsCopiedWhenConvertingAnExpenseItemEntityToAnExpenseItem()
        {
            IExpenseItemRow expenseItemEntity = new ExpenseAndExpenseItemRow
            {
                Amount = 1.0
            };

            ExpenseItem expenseItem = expenseItemEntity.ToModel();

            Assert.AreEqual(1.0, expenseItem.Amount);
        }

        [TestMethod]
        public void DescriptionIsCopiedWhenConvertingAnExpenseItemEntityToAnExpenseItem()
        {
            IExpenseItemRow expenseItemEntity = new ExpenseAndExpenseItemRow
            {
                Description = "Description"
            };

            ExpenseItem expenseItem = expenseItemEntity.ToModel();

            Assert.AreEqual("Description", expenseItem.Description);
        }

        [TestMethod]
        public void IdIsCopiedWhenConvertingAnExpenseItemEntityToAnExpenseItem()
        {
            var expectedId = new Guid("{00000000-0000-0000-0000-000000000000}");
            IExpenseItemRow expenseItemEntity = new ExpenseAndExpenseItemRow
            {
                ItemId = expectedId
            };

            ExpenseItem expenseItem = expenseItemEntity.ToModel();

            Assert.AreEqual(expectedId, expenseItem.Id);
        }

        [TestMethod]
        public void ReceiptThumbnailUrlIsCopiedWhenConvertingAnExpenseItemEntityToAnExpenseItem()
        {
            IExpenseItemRow expenseItemEntity = new ExpenseAndExpenseItemRow
            {
                ReceiptThumbnailUrl = "http://ReceiptThumbnailUrl"
            };

            ExpenseItem expenseItem = expenseItemEntity.ToModel();

            Assert.AreEqual(new Uri("http://ReceiptThumbnailUrl"), expenseItem.ReceiptThumbnailUrl);
        }

        [TestMethod]
        public void ReceiptUrlIsCopiedWhenConvertingAnExpenseItemEntityToAnExpenseItem()
        {
            IExpenseItemRow expenseItemEntity = new ExpenseAndExpenseItemRow
            {
                ReceiptUrl = "http://ReceiptUrl"
            };

            ExpenseItem expenseItem = expenseItemEntity.ToModel();

            Assert.AreEqual(new Uri("http://ReceiptUrl"), expenseItem.ReceiptUrl);
        }

        [TestMethod]
        public void AmountIsCopiedWhenConvertingAnExpenseItemToAnExpenseItemEntity()
        {
            var expenseItem = new ExpenseItem
            {
                Amount = 1.0
            };

            IExpenseItemRow expenseItemEntity = expenseItem.ToTableEntity();

            Assert.AreEqual(1.0, expenseItemEntity.Amount);
        }

        [TestMethod]
        public void DescriptionIsCopiedWhenConvertingAnExpenseItemToAnExpenseItemEntity()
        {
            var expenseItem = new ExpenseItem
            {
                Description = "Description"
            };

            IExpenseItemRow expenseItemEntity = expenseItem.ToTableEntity();

            Assert.AreEqual("Description", expenseItemEntity.Description);
        }

        [TestMethod]
        public void IdIsCopiedWhenConvertingAnExpenseItemToAnExpenseItemEntity()
        {
            var expectedId = new Guid("{00000000-0000-0000-0000-000000000000}");
            var expenseItem = new ExpenseItem
            {
                Id = expectedId
            };

            IExpenseItemRow expenseItemEntity = expenseItem.ToTableEntity();

            Assert.AreEqual(expectedId, expenseItemEntity.ItemId);
        }

        [TestMethod]
        public void ReceiptThumbnailUrlIsCopiedWhenConvertingAnExpenseItemToAnExpenseItemEntity()
        {
            var expenseItem = new ExpenseItem
            {
                ReceiptThumbnailUrl = new Uri("http://ReceiptThumbnailUrl")
            };

            IExpenseItemRow expenseItemEntity = expenseItem.ToTableEntity();

            Assert.AreSame("http://ReceiptThumbnailUrl", expenseItemEntity.ReceiptThumbnailUrl);
        }

        [TestMethod]
        public void ReceiptUrlIsCopiedWhenConvertingAnExpenseItemToAnExpenseItemEntity()
        {
            var expenseItem = new ExpenseItem
            {
                ReceiptUrl = new Uri("http://ReceiptUrl")
            };

            IExpenseItemRow expenseItemEntity = expenseItem.ToTableEntity();

            Assert.AreSame("http://ReceiptUrl", expenseItemEntity.ReceiptUrl);
        }

        [TestMethod]
        public void KindIsSetWhenConvertingAnExpenseItemToAnExpenseItemEntity()
        {
            var expenseItem = new ExpenseItem();

            IExpenseItemRow expenseItemEntity = expenseItem.ToTableEntity();

            Assert.AreSame(TableRows.ExpenseItem.ToString(), expenseItemEntity.Kind);
        }

        [TestMethod]
        public void ApproveDateIsCopiedWhenConvertingAnExpenseExportToAnExpenseExportEntity()
        {
            DateTime approveDate = DateTime.UtcNow;
            var expenseExport = new ExpenseExport
            {
                ApproveDate = approveDate
            };

            ExpenseExportRow expenseExportEntity = expenseExport.ToTableEntity();

            Assert.AreEqual(approveDate, expenseExportEntity.ApproveDate);
            Assert.AreEqual(approveDate.ToExpenseExportKey(), expenseExportEntity.PartitionKey);
        }

        [TestMethod]
        public void ApproverNameIsCopiedWhenConvertingAnExpenseExportToAnExpenseExportEntity()
        {
            var expenseExport = new ExpenseExport
            {
                ApproverName = "ApproverName"
            };

            ExpenseExportRow expenseExportEntity = expenseExport.ToTableEntity();

            Assert.AreEqual("ApproverName", expenseExportEntity.ApproverName);
        }

        [TestMethod]
        public void CostCenterIsCopiedWhenConvertingAnExpenseExportToAnExpenseExportEntity()
        {
            var expenseExport = new ExpenseExport
            {
                CostCenter = "12345"
            };

            ExpenseExportRow expenseExportEntity = expenseExport.ToTableEntity();

            Assert.AreEqual("12345", expenseExportEntity.CostCenter);
        }

        [TestMethod]
        public void ExpenseIdIsCopiedWhenConvertingAnExpenseExportToAnExpenseExportEntity()
        {
            var expenseExport = new ExpenseExport
            {
                ExpenseId = new ExpenseKey("0000000000000000000")
            };

            ExpenseExportRow expenseExportEntity = expenseExport.ToTableEntity();

            Assert.AreEqual("0000000000000000000", expenseExportEntity.ExpenseId);
            Assert.AreEqual("0000000000000000000", expenseExportEntity.RowKey);
        }

        [TestMethod]
        public void ReimbursementMethodIsCopiedWhenConvertingAnExpenseExportToAnExpenseExportEntity()
        {
            var expenseExport = new ExpenseExport
            {
                ReimbursementMethod = ReimbursementMethod.Check
            };

            ExpenseExportRow expenseExportEntity = expenseExport.ToTableEntity();

            Assert.AreEqual("Check", expenseExportEntity.ReimbursementMethod);
        }

        [TestMethod]
        public void TotalAmountIsCopiedWhenConvertingAnExpenseExportToAnExpenseExportEntity()
        {
            var expenseExport = new ExpenseExport
            {
                TotalAmount = 24.5
            };

            ExpenseExportRow expenseExportEntity = expenseExport.ToTableEntity();

            Assert.AreEqual(24.5, expenseExportEntity.TotalAmount);
        }

        [TestMethod]
        public void UserNameIsCopiedWhenConvertingAnExpenseExportToAnExpenseExportEntity()
        {
            var expenseExport = new ExpenseExport
            {
               UserName = "UserName"
            };

            ExpenseExportRow expenseExportEntity = expenseExport.ToTableEntity();

            Assert.AreEqual("UserName", expenseExportEntity.UserName);
        }

        [TestMethod]
        public void ApproveDateIsCopiedWhenConvertingAnExpenseExportEntityToAnExpenseExport()
        {
            DateTime approveDate = DateTime.UtcNow;
            var expenseExportEntity = new ExpenseExportRow
            {
                ApproveDate = approveDate
            };

            ExpenseExport expenseExport = expenseExportEntity.ToModel();

            Assert.AreEqual(approveDate, expenseExport.ApproveDate);
        }

        [TestMethod]
        public void ApproverNameIsCopiedWhenConvertingAnExpenseExportEntityToAnExpenseExport()
        {
            var expenseExportEntity = new ExpenseExportRow
            {
                ApproverName = "ApproverName"
            };

            ExpenseExport expenseExport = expenseExportEntity.ToModel();

            Assert.AreEqual("ApproverName", expenseExport.ApproverName);
        }

        [TestMethod]
        public void CostCenterIsCopiedWhenConvertingAnExpenseExportEntityToAnExpenseExport()
        {
            var expenseExportEntity = new ExpenseExportRow
            {
                CostCenter = "12345"
            };

            ExpenseExport expenseExport = expenseExportEntity.ToModel();

            Assert.AreEqual("12345", expenseExport.CostCenter);
        }

        [TestMethod]
        public void ExpenseIdIsCopiedWhenConvertingAnExpenseExportEntityToAnExpenseExport()
        {
            var expenseExportEntity = new ExpenseExportRow
            {
                ExpenseId = "0000000000000000000"
            };

            ExpenseExport expenseExport = expenseExportEntity.ToModel();

            Assert.AreEqual("0000000000000000000", expenseExport.ExpenseId.ToString());
        }

        [TestMethod]
        public void ReimbursementMethodIsCopiedWhenConvertingAnExpenseExportEntityToAnExpenseExport()
        {
            var expenseExportEnity = new ExpenseExportRow
            {
                ReimbursementMethod = "Check"
            };

            ExpenseExport expenseExport = expenseExportEnity.ToModel();

            Assert.AreEqual(ReimbursementMethod.Check, expenseExport.ReimbursementMethod);
        }

        [TestMethod]
        public void TotalAmountIsCopiedWhenConvertingAnExpenseExportEntityToAnExpenseExport()
        {
            var expenseExportEntity = new ExpenseExportRow
            {
                TotalAmount = 24.5
            };

            ExpenseExport expenseExport = expenseExportEntity.ToModel();

            Assert.AreEqual(24.5, expenseExport.TotalAmount);
        }

        [TestMethod]
        public void UserNameIsCopiedWhenConvertingAnExpenseExportEntityToAnExpenseExport()
        {
            var expenseExportEntity = new ExpenseExportRow
            {
                UserName = "UserName"
            };

            ExpenseExport expenseExport = expenseExportEntity.ToModel();

            Assert.AreEqual("UserName", expenseExport.UserName);
        }
    }
}