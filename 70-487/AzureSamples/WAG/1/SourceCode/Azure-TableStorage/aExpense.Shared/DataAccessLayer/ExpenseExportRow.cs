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
    using Microsoft.WindowsAzure.StorageClient;
    using Model;

    public class ExpenseExportRow : TableServiceEntity
    {
        private string id;
        private DateTime approveDate;

        public ExpenseExportRow()
        {
        }

        public ExpenseExportRow(string partitionKey, string rowKey)
            : base(partitionKey, rowKey)
        {
        }

        public string ExpenseId
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.RowKey = this.id;
            }
        }

        public DateTime ApproveDate
        {
            get
            {
                return this.approveDate;
            }

            set
            {
                this.approveDate = value;
                this.PartitionKey = this.approveDate.ToExpenseExportKey();
            }
        }

        public string UserName { get; set; }

        public string CostCenter { get; set; }

        public string ApproverName { get; set; }

        public string ReimbursementMethod { get; set; }

        public double TotalAmount { get; set; }
    }
}