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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    internal class BlobProvider
    {
        private const string PathSeparator = "/";
        private static readonly RetryPolicy RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1));
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);
        private readonly CloudBlobClient client;
        private readonly string containerName;
        private readonly object lockObject = new object();
        private CloudBlobContainer container;

        internal BlobProvider(StorageCredentials info, Uri baseUri, string containerName)
        {
            this.containerName = containerName;
            this.client = new CloudBlobClient(baseUri.ToString(), info);
        }

        internal string ContainerUrl
        {
            get { return string.Join(PathSeparator, new[] { this.client.BaseUri.AbsolutePath, this.containerName }); }
        }

        public IEnumerable<IListBlobItem> ListBlobs(string folder)
        {
            var cloudBlobContainer = this.GetContainer();
            try
            {
                return cloudBlobContainer.ListBlobs().Where(blob => blob.Uri.PathAndQuery.StartsWith(cloudBlobContainer.Uri.LocalPath + "/" + folder));
            }
            catch (InvalidOperationException se)
            {
                Log.Write(EventKind.Error, "Error enumerating contents of folder {0} exists: {1}", this.ContainerUrl + PathSeparator + folder, se.Message);
                throw;
            }
        }

        internal bool DeleteBlob(string blobName)
        {
            var cloudBlobContainer = this.GetContainer();
            try
            {
                cloudBlobContainer.GetBlobReference(blobName).Delete();

                return true;
            }
            catch (InvalidOperationException se)
            {
                Log.Write(EventKind.Error, "Error deleting blob {0}: {1}", this.ContainerUrl + PathSeparator + blobName, se.Message);
                throw;
            }
        }

        internal bool DeleteBlobsWithPrefix(string prefix)
        {
            bool ret = true;

            var e = this.ListBlobs(prefix);
            if (e == null)
            {
                return true;
            }

            var props = e.GetEnumerator();
            if (props == null)
            {
                return true;
            }

            while (props.MoveNext())
            {
                if (props.Current != null)
                {
                    if (!this.DeleteBlob(props.Current.Uri.ToString()))
                    {
                        // ignore this; it is possible that another thread could try to delete the blob
                        // at the same time
                        ret = false;
                    }
                }
            }

            return ret;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification= "The method returns the memory stream")]
        internal MemoryStream GetBlobContent(string blobName, out BlobProperties properties)
        {
            var blobContent = new MemoryStream();
            properties = this.GetBlobContent(blobName, blobContent);
            blobContent.Seek(0, SeekOrigin.Begin);
            return blobContent;
        }

        internal BlobProperties GetBlobContent(string blobName, Stream outputStream)
        {
            if (blobName == string.Empty)
            {
                return null;
            }

            var cloudBlobContainer = this.GetContainer();
            try
            {
                var blob = cloudBlobContainer.GetBlobReference(blobName);

                blob.DownloadToStream(outputStream);

                BlobProperties properties = blob.Properties;
                Log.Write(EventKind.Information, "Getting contents of blob {0}", this.ContainerUrl + PathSeparator + blobName);
                return properties;
            }
            catch (InvalidOperationException sc)
            {
                Log.Write(EventKind.Error, "Error getting contents of blob {0}: {1}", this.ContainerUrl + PathSeparator + blobName, sc.Message);
                throw;
            }
        }

        internal bool GetBlobContentsWithoutInitialization(string blobName, Stream outputStream, out BlobProperties properties)
        {
            var cloudBlobContainer = this.GetContainer();

            try
            {
                var blob = cloudBlobContainer.GetBlobReference(blobName);

                blob.DownloadToStream(outputStream);

                properties = blob.Properties;
                Log.Write(EventKind.Information, "Getting contents of blob {0}", this.client.BaseUri + PathSeparator + this.containerName + PathSeparator + blobName);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException is WebException)
                {
                    var webEx = ex.InnerException as WebException;
                    var resp = webEx.Response as HttpWebResponse;

                    if (resp != null)
                    {
                        if (resp.StatusCode == HttpStatusCode.NotFound)
                        {
                            properties = null;
                            return false;
                        }
                    }

                    throw;
                }

                throw;
            }
        }

        internal void UploadStream(string blobName, Stream output)
        {
            this.UploadStream(blobName, output, true);
        }

        internal bool UploadStream(string blobName, Stream output, bool overwrite)
        {
            var cloudBlobContainer = this.GetContainer();
            try
            {
                output.Position = 0; // Rewind to start
                Log.Write(EventKind.Information, "Uploading contents of blob {0}", this.ContainerUrl + PathSeparator + blobName);

                var blob = cloudBlobContainer.GetBlockBlobReference(blobName);

                blob.UploadFromStream(output);

                return true;
            }
            catch (InvalidOperationException se)
            {
                Log.Write(EventKind.Error, "Error uploading blob {0}: {1}", this.ContainerUrl + PathSeparator + blobName, se.Message);
                throw;
            }
        }

        private CloudBlobContainer GetContainer()
        {
            // we have to make sure that only one thread tries to create the container
            lock (this.lockObject)
            {
                if (this.container != null)
                {
                    return this.container;
                }

                try
                {
                    var cloudBlobContainer = new CloudBlobContainer(this.containerName, this.client);
                    var requestModifiers = new BlobRequestOptions
                    {
                        Timeout = Timeout,
                        RetryPolicy = RetryPolicy
                    };

                    cloudBlobContainer.CreateIfNotExist(requestModifiers);

                    this.container = cloudBlobContainer;

                    return this.container;
                }
                catch (InvalidOperationException se)
                {
                    Log.Write(EventKind.Error, "Error creating container {0}: {1}", this.ContainerUrl, se.Message);
                    throw;
                }
            }
        }
    }
}