//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.Models
{
    using Microsoft.WindowsAzure.StorageClient;

    public class WadLog : TableServiceEntity
    {
        public long EventTickCount { get; set; }
        public string DeploymentId { get; set; }
        public string Role { get; set; }
        public string RoleInstance { get; set; }
        public int Level { get; set; }
        public int EventId { get; set; }
        public int Pid { get; set; }
        public int Tid { get; set; }
        public string Message { get; set; }
    }
}
