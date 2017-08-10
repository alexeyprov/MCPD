//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Orders.Workers.Models;

    public class TransportPartnerStore : ITransportPartnerStore
    {
        private readonly IEnumerable<TransportPartner> partners;

        public TransportPartnerStore()
        {
            this.partners = new List<TransportPartner>
            {
                // Ships to the listed states only.
                new TransportPartner { TransportPartnerId = 1, Name = "Contoso", ShipsTo = new List<string> { "Minnesota", "Iowa", "Missouri", "Illinois", "Winconsin", "Indiana", "Michigan", "Ohio" } },

                // Ships everywhere else.
                new TransportPartner { TransportPartnerId = 2, Name = "Fabrikam", ShipsTo = new List<string>() },
            };
        }

        public string GetTransportPartnerName(string state)
        {
            if (this.partners.ToList()[0].ShipsTo.Contains(state, StringComparer.InvariantCultureIgnoreCase))
            {
                // Return "Contoso partner "local"
                return this.partners.ToList()[0].Name;
            }

            // Return "Fabrikam" transport partner
            return this.partners.ToList()[1].Name;
        }
    }
}