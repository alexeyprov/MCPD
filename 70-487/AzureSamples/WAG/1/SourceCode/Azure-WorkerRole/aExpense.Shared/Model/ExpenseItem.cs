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
    using System.Linq;
    using System.Text;
    
    [Serializable]
    public class ExpenseItem
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public byte[] Receipt { get; set; }

        public Uri ReceiptUrl { get; set; }

        public Uri ReceiptThumbnailUrl { get; set; }
    }
}
