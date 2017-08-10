//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.DataStores.Caching
{
    using System;

    public interface ICachingStrategy
    {
        TimeSpan DefaultTimeout
        {
            get;
            set;
        }

        object Get<T>(string key, Func<T> fallbackAction, TimeSpan? timeout) where T : class;
    }
}