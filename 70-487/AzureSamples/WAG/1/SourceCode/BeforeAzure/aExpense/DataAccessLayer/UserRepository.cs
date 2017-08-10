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
    using System.Web.Profile;
    using System.Web.Security;
    using Model;

    public class UserRepository
    {
        public User GetUser(string userName)
        {
            var membershipUser = Membership.GetUser(userName);
            if (membershipUser == null)
            {
                return null;
            }

            string[] roles = Roles.GetRolesForUser();
            var profile = ProfileBase.Create(userName);
            var attributes = SimulatedLdapProfileStore.GetAttributesFor(userName, new[] { "costCenter", "manager", "displayName" });
            
            var user = new User
                           {
                               CostCenter = attributes["costCenter"],
                               FullName = attributes["displayName"],
                               Manager = attributes["manager"],
                               UserName = membershipUser.UserName,
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