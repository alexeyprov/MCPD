//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Claims
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using Microsoft.IdentityModel.Claims;

    public static class ClaimHelper
    {
        public static Claim GetCurrentUserClaim(string claimType)
        {
            return GetClaimFromPrincipal(Thread.CurrentPrincipal, claimType);
        }

        public static IEnumerable<Claim> GetCurrentUserClaims(string claimType)
        {
            return GetClaimsFromPrincipal(Thread.CurrentPrincipal, claimType);
        }

        public static Claim GetClaimFromPrincipal(IPrincipal principal, string claimType)
        {
            var claims = GetClaimsFromPrincipal(principal, claimType);

            return claims.FirstOrDefault();
        }

        public static IEnumerable<Claim> GetClaimsFromPrincipal(IPrincipal principal, string claimType)
        {
            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }

            IClaimsPrincipal claimsPrincipal = principal as IClaimsPrincipal;

            if (claimsPrincipal == null)
            {
                throw new ArgumentException("Cannot convert principal to IClaimsPrincipal.", "principal");
            }

            return GetClaimsFromIdentity(claimsPrincipal.Identities[0], claimType);
        }

        public static Claim GetClaimFromIdentity(IIdentity identity, string claimType)
        {
            var claims = GetClaimsFromIdentity(identity, claimType);

            return claims.FirstOrDefault();
        }

        public static IEnumerable<Claim> GetClaimsFromIdentity(IIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            IClaimsIdentity claimsIdentity = identity as IClaimsIdentity;

            if (claimsIdentity == null)
            {
                throw new ArgumentException("Cannot convert identity to IClaimsIdentity", "identity");
            }

            return claimsIdentity.Claims.Where(c => c.ClaimType == claimType);
        }
    }
}