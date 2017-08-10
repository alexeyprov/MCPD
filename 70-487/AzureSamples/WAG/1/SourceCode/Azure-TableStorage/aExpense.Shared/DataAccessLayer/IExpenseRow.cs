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
    
    public interface IExpenseRow : IRow
    {
        string Id { get; set; }
        string UserName { get; set; }
        bool? Approved { get; set; }
        string ApproverName { get; set; }
        string CostCenter { get; set; }

        // DateTime has to be Nullable to run it in the storage emulator.
        // The same applies for all types that are not nullable like bool and Guid.
        DateTime? Date { get; set; }
        string ReimbursementMethod { get; set; }
        string Title { get; set; }
    }
}