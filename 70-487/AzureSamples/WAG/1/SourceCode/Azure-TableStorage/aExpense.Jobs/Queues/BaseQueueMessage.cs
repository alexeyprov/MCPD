//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Jobs.Queues
{
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class BaseQueueMessage
    {
        private object context;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We would never want this to be serialized")]
        public object GetContext()
        {
            return this.context;
        }

        public void SetContext(object value)
        {
            this.context = value;
        }
    }
}