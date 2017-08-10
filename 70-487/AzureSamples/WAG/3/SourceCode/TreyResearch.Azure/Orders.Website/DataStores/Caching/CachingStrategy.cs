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
    using System.Security;
    using Microsoft.ApplicationServer.Caching;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Orders.Shared.Helpers;
    using Guard = Orders.Shared.Guard;

    public class CachingStrategy : ICachingStrategy, IDisposable
    {
        private readonly RetryPolicy cacheRetryPolicy;
        private DataCacheFactory cacheFactory;

        // IMPORTANT: The timeout value (the time before objects expire from the cache) depends heavily on the solution implemented, and a
        // thorough analysis should be performed in order to find the correct value for this setting.
        private TimeSpan defaultTimeout = TimeSpan.FromMinutes(10);

        public CachingStrategy()
        {
        }

        public CachingStrategy(string host, int port, string key, bool isLocalCacheEnabled, long objectCount, int ttlValue, string sync)
        {
            Guard.CheckArgumentNullOrEmpty(key, "key");
            Guard.CheckArgumentNullOrEmpty(sync, "sync");

            try
            {
                // Declare array for cache host.
                var servers = new DataCacheServerEndpoint[1];

                servers[0] = new DataCacheServerEndpoint(host, port);

                // Setup DataCacheSecurity configuration.
                var secureAcsKey = new SecureString();
                foreach (char a in key)
                {
                    secureAcsKey.AppendChar(a);
                }
                secureAcsKey.MakeReadOnly();
                var factorySecurity = new DataCacheSecurity(secureAcsKey);

                // Setup the DataCacheFactory configuration.
                var factoryConfig = new DataCacheFactoryConfiguration
                    {
                        Servers = servers, 
                        SecurityProperties = factorySecurity
                    };

                if (isLocalCacheEnabled)
                {
                    var invalidationPolicy = sync.Equals("notificationbased", StringComparison.OrdinalIgnoreCase) ? DataCacheLocalCacheInvalidationPolicy.NotificationBased : DataCacheLocalCacheInvalidationPolicy.TimeoutBased;
                    var localCacheProperties = new DataCacheLocalCacheProperties(objectCount, new TimeSpan(0, 0, 0, ttlValue), invalidationPolicy);
                    factoryConfig.LocalCacheProperties = localCacheProperties;
                }

                // Create a configured DataCacheFactory object.
                this.cacheFactory = new DataCacheFactory(factoryConfig);

                this.cacheRetryPolicy = RetryPolicyFactory.GetDefaultAzureCachingRetryPolicy();
                this.cacheRetryPolicy.Retrying +=
                    (sender, args) => TraceHelper.TraceInformation("Retry in CachingStrategy - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
            }
            catch (Exception ex)
            {
                TraceHelper.TraceError("Cache Setup Exception thrown: {0}", ex.Message); 
            }
        }

        public TimeSpan DefaultTimeout
        {
            get
            {
                return this.defaultTimeout;
            }

            set
            {
                this.defaultTimeout = value;
            }
        }

        public virtual object Get<T>(string key, Func<T> fallbackAction, TimeSpan? timeout) where T : class
        {
            Guard.CheckArgumentNull(fallbackAction, "fallbackAction");

            try
            {
                // The default cache is retrieved from the cache factory
                var dataCache = this.cacheFactory.GetDefaultCache();

                // First, find the object to be retrieved in the cache 
                var cachedObject = this.cacheRetryPolicy.ExecuteAction(() => dataCache.Get(key));

                if (cachedObject != null)
                {
                    TraceHelper.TraceInformation("Object '{0}' was retrieved from the cache", key);

                    // The object was found in the cache, we return it without hitting the default data store
                    return cachedObject;
                }
                TraceHelper.TraceInformation("Object '{0}' was not found in the cache", key);

                // The object was not found in the cache, and the action is executed trying to get the object from the default data store
                var objectToBeCached = fallbackAction();

                if (objectToBeCached != null)
                {
                    try
                    {
                        // If the object is found, we have to store it in the cache for future requests
                        this.cacheRetryPolicy.ExecuteAction(() => dataCache.Put(key, objectToBeCached, timeout != null ? timeout.Value : this.DefaultTimeout));

                        TraceHelper.TraceInformation("Object '{0}' was stored in the cache", key);

                        // The object is returned
                        return objectToBeCached;
                    }
                    catch (DataCacheException ex)
                    {
                        // If we can't store the object in the cache, we log the exception
                        TraceHelper.TraceError("Exception thrown in CachingStrategy: {0}", ex.Message);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                TraceHelper.TraceError("Exception thrown in CachingStrategy: {0}", ex.Message);

                // If there is a failure while trying to get the object, we get the object by calling to the fallbackAction
                return fallbackAction();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.cacheFactory != null)
                {
                    this.cacheFactory.Dispose();
                    this.cacheFactory = null;
                }
            }
        }
    }
}