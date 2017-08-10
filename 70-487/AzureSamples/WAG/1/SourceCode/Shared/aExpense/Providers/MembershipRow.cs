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
    using System.Configuration.Provider;
    using Microsoft.WindowsAzure.StorageClient;

    [CLSCompliant(false)]
    public class MembershipRow : TableServiceEntity, IComparable
    {
        private string applicationName;
        private string comment;

        private DateTime createDate;
        private string email;
        private DateTime failedPasswordAnswerAttemptWindowStart;
        private DateTime failedPasswordAttemptWindowStart;
        private DateTime lastActivityDate;
        private DateTime lastLockoutDate;
        private DateTime lastLoginDate;
        private DateTime lastPasswordChangedDate;
        private string password;
        private string passwordAnswer;
        private string passwordQuestion;
        private string passwordSalt;
        private string profileBlobName;
        private DateTime profileLastUpdated;
        private string userName;

        // partition key is applicationName + userName
        // rowKey is empty
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "The constructor calls a base constructor")]
        public MembershipRow(string applicationName, string userName)
            : base(SecUtility.CombineToKey(applicationName, userName), string.Empty)
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new ProviderException("Partition key cannot be empty!");
            }
            ////if (string.IsNullOrEmpty(userName))
            ////{
            ////    throw new ProviderException("RowKey cannot be empty!");
            ////}

            // applicationName + userName is partitionKey
            // the reasoning behind this is that we want to strive for the best scalability possible 
            // chosing applicationName as the partition key and userName as row key would not give us that because 
            // it would mean that a site with millions of users had all users on a single partition
            // having the applicationName and userName inside the partition key is important for queries as queries
            // for users in a single application are the most frequent 
            // these queries are faster because application name and user name are part of the key
            ////PartitionKey = SecUtility.CombineToKey(applicationName, userName);
            ////RowKey = string.Empty;

            this.ApplicationName = applicationName;
            this.UserName = userName;

            this.Password = string.Empty;
            this.PasswordSalt = string.Empty;
            this.Email = string.Empty;
            this.PasswordAnswer = string.Empty;
            this.PasswordQuestion = string.Empty;
            this.Comment = string.Empty;
            this.ProfileBlobName = string.Empty;

            this.CreateDateUtc = Configuration.MinSupportedDateTime;
            this.LastLoginDateUtc = Configuration.MinSupportedDateTime;
            this.LastActivityDateUtc = Configuration.MinSupportedDateTime;
            this.LastLockoutDateUtc = Configuration.MinSupportedDateTime;
            this.LastPasswordChangedDateUtc = Configuration.MinSupportedDateTime;
            this.FailedPasswordAttemptWindowStartUtc = Configuration.MinSupportedDateTime;
            this.FailedPasswordAnswerAttemptWindowStartUtc = Configuration.MinSupportedDateTime;
            this.ProfileLastUpdatedUtc = Configuration.MinSupportedDateTime;

            this.ProfileIsCreatedByProfileProvider = false;
            this.ProfileSize = 0;
        }

        public MembershipRow()
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
                this.PartitionKey = SecUtility.CombineToKey(this.ApplicationName, this.UserName);
            }
        }

        public string Comment
        {
            get
            {
                return this.comment;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.comment = value;
            }
        }

        public DateTime CreateDateUtc
        {
            get
            {
                return this.createDate;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.createDate);
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.email = value;
            }
        }

        public int FailedPasswordAnswerAttemptCount { get; set; }

        public DateTime FailedPasswordAnswerAttemptWindowStartUtc
        {
            get
            {
                return this.failedPasswordAnswerAttemptWindowStart;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.failedPasswordAnswerAttemptWindowStart);
            }
        }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime FailedPasswordAttemptWindowStartUtc
        {
            get
            {
                return this.failedPasswordAttemptWindowStart;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.failedPasswordAttemptWindowStart);
            }
        }

        public bool IsAnonymous { get; set; }
        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime LastActivityDateUtc
        {
            get
            {
                return this.lastActivityDate;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.lastActivityDate);
            }
        }

        public DateTime LastLockoutDateUtc
        {
            get
            {
                return this.lastLockoutDate;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.lastLockoutDate);
            }
        }

        public DateTime LastLoginDateUtc
        {
            get
            {
                return this.lastLoginDate;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.lastLoginDate);
            }
        }

        public DateTime LastPasswordChangedDateUtc
        {
            get
            {
                return this.lastPasswordChangedDate;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.lastPasswordChangedDate);
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.password = value;
            }
        }

        public string PasswordAnswer
        {
            get
            {
                return this.passwordAnswer;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.passwordAnswer = value;
            }
        }

        public int PasswordFormat { get; set; }

        public string PasswordQuestion
        {
            get
            {
                return this.passwordQuestion;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.passwordQuestion = value;
            }
        }

        public string PasswordSalt
        {
            get
            {
                return this.passwordSalt;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.passwordSalt = value;
            }
        }

        public string ProfileBlobName
        {
            get
            {
                return this.profileBlobName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value..");
                }

                this.profileBlobName = value;
            }
        }

        public bool ProfileIsCreatedByProfileProvider { get; set; }

        public DateTime ProfileLastUpdatedUtc
        {
            get
            {
                return this.profileLastUpdated;
            }

            set
            {
                SecUtility.SetUtcTime(value, out this.profileLastUpdated);
            }
        }

        public int ProfileSize { get; set; }
        public Guid UserId { get; set; }

        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.userName = value;
                this.PartitionKey = SecUtility.CombineToKey(this.ApplicationName, this.UserName);
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var row = obj as MembershipRow;
            if (row == null)
            {
                throw new ArgumentException("The parameter obj is not of type MembershipRow.");
            }

            return string.Compare(this.UserName, row.UserName, StringComparison.Ordinal);
        }
    }
}