//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Adatum.SimulatedIssuer
{
    public static class Adatum
    {
        public static string OrganizationName
        {
            get
            {
                return "Adatum";
            }
        }

        public static class ClaimTypes
        {
            public static readonly string Organization = "http://schemas.adatum.com/claims/2009/08/organization";
            public static readonly string CostCenter = "http://schemas.adatum.com/claims/2009/08/costcenter";
            public static readonly string Manager = "http://schemas.adatum.com/claims/2009/08/manager";
        }

        public static class Roles
        {
            public static readonly string Employee = "Employee";
            public static readonly string Manager = "Manager";
        }

        public static class CostCenters
        {
            public static readonly string Accountancy = "92452";
            public static readonly string CustomerService = "31023";
        }        
    }
}