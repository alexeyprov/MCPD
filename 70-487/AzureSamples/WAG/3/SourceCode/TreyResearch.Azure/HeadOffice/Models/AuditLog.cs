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
    using System;

    public class AuditLog
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
    }
}