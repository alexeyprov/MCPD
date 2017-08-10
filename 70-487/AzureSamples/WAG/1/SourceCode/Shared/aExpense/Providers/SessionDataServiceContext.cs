//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Providers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.WindowsAzure.StorageClient;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Class is used by the devtablegen tool to generate a database for the development storage tool")]
    internal class SessionDataServiceContext : TableServiceContext
    {
        public SessionDataServiceContext()
            : base(null, null)
        {
        }

        public IQueryable<SessionRow> Sessions
        {
            get
            {
                return CreateQuery<SessionRow>("Sessions");
            }
        }
    }
}