//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.Services
{
    using HeadOffice.DataStores;

    public class OrdersStatistics : IOrdersStatistics
    {
        private readonly IOrderStore orderStore;

        public OrdersStatistics()
        {
            this.orderStore = new OrderStore();
        }

        public double SalesByQuarter(int quarter)
        {
            return this.orderStore.SalesByQuarter(quarter);
        }

        public double SalesByRegion(string region)
        {
            return this.orderStore.SalesByRegion(region);
        }
    }
}