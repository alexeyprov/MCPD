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
    public class RoleRow : TableServiceEntity
    {
        private string applicationName;
        private string roleName;
        private string userName;

        public RoleRow()
        {
        }

        // applicationName + userName is partitionKey
        // roleName is rowKey
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "The constructor calls a base constructor")]
        public RoleRow(string applicationName, string roleName, string userName)
            : base(SecUtility.CombineToKey(applicationName, userName), SecUtility.Escape(roleName))
        {
            SecUtility.CheckParameter(ref applicationName, true, true, true, Constants.MaxTableApplicationNameLength, "applicationName");
            SecUtility.CheckParameter(ref roleName, true, true, true, 512, "roleName");
            SecUtility.CheckParameter(ref userName, true, false, true, Constants.MaxTableUsernameLength, "userName");
            this.ApplicationName = applicationName;
            this.RoleName = roleName;
            this.UserName = userName;
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

        public string RoleName
        {
            get
            {
                return this.roleName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentException(
                        "To ensure string values are always updated, this implementation does not allow null as a string value.");
                }

                this.roleName = value;
                this.RowKey = SecUtility.Escape(this.RoleName);
            }
        }

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
    }
}