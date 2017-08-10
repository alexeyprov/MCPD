﻿//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Tailspin.Web.Models
{
    using Tailspin.Web.Survey.Shared.Models;

    public class TenantMasterPageViewData
    {
        public string Title { get; set; }

        public Tenant Tenant { get; set; }
    }
}