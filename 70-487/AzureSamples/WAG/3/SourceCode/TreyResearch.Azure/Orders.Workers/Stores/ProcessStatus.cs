//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers.Stores
{
    public class ProcessStatus
    {
        public const string PendingProcess = "pending process";
        public const string Processed = "processed";
        public const string Error = "error";
        public const string CriticalError = "critical error";
    }
}
