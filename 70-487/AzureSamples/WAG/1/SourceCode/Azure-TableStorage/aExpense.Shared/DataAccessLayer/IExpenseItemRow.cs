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

    public interface IExpenseItemRow : IRow
    {
        // Guid has to be Nullable to run it in the storage emulator.
        // The same applies for all types that are not nullable like bool and DateTime.
        Guid? ItemId { get; set; }
        string Description { get; set; }
        double? Amount { get; set; }
        string ReceiptUrl { get; set; }
        string ReceiptThumbnailUrl { get; set; }
    }
}