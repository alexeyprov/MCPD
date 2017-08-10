//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense
{
    using System;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

    public class Global : System.Web.HttpApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "This is what the framework expects.")]
        private void Application_Error(object sender, EventArgs e)
        {
            // Get reference to the source of the exception chain
            Exception ex = Server.GetLastError();
            ExceptionPolicy.HandleException(ex, "Log Policy");            
        }
    }
}