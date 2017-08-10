//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.Helpers
{
    using System.Globalization;
    using System.Linq;
    using Microsoft.IdentityModel.Claims;

    public static class IdentityExtensions
    {
        private const string ClaimType = "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        public static string GetFederatedUsername(this IClaimsIdentity identity)
        {
            var originalIssuer = identity.Claims.Single(c => c.ClaimType == ClaimType).Value;

            var userName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", originalIssuer, identity.Name);

            return userName;
        }

        public static string GetOriginalIssuer(this IClaimsIdentity identity)
        {
            return identity.Claims.Single(c => c.ClaimType == ClaimType).Value;
        }
    }
}