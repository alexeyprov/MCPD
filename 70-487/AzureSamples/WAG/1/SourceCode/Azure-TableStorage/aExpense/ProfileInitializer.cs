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
    using System.Collections.Specialized;
    using System.Web.Profile;
    using AExpense.Model;
    using AExpense.Providers;

    public static class ProfileInitializer
    {
        public static void Initialize()
        {
            CreateProfile();
        }

        private static void CreateProfile()
        {
            var provider = new TableStorageProfileProvider();
            provider.Initialize(string.Empty, new NameValueCollection());

            string username = @"ADATUM\johndoe";
            ReimbursementMethod reimbursementMethod = ReimbursementMethod.Cash;
            CreateUserInProfile(username, reimbursementMethod);

            username = @"ADATUM\mary";
            reimbursementMethod = ReimbursementMethod.Check;
            CreateUserInProfile(username, reimbursementMethod);

            username = @"ADATUM\bob";
            reimbursementMethod = ReimbursementMethod.Check;
            CreateUserInProfile(username, reimbursementMethod);
        }

        private static void CreateUserInProfile(string username, ReimbursementMethod reimbursementMethod)
        {
            var profile = ProfileBase.Create(username);
            profile.SetPropertyValue("PreferredReimbursementMethod", Enum.GetName(typeof(ReimbursementMethod), reimbursementMethod));
            profile.Save();
        }
    }
}
