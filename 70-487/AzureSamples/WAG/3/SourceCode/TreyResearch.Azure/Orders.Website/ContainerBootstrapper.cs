//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website
{
    using System;
    using System.Globalization;
    using Microsoft.Practices.Unity;
    using Orders.Shared;
    using Orders.Website.Controllers;
    using Orders.Website.DataStores;
    using Orders.Website.DataStores.Caching;

    public static class ContainerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AccountController>();
            container.RegisterType<CheckoutController>();
            container.RegisterType<MyOrdersController>();
            container.RegisterType<ShoppingCartController>();
            container.RegisterType<StoreController>();

            container.RegisterType<ICartStore, CartStore>();
            container.RegisterType<ICountryStore, CountryStore>();
            container.RegisterType<ICustomerStore, CustomerStore>();
            container.RegisterType<IOrderStore, OrderStore>();
            container.RegisterType<IStateStore, StateStore>();

            // To disable caching on the product store, replace the code below with the following: container.RegisterType<IProductStore, ProductStore>();
            container.RegisterType<IProductStore, ProductStoreWithCache>(
                new InjectionConstructor(new ResolvedParameter<ProductStore>(), new ResolvedParameter<ICachingStrategy>()));
            container.RegisterType<ProductStore>();

            // To change the caching strategy, replace the CachingStrategy class with the strategy that you want to use instead.
            var cacheAcsKey = CloudConfiguration.GetConfigurationSetting("CacheAcsKey", null);
            var port = Convert.ToInt32(CloudConfiguration.GetConfigurationSetting("CachePort", null), CultureInfo.InvariantCulture);
            var host = CloudConfiguration.GetConfigurationSetting("CacheHost", null);

            var isLocalCacheEnabled = Convert.ToBoolean(CloudConfiguration.GetConfigurationSetting("IsLocalCacheEnabled", null), CultureInfo.InvariantCulture);
            var localCacheObjectCount = Convert.ToInt64(CloudConfiguration.GetConfigurationSetting("LocalCacheObjectCount", null), CultureInfo.InvariantCulture);
            var localCacheTtlValue = Convert.ToInt32(CloudConfiguration.GetConfigurationSetting("LocalCacheTtlValue", null), CultureInfo.InvariantCulture);
            var localCacheSync = CloudConfiguration.GetConfigurationSetting("LocalCacheSync", null);

            container.RegisterType<ICachingStrategy, CachingStrategy>(new ContainerControlledLifetimeManager(), new InjectionConstructor(host, port, cacheAcsKey, isLocalCacheEnabled, localCacheObjectCount, localCacheTtlValue, localCacheSync));
        }
    }
}