//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Model
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Roles = new List<string>();
        }
        
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Manager { get; set; }

        public IList<string> Roles { get; set; }

        public string CostCenter { get; set; }

        public ReimbursementMethod PreferredReimbursementMethod { get; set; }        
    }
}