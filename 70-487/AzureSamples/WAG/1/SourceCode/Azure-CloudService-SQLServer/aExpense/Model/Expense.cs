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
    using System.Collections.Generic;

    public class Expense
    {
        public Expense()
        {
            this.Details = new List<ExpenseItem>();
        }

        public Guid Id { get; set; }

        public User User { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public bool Approved { get; set; }

        public string CostCenter { get; set; }

        public ReimbursementMethod ReimbursementMethod { get; set; }

        public List<ExpenseItem> Details { get; set; }

        public double Total { get; set; }

        public string ApproverName { get; set; }

        public string Description { get; set; }
    }
}