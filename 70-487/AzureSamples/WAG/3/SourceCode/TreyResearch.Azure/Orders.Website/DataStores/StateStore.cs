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

    public class StateStore : IStateStore
    {
        private readonly IEnumerable<State> states;

        public StateStore()
        {
            this.states = new List<State>
            {
                new State { Name = "Alabama", AlphaCode = "AL", StateId = 1 },
                new State { Name = "Alaska", AlphaCode = "AK", StateId = 2 }, 
                new State { Name = "Arizona", AlphaCode = "AZ", StateId = 4 },
                new State { Name = "Arkansas", AlphaCode = "AR", StateId = 5 },
                new State { Name = "California", AlphaCode = "CA", StateId = 6 },
                new State { Name = "Colorado", AlphaCode = "CO", StateId = 8 },
                new State { Name = "Connecticut", AlphaCode = "CT", StateId = 9 },
                new State { Name = "Delaware", AlphaCode = "DE", StateId = 10 },
                new State { Name = "District of Columbia", AlphaCode = "DC", StateId = 11 },
                new State { Name = "Florida", AlphaCode = "FL", StateId = 12 },
                new State { Name = "Georgia", AlphaCode = "GA", StateId = 13 },
                new State { Name = "Hawaii", AlphaCode = "HI", StateId = 15 },
                new State { Name = "Idaho", AlphaCode = "ID", StateId = 16 },
                new State { Name = "Illinois", AlphaCode = "IL", StateId = 17 },
                new State { Name = "Indiana", AlphaCode = "IN", StateId = 18 },
                new State { Name = "Iowa", AlphaCode = "IA", StateId = 20 },
                new State { Name = "Kentucky", AlphaCode = "KY", StateId = 21 },
                new State { Name = "Louisiana", AlphaCode = "LA", StateId = 22 },
                new State { Name = "Maine", AlphaCode = "ME", StateId = 23 },
                new State { Name = "Maryland", AlphaCode = "MD", StateId = 24 },
                new State { Name = "Massachusetts", AlphaCode = "MA", StateId = 25 },
                new State { Name = "Michigan", AlphaCode = "MI", StateId = 26 },
                new State { Name = "Minnesota", AlphaCode = "MN", StateId = 27 },
                new State { Name = "Mississippi", AlphaCode = "MS", StateId = 28 },
                new State { Name = "Missouri", AlphaCode = "MO", StateId = 29 },
                new State { Name = "Montana", AlphaCode = "MT", StateId = 30 },
                new State { Name = "Nebraska", AlphaCode = "NE", StateId = 31 },
                new State { Name = "Nevada", AlphaCode = "NV", StateId = 32 },
                new State { Name = "New Hampshire", AlphaCode = "NH", StateId = 33 },
                new State { Name = "New Jersey", AlphaCode = "NJ", StateId = 34 },
                new State { Name = "New Mexico", AlphaCode = "NM", StateId = 35 },
                new State { Name = "New York", AlphaCode = "NY", StateId = 36 },
                new State { Name = "North Carolina", AlphaCode = "NC", StateId = 37 },
                new State { Name = "North Dakota", AlphaCode = "ND", StateId = 38 },
                new State { Name = "Ohio", AlphaCode = "OH", StateId = 39 },
                new State { Name = "Oklahoma", AlphaCode = "OK", StateId = 40 },
                new State { Name = "Oregon", AlphaCode = "OR", StateId = 41 },
                new State { Name = "Pennsylvania", AlphaCode = "PA", StateId = 42 },
                new State { Name = "Rhode Island", AlphaCode = "RI", StateId = 44 },
                new State { Name = "South Carolina", AlphaCode = "SC", StateId = 45 },
                new State { Name = "South Dakota", AlphaCode = "SD", StateId = 46 },
                new State { Name = "Tennessee", AlphaCode = "TN", StateId = 47 },
                new State { Name = "Texas", AlphaCode = "TX", StateId = 48 },
                new State { Name = "Utah", AlphaCode = "UT", StateId = 49 },
                new State { Name = "Vermont", AlphaCode = "VT", StateId = 50 },
                new State { Name = "Virginia", AlphaCode = "VA", StateId = 51 },
                new State { Name = "Washington", AlphaCode = "WA", StateId = 53 },
                new State { Name = "West Virginia", AlphaCode = "WV", StateId = 54 },
                new State { Name = "Wisconsin", AlphaCode = "WI", StateId = 55 },
                new State { Name = "Wyoming", AlphaCode = "WY", StateId = 56 },
            };
        }

        public IEnumerable<State> FindAll()
        {
            return this.states;
        }
    }
}