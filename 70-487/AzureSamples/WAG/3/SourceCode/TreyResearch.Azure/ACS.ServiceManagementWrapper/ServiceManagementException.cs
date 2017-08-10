//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace ACS.ServiceManagementWrapper
{
    using System;
    using System.Globalization;

    public class ServiceManagementException : Exception
    {
        public ServiceManagementException(Exception exception, string swtToken)
            : base(
                string.Format(
                        CultureInfo.InvariantCulture,
                        "{0} (SWT_Token: {1})",
                        "ServiceManagementException",
                        Uri.UnescapeDataString(swtToken)), 
                exception)
        { 
        }
    }
}