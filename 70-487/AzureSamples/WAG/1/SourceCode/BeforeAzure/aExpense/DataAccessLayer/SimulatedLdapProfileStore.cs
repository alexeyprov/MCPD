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

    public static class SimulatedLdapProfileStore
    {
        public static Dictionary<string, string> GetAttributesFor(string userName, string[] attributes)
        {
            // this is a mock version of an LDAP query
            // (&(objectCategory=person)(objectClass=user);costCenter;manager;displayName" 
            Dictionary<string, string> results;

            switch (userName)
            {
                case "ADATUM\\johndoe":
                    results = new Dictionary<string, string>
                    {
                        { "costCenter", "31023" },
                        { "manager", "ADATUM\\mary" },
                        { "displayName", "John Doe" }
                    };
                    break;

                case "ADATUM\\mary":
                    results = new Dictionary<string, string>
                    {
                        { "costCenter", "92452" },
                        { "manager", "ADATUM\\bob" },
                        { "displayName", "Mary May" }
                    };
                    break;

                case "ADATUM\\bob":
                    results = new Dictionary<string, string>
                    {
                        { "costCenter", "92452" },
                        { "manager", "ADATUM\\bob" },
                        { "displayName", "Robert Tor" }
                    };
                    break;

                default:
                    throw new ArgumentException("User does not exists in LDAP");
            }

            return results;
        }
    }
}
