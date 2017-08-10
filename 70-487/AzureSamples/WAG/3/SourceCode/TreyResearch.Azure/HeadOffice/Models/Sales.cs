//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.Models
{
    internal class Sales
    {
        internal int Quarter { get; set; }
        internal string Region { get; set; }
        internal double SalesAmmount { get; set; }
    }
}