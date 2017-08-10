//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Workers.Jobs
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using ACS.ServiceManagementWrapper;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Orders.Shared;
    using Orders.Shared.Communication;
    using Orders.Shared.Communication.Adapters;
    using Orders.Shared.Communication.Exceptions;
    using Orders.Shared.Communication.Messages;
    using Orders.Shared.Helpers;
    using Orders.Workers.Stores;
    using Orders.Workers.Stores.Entities;

    public class StatusUpdateJob : IJob, IDisposable
    {
        private readonly RetryPolicy sqlCommandRetryPolicy;
        private CancellationTokenSource tokenSource;
        private bool disposed;

        public StatusUpdateJob()
        {
            // this policy is defined in the configurationfile
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
        }

        public void Run()
        {
            TraceHelper.TraceInformation("Initializing Status Update job...");

            this.tokenSource = new CancellationTokenSource();
            var serviceBusQueueDescription = new ServiceBusQueueDescription
                {
                    Namespace = CloudConfiguration.GetConfigurationSetting("serviceBusNamespace", string.Empty), 
                    QueueName = CloudConfiguration.GetConfigurationSetting("orderStatusUpdateQueue", string.Empty), 
                    Issuer = CloudConfiguration.GetConfigurationSetting("statusUpdateIssuer", string.Empty), 
                    DefaultKey = CloudConfiguration.GetConfigurationSetting("statusUpdateKey", string.Empty)
                };

            var orderStatusUpdatesQueue = new ServiceBusQueue(serviceBusQueueDescription);

            var receiverHandler = new ServiceBusReceiverHandler<OrderStatusUpdateMessage>(new MessageReceiverAdapter(orderStatusUpdatesQueue.GetReceiver()))
                { MessagePollingInterval = TimeSpan.FromSeconds(2) };

            // MessagePollingInterval should be configured taking into consideration variables such as CPU processing power, 
            // expected volume of orders to process and number of worker role instances
            receiverHandler.ProcessMessages(
                (message, replyTo, token) =>
                    {                        
                        return Task.Factory.StartNew(
                            () =>
                                {
                                    TraceHelper.TraceInformation("Processing messages...");

                                    if (!this.IsValidToken(message.OrderId, token))
                                    {
                                        // Throw exception, to be caught by handler.  Will send it to the DeadLetter queue.
                                        throw new InvalidTokenException();
                                    }

                                    var orderStatus = new OrderStatus { OrderId = message.OrderId, Status = message.Status };

                                    using (var db = TreyResearchModelFactory.CreateContext())
                                    {
                                        // Checking for duplicate entries in the order status table.  If a duplicate message arrives, it is discarded.
                                        var existingStatus =
                                            this.sqlCommandRetryPolicy.ExecuteAction(
                                                () => db.OrderStatus.SingleOrDefault(os => os.OrderId == message.OrderId && os.Status == message.Status));
                                        if (existingStatus != null)
                                        {
                                            return;
                                        }

                                        var order = this.sqlCommandRetryPolicy.ExecuteAction(() => db.Order.Single(o => o.OrderId == message.OrderId));
                                        order.TransportPartner = message.TransportPartnerName;

                                        if (message.TrackingId != Guid.Empty)
                                        {
                                            order.TrackingId = message.TrackingId;
                                        }

                                        db.OrderStatus.AddObject(new OrderStatus { OrderId = orderStatus.OrderId, Status = orderStatus.Status, Timestamp = DateTime.UtcNow });

                                        this.sqlCommandRetryPolicy.ExecuteAction(() => db.SaveChanges());
                                    }
                                });
                    }, 
                this.tokenSource.Token);

            while (this.tokenSource != null && !this.tokenSource.IsCancellationRequested)
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

        public void Stop()
        {
            // Cancel further processing by the receiver handler.
            this.tokenSource.Cancel();
        }

        public void Dispose()
        {
            this.Dispose(true);
                      
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.tokenSource != null)
                    {
                        this.tokenSource.Dispose();
                    }
                }

                // Indicate that the instance has been disposed.
                this.tokenSource = null;
                this.disposed = true;
            }
        }

        private bool IsValidToken(Guid orderId, string token)
        {
            string transportPartner;

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var order = this.sqlCommandRetryPolicy.ExecuteAction(() => database.Order.SingleOrDefault(o => o.OrderId == orderId));
                if (order != null)
                {
                    transportPartner = order.TransportPartner;
                }
                else
                {
                    throw new InvalidOperationException("Invalid Order Id");
                }
            }

            string acsServiceNamespace = CloudConfiguration.GetConfigurationSetting("acsNamespace", null);
            string acsUsername = CloudConfiguration.GetConfigurationSetting("acsUsername", null);
            string acsPassword = CloudConfiguration.GetConfigurationSetting("acsUserKey", null);

            var acsWrapper = new ServiceManagementWrapper(acsServiceNamespace, acsUsername, acsPassword);
            var relyingParty = acsWrapper.RetrieveRelyingParties().SingleOrDefault(rp => rp.Name.Contains(transportPartner));

            var keyValue = string.Empty;

            if (relyingParty != null)
            {
                var key = relyingParty.RelyingPartyKeys.FirstOrDefault();
                Debug.Assert(key != null, "Key should not be null");
                keyValue = Convert.ToBase64String(key.Value);
            }

            // Sample values for trustedAudience: 
            //  urn:OrderStatusUpdateQueue/Contoso
            //  urn:OrderStatusUpdateQueue/Fabrikam
            var trustedAudience = string.Format(
                "urn:{0}/{1}",
                CloudConfiguration.GetConfigurationSetting("orderStatusUpdateQueue", string.Empty),
                HttpUtility.UrlEncode(transportPartner));

            var validator = new TokenValidator("accesscontrol.windows.net", RoleEnvironment.GetConfigurationSettingValue("acsNamespace"), trustedAudience, keyValue);

            return validator.Validate(token);
        }
    }
}
