﻿//===============================================================================
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
    using HeadOffice.Models;

    public interface ICustomerStore
    {
        void Add(Customer customer);
        Customer FindOne(string userName);
        IEnumerable<Customer> FindAll();
    }
}
