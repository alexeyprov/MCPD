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

    public class ExpenseReceiptStorage
    {
        public const string ReceiptContainerName = "receipt";

        private readonly CloudStorageAccount account;
        private readonly string containerName;
        private readonly CloudBlobContainer container;
        private readonly Microsoft.Practices.TransientFaultHandling.RetryPolicy storageRetryPolicy;

        public ExpenseReceiptStorage()
        {
            this.account = CloudConfiguration.GetStorageAccount("DataConnectionString");

            this.containerName = ReceiptContainerName;
            var client = this.account.CreateCloudBlobClient();
            client.RetryPolicy = RetryPolicies.NoRetry();

            this.container = client.GetContainerReference(this.containerName);

            this.storageRetryPolicy = RetryPolicyFactory.GetDefaultAzureStorageRetryPolicy();
        }

        public string AddReceipt(string receiptId, byte[] receipt, string contentType)
        {
            CloudBlob blob = this.storageRetryPolicy.ExecuteAction(() => this.container.GetBlobReference(receiptId));
            blob.Properties.ContentType = contentType;
            this.storageRetryPolicy.ExecuteAction(() => blob.UploadByteArray(receipt));
                        
            return blob.Uri.ToString();
        }

        public byte[] GetReceipt(string receiptId)
        {
            CloudBlob blob = this.storageRetryPolicy.ExecuteAction(() => this.container.GetBlobReference(receiptId));
            
            try
            {
               return blob.DownloadByteArray();
            }
            catch (StorageClientException)
            {
                return null;
            }
        }

        public void DeleteReceipt(string receiptId)
        {
            CloudBlob blob = this.storageRetryPolicy.ExecuteAction(() => this.container.GetBlobReference(receiptId));
            this.storageRetryPolicy.ExecuteAction(() => blob.DeleteIfExists());
        }
    }
}
