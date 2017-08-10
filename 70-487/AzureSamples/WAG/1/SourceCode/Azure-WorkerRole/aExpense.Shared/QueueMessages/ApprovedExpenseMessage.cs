//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.QueueMessages
{
    using System;
    using System.Runtime.Serialization;
    using Queues;

    [CLSCompliant(false)]
    [DataContract]
    public class ApprovedExpenseMessage : BaseQueueMessage
    {
        [DataMember]
        public string ExpenseId { get; set; }

        [DataMember]
        public DateTime ApproveDate { get; set; }
    }
}