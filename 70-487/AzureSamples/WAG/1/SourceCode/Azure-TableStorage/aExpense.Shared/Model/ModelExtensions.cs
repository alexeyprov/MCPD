//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Model
{
    using System;
    using System.Text;
    using DataAccessLayer;

    public static class ModelExtensions
    {
        public static Expense ToModel(this IExpenseRow entity)
        {
            var expense = new Expense
            {
                Id = new ExpenseKey(entity.Id),
                Approved = entity.Approved.HasValue ? entity.Approved.Value : default(bool),
                CostCenter = entity.CostCenter,
                Date = entity.Date.HasValue ? entity.Date.Value : default(DateTime),
                ReimbursementMethod = (ReimbursementMethod)Enum.Parse(typeof(ReimbursementMethod), entity.ReimbursementMethod),
                Title = entity.Title,
                User = new User { UserName = entity.UserName },
                ApproverName = entity.ApproverName
            };

            return expense;
        }

        public static string ToCsvLine(this ExpenseExport model)
        {
            return string.Format(
                "{0},{1},{2},{3},{4},{5},{6}", 
                model.ApproveDate, 
                model.ExpenseId, 
                model.ApproverName, 
                model.UserName, 
                model.CostCenter,
                Enum.GetName(
                    typeof(ReimbursementMethod), 
                model.ReimbursementMethod), 
                model.TotalAmount);
        }

        public static ExpenseExport ToModel(this ExpenseExportRow entity)
        {
            var expenseReport = new ExpenseExport
                                    {
                                        ApproveDate = entity.ApproveDate,
                                        ApproverName = entity.ApproverName,
                                        CostCenter = entity.CostCenter,
                                        ExpenseId = entity.ExpenseId == null ? ExpenseKey.Now : new ExpenseKey(entity.ExpenseId),
                                        ReimbursementMethod = entity.ReimbursementMethod == null ? ReimbursementMethod.NotSet : (ReimbursementMethod)Enum.Parse(typeof(ReimbursementMethod), entity.ReimbursementMethod),
                                        TotalAmount = entity.TotalAmount,
                                        UserName = entity.UserName
                                    };
            return expenseReport;
        }

        public static string ToExpenseExportKey(this DateTime model)
        {
            return model.ToString("yyyy-MM-dd");
        }

        public static ExpenseExportRow ToTableEntity(this ExpenseExport model)
        {
            var expenseExport = new ExpenseExportRow
                                    {
                                        ApproveDate = model.ApproveDate,
                                        ApproverName = model.ApproverName,
                                        CostCenter = model.CostCenter,
                                        ExpenseId = model.ExpenseId == null ? null : model.ExpenseId.ToString(),
                                        ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), model.ReimbursementMethod),
                                        UserName = model.UserName,
                                        TotalAmount = model.TotalAmount
                                    };

            return expenseExport;
        }

        public static IExpenseRow ToTableEntity(this Expense model)
        {
            var expense = new ExpenseAndExpenseItemRow(TableRows.Expense)
            {
                Id = model.Id == null ? null : model.Id.ToString(),
                Approved = model.Approved,
                CostCenter = model.CostCenter,
                Date = model.Date,
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), model.ReimbursementMethod),
                Title = model.Title,
                UserName = model.User.UserName,
                ApproverName = model.ApproverName
            };

            return expense;
        }

        public static IExpenseItemRow ToTableEntity(this ExpenseItem model)
        {
            var expenseItem = new ExpenseAndExpenseItemRow(TableRows.ExpenseItem)
            {
                ItemId = model.Id,
                Amount = model.Amount,
                Description = model.Description,
                ReceiptUrl = model.ReceiptUrl == null ? null : model.ReceiptUrl.OriginalString,
                ReceiptThumbnailUrl = model.ReceiptThumbnailUrl == null ? null : model.ReceiptThumbnailUrl.OriginalString
            };

            return expenseItem;
        }

        public static ExpenseItem ToModel(this IExpenseItemRow entity)
        {
            var expenseItem = new ExpenseItem
            {
                Id = entity.ItemId.HasValue ? entity.ItemId.Value : default(Guid),
                Amount = entity.Amount.HasValue ? entity.Amount.Value : default(double),
                Description = entity.Description,
                ReceiptUrl = string.IsNullOrEmpty(entity.ReceiptUrl) ? null : new Uri(entity.ReceiptUrl),
                ReceiptThumbnailUrl = string.IsNullOrEmpty(entity.ReceiptThumbnailUrl) ? null : new Uri(entity.ReceiptThumbnailUrl)
            };

            return expenseItem;
        }
    }
}