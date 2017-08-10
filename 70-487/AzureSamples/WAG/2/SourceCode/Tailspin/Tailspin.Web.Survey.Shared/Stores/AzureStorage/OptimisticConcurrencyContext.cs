//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Tailspin.Web.Survey.Shared.Stores.AzureStorage
{
    using Microsoft.WindowsAzure.StorageClient;

    public class OptimisticConcurrencyContext : IConcurrencyControlContext
    {
        public OptimisticConcurrencyContext()
        {
            this.AccessCondition = AccessCondition.None;
        }

        internal OptimisticConcurrencyContext(string entityTag)
        {
            this.AccessCondition = AccessCondition.IfMatch(entityTag);
        }

        public AccessCondition AccessCondition { get; set; }

        public string ObjectId { get; set; }
    }
}
