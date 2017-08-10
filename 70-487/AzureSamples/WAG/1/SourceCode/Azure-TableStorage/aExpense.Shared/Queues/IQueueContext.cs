//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Shared.Queues
{
    using System;

    [CLSCompliant(false)]
    public interface IQueueContext
    {
        void AddMessage(BaseQueueMessage message);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "We already provide non-generic")]
        T GetMessage<T>() where T : BaseQueueMessage;

        object GetMessage(Type messageType);

        void DeleteMessage(BaseQueueMessage message);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "We are providing a version without Generics")]
        T[] GetMultipleMessages<T>(int maxMessages) where T : BaseQueueMessage;

        object[] GetMultipleMessages(Type messageType, int maxMessages);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "We already provide non-generic")]
        void Purge<T>() where T : BaseQueueMessage;

        void Purge(Type messageType);
    }
}