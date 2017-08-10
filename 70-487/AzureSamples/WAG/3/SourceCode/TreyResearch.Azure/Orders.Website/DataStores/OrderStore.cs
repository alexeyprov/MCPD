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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling;
    using Microsoft.Practices.TransientFaultHandling;
    using Orders.Shared.Helpers;
    using Orders.Website.DataStores.Entities;
    using Guard = Orders.Shared.Guard;
    using Order = Orders.Website.Models.Order;
    using OrderDetail = Orders.Website.DataStores.Entities.OrderDetail;
    using OrderStatus = Orders.Website.Models.OrderStatus;

    public class OrderStore : IOrderStore
    {
        private readonly RetryPolicy sqlCommandRetryPolicy;

        public OrderStore()
        {
            // this policy is defined in the configurationfile
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
            this.sqlCommandRetryPolicy.Retrying += (sender, args) => TraceHelper.TraceInformation("Retry in OrderStore - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
        }

        public IEnumerable<Order> FindByUser(string userName)
        {
            Guard.CheckArgumentNullOrEmpty(userName, "userName");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                return
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        database.Orders.Where(o => o.UserName == userName).Select(
                            o =>
                            new Order
                                {
                                    OrderId = o.OrderId,
                                    UserName = o.UserName,
                                    OrderDate = o.OrderDate,
                                    Address = o.Address,
                                    City = o.City,
                                    State = o.State,
                                    PostalCode = o.PostalCode,
                                    Country = o.Country,
                                    Phone = o.Phone,
                                    Email = o.Email,
                                    Total = o.Total,
                                    Status =
                                        database.OrderStatus.Where(s => s.OrderId == o.OrderId).OrderByDescending(
                                            s => s.Timestamp).Select(
                                                st => new OrderStatus
                                                    {
                                                        OrderId = st.OrderId,
                                                        Status = st.Status
                                                    }).FirstOrDefault(),
                                    TransportPartner = o.TransportPartner,
                                    TrackingId = o.TrackingId
                                }).ToList());
            }
        }

        public void Add(Order order)
        {
            Guard.CheckArgumentNull(order, "order");

            var orderId = Guid.NewGuid();

            var orderToSave = new Entities.Order
                {
                    OrderId = orderId, 
                    UserName = order.UserName, 
                    OrderDate = order.OrderDate, 
                    Address = order.Address, 
                    City = order.City, 
                    State = order.State, 
                    PostalCode = order.PostalCode, 
                    Country = order.Country, 
                    Phone = order.Phone, 
                    Email = order.Email, 
                    Total = order.OrderDetails.Sum(d => d.Quantity * d.Product.Price)
                };

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                database.Orders.AddObject(orderToSave);

                foreach (var orderDetail in order.OrderDetails)
                {
                    var detailToSave = new OrderDetail
                        {
                            OrderDetailId = Guid.NewGuid(),
                            ProductId = orderDetail.ProductId, 
                            OrderId = orderId, 
                            Quantity = orderDetail.Quantity 
                        };
                    database.OrderDetails.AddObject(detailToSave);
                }

                var status = new Entities.OrderStatus
                    {
                        OrderId = orderId, 
                        Status = "TreyResearch: Order placed", 
                        Timestamp = DateTime.UtcNow 
                    };
                database.OrderStatus.AddObject(status);

                var orderProcess = new OrderProcessStatus
                    {
                       OrderId = orderId, ProcessStatus = "pending process" 
                    };
                database.OrderProcessStatus.AddObject(orderProcess);

                this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());
                order.OrderId = orderId;
            }
        }
    }
}