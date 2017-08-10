//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.DataStores
{
    using System.Collections.Generic;
    using Orders.Website.Models;

    public class CountryStore : ICountryStore
    {
        private readonly IEnumerable<Country> countries;

        public CountryStore()
        {
            this.countries = new List<Country>
            {
                new Country { CountryId = 1, Name = "USA" },
                new Country { CountryId = 2, Name = "Canada" },
                new Country { CountryId = 3, Name = "Mexico" },
                new Country { CountryId = 4, Name = "Brazil" },
                new Country { CountryId = 5, Name = "Argentina" }
            };
        }

        public IEnumerable<Country> FindAll()
        {
            return this.countries;
        }
    }
}