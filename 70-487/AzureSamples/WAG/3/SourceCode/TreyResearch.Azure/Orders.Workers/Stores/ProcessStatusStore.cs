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
    using System.Data;
    using System.Linq;
    using System.Transactions;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Orders.Shared;
    using Orders.Shared.Helpers;
    using Orders.Workers.Stores.Entities;

    public class ProcessStatusStore : IProcessStatusStore
    {
        private readonly RetryPolicy sqlCommandRetryPolicy;
        private readonly RetryPolicy sqlConnectionRetryPolicy;

        public ProcessStatusStore()
        {
            // this policy is defined in the configurationfile
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
            this.sqlConnectionRetryPolicy = RetryPolicyFactory.GetDefaultSqlConnectionRetryPolicy();
            this.sqlCommandRetryPolicy.Retrying += (sender, args) => TraceHelper.TraceWarning("Retry in ProcessStatusStore - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
        }

        public Guid LockOrders(string roleInstanceId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var batchId = Guid.NewGuid();
                const string CommandText = "UPDATE TOP(32) OrderProcessStatus SET LockedBy = {0}, LockedUntil = {1}, BatchId = {2} WHERE (ProcessStatus != {3} AND ProcessStatus != {4} ) AND (LockedUntil < {5} OR LockedBy IS NULL)";
                this.sqlCommandRetryPolicy.ExecuteAction(
                    () =>
                    database.ExecuteStoreCommand(
                        CommandText, roleInstanceId, DateTime.UtcNow.AddSeconds(320), batchId, ProcessStatus.Processed, ProcessStatus.CriticalError, DateTime.UtcNow));

                return batchId;
            }
        }

        public IEnumerable<Models.OrderProcessStatus> GetLockedOrders(string roleInstanceId, Guid batchId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                return
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        database.OrderProcessStatus.Where(
                            o =>
                            o.LockedBy.Equals(roleInstanceId, StringComparison.OrdinalIgnoreCase)
                            && o.BatchId == batchId).Select(
                                op =>
                                new Models.OrderProcessStatus
                                    {
                                        LockedBy = op.LockedBy,
                                        LockedUntil = op.LockedUntil,
                                        OrderId = op.OrderId,
                                        ProcessStatus = op.ProcessStatus,
                                        Order =
                                            new Models.Order
                                                {
                                                    OrderId = op.Order.OrderId,
                                                    UserName = op.Order.UserName,
                                                    OrderDate = op.Order.OrderDate,
                                                    Address = op.Order.Address,
                                                    City = op.Order.City,
                                                    State = op.Order.State,
                                                    PostalCode = op.Order.PostalCode,
                                                    Country = op.Order.Country,
                                                    Phone = op.Order.Phone,
                                                    Email = op.Order.Email,
                                                    Total = op.Order.Total
                                                }
                                    }).ToList());
            }
        }

        public Models.OrderProcessStatus GetByOrderId(Guid orderId)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var processStatus =
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () => database.OrderProcessStatus.SingleOrDefault(o => o.OrderId == orderId));
                return new Models.OrderProcessStatus
                {
                    OrderId = processStatus.OrderId,
                    LockedBy = processStatus.LockedBy,
                    LockedUntil = processStatus.LockedUntil,
                    ProcessStatus = processStatus.ProcessStatus
                };
            }
        }

        public void SendComplete(Guid orderId, string transportPartner)
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                try
                {
                    using (var t = new TransactionScope())
                    {
                        // avoids the transaction being promoted.
                        this.sqlConnectionRetryPolicy.ExecuteAction(() => database.Connection.Open());

                        var processStatus =
                            this.sqlCommandRetryPolicy.ExecuteAction(
                                () => database.OrderProcessStatus.SingleOrDefault(o => o.OrderId == orderId));
                        processStatus.ProcessStatus = ProcessStatus.Processed;
                        processStatus.LockedBy = null;
                        processStatus.LockedUntil = null;
                        this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());

                        var status = new OrderStatus { OrderId = orderId, Status = "TreyResearch: Order sent to transport partner", Timestamp = DateTime.UtcNow };
                        database.OrderStatus.AddObject(status);
                        this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());

                        var order =
                            this.sqlCommandRetryPolicy.ExecuteAction(
                                () => database.Order.SingleOrDefault(o => o.OrderId == orderId));
                        order.TransportPartner = transportPartner;
                        this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());

                        t.Complete();
                    }
                }
                catch (UpdateException ex)
                {
                    // There was probably a deadlock or a PK constraint violation due to overlapping processes. 
                    // The order will be processed by the next Worker Role that retrieves it.
                    TraceHelper.TraceWarning(ex.Message);
                }
                finally
                {
                    TraceHelper.TraceInformation("NewOrderJob: The Order '{0}' was processed successfully.", orderId.ToString());

                    if (database.Connection.State == ConnectionState.Open)
                    {
                        database.Connection.Close();
                    }
                }
            }
        }

        public void UpdateWithError(Exception exception, Guid orderId)
        {
            TraceHelper.TraceWarning("NewOrderJob: The Order '{0}' couldn't be processed. Error details: {1}", orderId.ToString(), exception.ToString());

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var processStatus =
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () => database.OrderProcessStatus.SingleOrDefault(o => o.OrderId == orderId));
                processStatus.ProcessStatus = ProcessStatus.Error;
                processStatus.LockedBy = null;
                processStatus.LockedUntil = null;
                processStatus.RetryCount = processStatus.RetryCount + 1;

                var newOrderJobRetryCountCheck = int.Parse(CloudConfiguration.GetConfigurationSetting("NewOrderJobRetryCountCheck", "3"));

                if (processStatus.RetryCount > newOrderJobRetryCountCheck)
                {
                    processStatus.ProcessStatus = ProcessStatus.CriticalError;
                    TraceHelper.TraceError("NewOrderJob: The Order '{0}' has reached {1} retries. This order requires manual intervention.", orderId.ToString(), processStatus.RetryCount);
                }

                this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());
            }
        }
    }
}
