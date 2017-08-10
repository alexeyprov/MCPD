﻿//===============================================================================
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
    using Shared.Queues;

    [DataContract]
    public class NewReceiptMessage : BaseQueueMessage
    {
        [DataMember]
        public string ExpenseItemId { get; set; }
    }
}
