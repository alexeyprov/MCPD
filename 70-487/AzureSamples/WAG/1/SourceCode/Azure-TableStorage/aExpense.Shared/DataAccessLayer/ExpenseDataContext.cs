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
    using System.Globalization;
    using System.Linq;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public class ExpenseDataContext : TableServiceContext
    {
        public const string ExpenseTable = "multientityschemaexpenses";
        public const string ExpenseExportTable = "multientityschemaexpenseexport";

        public ExpenseDataContext(CloudStorageAccount account)
            : this(account.TableEndpoint.ToString(), account.Credentials)
        {
        }

        public ExpenseDataContext(string baseAddress, StorageCredentials credentials)
            : base(baseAddress, credentials)
        {
            this.ResolveType = ResolveEntityType;
        }

        public IQueryable<ExpenseAndExpenseItemRow> ExpensesAndExpenseItems
        {
            get
            {
                return this.CreateQuery<ExpenseAndExpenseItemRow>(ExpenseTable);
            }
        }

        public IQueryable<ExpenseExportRow> ExpenseExport
        {
            get
            {
                return this.CreateQuery<ExpenseExportRow>(ExpenseExportTable);
            }
        } 
        
        private static Type ResolveEntityType(string name)
        {
            var tableName = name.Split(new[] { '.' }).Last();
            switch (tableName)
            {
                case ExpenseTable:
                    return typeof(ExpenseAndExpenseItemRow);
                case ExpenseExportTable:
                    return typeof(ExpenseExportRow);
            }

            throw new ArgumentException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Could not resolve the table name '{0}' to a known entity type.",
                    name));
        }
    }
}
