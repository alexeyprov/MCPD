//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Shared
{
    using System;
    using AExpense;
    using AExpense.DataAccessLayer;
    using AExpense.QueueMessages;
    using AExpense.Shared.Queues;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public static class ApplicationStorageInitializer
    {
        public static void Initialize()
        {
            CloudStorageAccount account = CloudConfiguration.GetStorageAccount("DataConnectionString");

            // Tables
            var cloudTableClient = new CloudTableClient(account.TableEndpoint.ToString(), account.Credentials);
            cloudTableClient.CreateTableIfNotExist<ExpenseAndExpenseItemRow>(ExpenseDataContext.ExpenseTable);
            cloudTableClient.CreateTableIfNotExist<ExpenseExportRow>(ExpenseDataContext.ExpenseExportTable);

            // Blobs
            var client = account.CreateCloudBlobClient();
            client.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(5));
            var container = client.GetContainerReference(ExpenseReceiptStorage.ReceiptContainerName);
            container.CreateIfNotExist();
            container = client.GetContainerReference(ExpenseExportStorage.ExpenseExportContainerName);
            container.CreateIfNotExist();

            // Queues
            var queueContext = new AzureQueueContext(account);
            queueContext.Purge<NewReceiptMessage>();
            queueContext.Purge<ApprovedExpenseMessage>();
        }
    }
}
