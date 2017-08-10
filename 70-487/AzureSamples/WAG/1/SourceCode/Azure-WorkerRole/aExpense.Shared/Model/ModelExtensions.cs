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
    using System.Linq;

    public static class ModelExtensions
    {
        public static Model.Expense ToModel(this DataAccessLayer.Expense entity)
        {
            var expense = new Expense
            {
                Id = entity.Id,
                Approved = entity.Approved,
                CostCenter = entity.CostCenter,
                Date = entity.Date,
                ReimbursementMethod = (ReimbursementMethod)Enum.Parse(typeof(ReimbursementMethod), entity.ReimbursementMethod),
                Title = entity.Title,
                User = new User { UserName = entity.UserName },
                ApproverName = entity.Approver,
                Total = Convert.ToDouble(entity.Amount),
                Description = entity.Description,
                Details = entity.ExpenseDetails.Select(i => i.ToModel()).ToList()
            };

            return expense;
        }

        public static ExpenseItem ToModel(this DataAccessLayer.ExpenseDetail entity)
        {
            var expenseItem = new ExpenseItem
            {
                Id = entity.Id,
                Amount = Convert.ToDouble(entity.Amount),
                Description = entity.Description,
                ReceiptUrl = string.IsNullOrEmpty(entity.ReceiptUrl) ? null : new Uri(entity.ReceiptUrl),
                ReceiptThumbnailUrl = string.IsNullOrEmpty(entity.ReceiptThumbnailUrl) ? null : new Uri(entity.ReceiptThumbnailUrl)
            };

            return expenseItem;
        }

        public static DataAccessLayer.Expense ToEntity(this Model.Expense model)
        {
            var expense = new DataAccessLayer.Expense
            {
                Id = model.Id,
                Approved = model.Approved,
                CostCenter = model.CostCenter,
                Date = model.Date,
                ReimbursementMethod = Enum.GetName(typeof(ReimbursementMethod), model.ReimbursementMethod),
                Title = model.Title,
                UserName = model.User.UserName,
                Approver = model.ApproverName,
                Amount = Convert.ToDecimal(model.Details.Sum(d => d.Amount)),
                Description = string.Empty
            };

            return expense;
        }

        public static DataAccessLayer.ExpenseDetail ToEntity(this Model.ExpenseItem model, Model.Expense expense)
        {
            var expenseItem = new DataAccessLayer.ExpenseDetail
            {
                Id = model.Id,
                Description = model.Description,
                Amount = Convert.ToDecimal(model.Amount),
                ExpenseId = expense.Id
            };

            return expenseItem;
        }
    }
}