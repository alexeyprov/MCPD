//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Shared.Communication.Exceptions
{
    using System;

    [Serializable]
    public class InvalidTokenException : Exception
    {
        private const string DefaultMessage = "The provided token is invalid";

        public InvalidTokenException() : base(DefaultMessage)
        {
        }

        public InvalidTokenException(Exception innerException) : base(DefaultMessage, innerException)
        {
        }
    }
}
