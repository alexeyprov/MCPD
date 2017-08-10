//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Profile;
    using AExpense.Model;
    using Claims;

    public class UserRepository
    {
        public User GetUser(string userName)
        {            
            // this is replaced with claims
            // string[] roles = Roles.GetRolesForUser();
            var roleClaims = ClaimHelper.GetCurrentUserClaims(Microsoft.IdentityModel.Claims.ClaimTypes.Role);
            string[] roles = roleClaims.Select(r => r.Value).ToArray();
            
            // var attributes = SimulatedLdapProfileStore.GetAttributesFor(userName, new[] { "costCenter", "manager", "displayName" });
            
            // we still use profile for app-specific profile data like preferred reiumbursment method
            var profile = ProfileBase.Create(userName);
            
            var user = new User
                           {
                               CostCenter = ClaimHelper.GetCurrentUserClaim(Adatum.ClaimTypes.CostCenter).Value,
                               FullName = ClaimHelper.GetCurrentUserClaim(System.IdentityModel.Claims.ClaimTypes.GivenName).Value + " " + ClaimHelper.GetCurrentUserClaim(System.IdentityModel.Claims.ClaimTypes.Surname).Value,
                               Manager = ClaimHelper.GetCurrentUserClaim(Adatum.ClaimTypes.Manager).Value,
                               UserName = ClaimHelper.GetCurrentUserClaim(System.IdentityModel.Claims.ClaimTypes.Name).Value,
                               PreferredReimbursementMethod = string.IsNullOrEmpty(profile.GetProperty<string>("PreferredReimbursementMethod")) ? 
                                                                ReimbursementMethod.NotSet : (ReimbursementMethod)Enum.Parse(typeof(ReimbursementMethod), profile.GetProperty<string>("PreferredReimbursementMethod")),
                               Roles = new List<string>(roles)
                           };
            return user;
        }        

        public void UpdateUserPreferredReimbursementMethod(User user)
        {
            var profile = ProfileBase.Create(user.UserName);

            profile.SetPropertyValue("PreferredReimbursementMethod", Enum.GetName(typeof(ReimbursementMethod), user.PreferredReimbursementMethod));
            profile.Save();
        }        
    }
}