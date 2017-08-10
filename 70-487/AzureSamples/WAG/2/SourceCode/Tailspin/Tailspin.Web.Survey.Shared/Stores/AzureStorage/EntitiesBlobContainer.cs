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
    using System.IO;
    using System.Text;
    using System.Web.Script.Serialization;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    public class EntitiesBlobContainer<T> : AzureBlobContainer<T>
    {
        public EntitiesBlobContainer(CloudStorageAccount account)
            : base(account)
        {
        }

        public EntitiesBlobContainer(CloudStorageAccount account, string containerName)
            : base(account, containerName)
        {
        }

        protected override T ReadObject(CloudBlob blob)
        {
            return new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Deserialize<T>(blob.DownloadText());
        }

        protected override void WriteOject(CloudBlob blob, BlobRequestOptions options, T obj)
        {
            blob.Properties.ContentType = "application/json";
            blob.UploadText(new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Serialize(obj), Encoding.Default, options);
        }

        protected override byte[] BinarizeObjectForStreaming(BlobProperties properties, T obj)
        {
            properties.ContentType = "application/json";
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.Default))
            {
                writer.Write(new JavaScriptSerializer().Serialize(obj));
                writer.Flush();

                return stream.ToArray();
            }
        }
    }
}