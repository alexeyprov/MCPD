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
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public class ExpenseExportStorage
    {
        public const string ExpenseExportContainerName = "expenseout";

        private readonly CloudStorageAccount account;
        private readonly string containerName;
        private readonly CloudBlobContainer container;
        private readonly Microsoft.Practices.TransientFaultHandling.RetryPolicy storageRetryPolicy;

        public ExpenseExportStorage()
        {
            this.account = CloudConfiguration.GetStorageAccount("DataConnectionString");

            this.containerName = ExpenseExportContainerName;
            var client = this.account.CreateCloudBlobClient();
            client.RetryPolicy = RetryPolicies.NoRetry();

            this.storageRetryPolicy = RetryPolicyFactory.GetDefaultAzureStorageRetryPolicy();
            this.container = client.GetContainerReference(this.containerName);          
        }

        public string AddExport(string name, string content, string contentType)
        {
            CloudBlob blob = this.storageRetryPolicy.ExecuteAction(() => this.container.GetBlobReference(name));
            blob.Properties.ContentType = contentType;
            this.storageRetryPolicy.ExecuteAction(() => blob.UploadText(content));

            return blob.Uri.ToString();
        } 

        public string GetExport(string name)
        {
            CloudBlob blob = this.storageRetryPolicy.ExecuteAction(() => this.container.GetBlobReference(name));
            try
            {
                return blob.DownloadText();
            }
            catch (StorageClientException)
            {
                return string.Empty;
            }
        }
    }
}