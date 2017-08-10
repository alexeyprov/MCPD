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
    using Microsoft.WindowsAzure.StorageClient;

    [CLSCompliant(false)]
    public class SessionRow : TableServiceEntity
    {
        private string applicationName;
        private string blobName;
        private DateTime created;
        private DateTime expires;
        private string id;
        private DateTime lockDate;

        // application name + session id is partitionKey
        public SessionRow(string sessionId, string applicationName)
            : base(SecUtility.CombineToKey(applicationName, sessionId), string.Empty)
        {
            SecUtility.CheckParameter(ref sessionId, true, true, true, Configuration.MaxStringPropertySizeInChars, "sessionId");
            SecUtility.CheckParameter(ref applicationName, true, true, true, Constants.MaxTableApplicationNameLength, "applicationName");

            this.Id = sessionId;
            this.ApplicationName = applicationName;
            this.ExpiresUtc = Configuration.MinSupportedDateTime;
            this.LockDateUtc = Configuration.MinSupportedDateTime;
            this.CreatedUtc = Configuration.MinSupportedDateTime;
            this.Timeout = 0;
            this.BlobName = string.Empty;
        }

        public SessionRow()
        {
        }

        public string ApplicationName
        {
            get
            {
                return this.applicationName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.applicationName = value;
                PartitionKey = SecUtility.CombineToKey(this.ApplicationName, this.Id);
            }
        }

        public string BlobName
        {
            get
            {
                return this.blobName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.blobName = value;
            }
        }

        public DateTime CreatedUtc
        {
            get
            {
                return this.created;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.created);
            }
        }

        public DateTime ExpiresUtc
        {
            get
            {
                return this.expires;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.expires);
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.id = value;
                PartitionKey = SecUtility.CombineToKey(this.ApplicationName, this.Id);
            }
        }

        public bool Initialized { get; set; }
        public int Lock { get; set; }

        public DateTime LockDateUtc
        {
            get { return this.lockDate; }
            set { SecUtility.SetUtcTime(value, out this.lockDate); }
        }

        public bool Locked { get; set; }
        public int Timeout { get; set; }
    }
}