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
    
    //// This class is queried for properties from ExpenseRow or ExpenseItemRow
    public class ExpenseAndExpenseItemRow : Row, IExpenseRow, IExpenseItemRow
    {
        public ExpenseAndExpenseItemRow()
        {
        }

        public ExpenseAndExpenseItemRow(TableRows rowKind)
        {
            this.Kind = rowKind.ToString();
        }

        // Properties from ExpenseRow
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool? Approved { get; set; }
        public string ApproverName { get; set; }
        public string CostCenter { get; set; }
        public DateTime? Date { get; set; }
        public string ReimbursementMethod { get; set; }
        public string Title { get; set; }

        // Properties from ExpenseItemRow
        public Guid? ItemId { get; set; }
        public string Description { get; set; }
        public double? Amount { get; set; }
        public string ReceiptUrl { get; set; }
        public string ReceiptThumbnailUrl { get; set; }
    }
}