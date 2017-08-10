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
    using System.Data.Services.Common;
    using Microsoft.WindowsAzure.StorageClient;

    [CLSCompliant(false)]
    public abstract class Row : TableServiceEntity, IRow
    {
        protected Row()
        {
        }

        protected Row(string kind) : this(null, null, kind)
        {
        }

        protected Row(string partitionKey, string rowKey, string kind)
            : base(partitionKey, rowKey)
        {
            this.Kind = kind;
        }

        public string Kind { get; set; }
    }
}