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
    using System.Web.Profile;

    public static class ProfileExtensions
    {
        public static T GetProperty<T>(this ProfileBase profile, string property)
        {
            object value = profile.GetPropertyValue(property);

            if (value == null)
            {
                return default(T);
            }

            return (T)value;
        }
    }
}