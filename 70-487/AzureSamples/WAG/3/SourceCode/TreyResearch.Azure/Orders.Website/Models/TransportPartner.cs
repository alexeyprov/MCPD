//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.Models
{
    using System.Collections.Generic;

    public class TransportPartner
    {
        public int TransportPartnerId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> ShipsTo { get; set; }
    }
}