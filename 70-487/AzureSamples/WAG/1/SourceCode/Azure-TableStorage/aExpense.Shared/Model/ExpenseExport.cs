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

    public class ExpenseExport
    {
        public DateTime ApproveDate { get; set; }
        
        public string ApproverName { get; set; }

        public string CostCenter { get; set; }

        public ExpenseKey ExpenseId { get; set; }

        public ReimbursementMethod ReimbursementMethod { get; set; }
        
        public double TotalAmount { get; set; }
        
        public string UserName { get; set; }
    }
}