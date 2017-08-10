//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.DataStores
{
    using System.Collections.Generic;
    using System.Linq;
    using HeadOffice.Models;

    public class OrderStore : IOrderStore
    {
        private readonly List<Sales> salesData;

        public OrderStore()
        {
            this.salesData = new List<Sales>();

            var regions = new List<string> { "West", "East", "Central" };

            for (var quarter = 0; quarter < 4; quarter++)
            {
                for (var region = 0; region < 3; region++)
                {
                    this.salesData.Add(new Sales { Quarter = quarter, Region = regions[region], SalesAmmount = (quarter + 1) * (region + 1) * 1000 });
                }
            }
        }

        public double SalesByQuarter(int quarter)
        {
            return this.salesData.Where(s => s.Quarter == quarter).Sum(s => s.SalesAmmount);
        }

        public double SalesByRegion(string region)
        {
            return this.salesData.Where(s => s.Region == region).Sum(s => s.SalesAmmount);
        }
    }
}