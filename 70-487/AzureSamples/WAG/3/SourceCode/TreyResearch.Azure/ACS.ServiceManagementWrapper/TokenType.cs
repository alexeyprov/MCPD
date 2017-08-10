﻿//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace ACS.ServiceManagementWrapper
{
    public enum TokenType
    {
        SWT,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "This is converted to a string that is the same name passed to the endpoint.")]
        SAML_1_1,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "This is converted to a string that is the same name passed to the endpoint.")]
        SAML_2_0
    }
}