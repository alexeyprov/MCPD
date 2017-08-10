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
    using Orders.Website.Models;
    using Guard = Orders.Shared.Guard;

    public class CustomerStore : ICustomerStore
    {
        private readonly RetryPolicy sqlCommandRetryPolicy;

        public CustomerStore()
        {
            // this policy is defined in the configurationfile
            this.sqlCommandRetryPolicy = RetryPolicyFactory.GetDefaultSqlCommandRetryPolicy();
            this.sqlCommandRetryPolicy.Retrying += (sender, args) => TraceHelper.TraceInformation("Retry in CustomerStore - Count:{0}, Delay:{1}, Exception:{2}", args.CurrentRetryCount, args.Delay, args.LastException);
        }

        public void Add(Customer customer)
        {
            Guard.CheckArgumentNull(customer, "customer");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var customerToSave = new Entities.Customer
                {
                    CustomerId = Guid.NewGuid(),
                    UserName = customer.UserName,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Email = customer.Email,
                    Phone = customer.Phone
                };

                database.Customers.AddObject(customerToSave);
                this.sqlCommandRetryPolicy.ExecuteAction(() => database.SaveChanges());
            }
        }

        public Customer FindOne(string userName)
        {
            Guard.CheckArgumentNullOrEmpty(userName, "userName");

            using (var database = TreyResearchModelFactory.CreateContext())
            {
                var customer =
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () => database.Customers.SingleOrDefault(c => c.UserName == userName));

                if (customer == null)
                {
                    return null;
                }

                return new Customer
                {
                    CustomerId = customer.CustomerId,
                    UserName = customer.UserName,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    City = customer.City,
                    State = customer.State,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country,
                    Email = customer.Email,
                    Phone = customer.Phone
                };
            }
        }

        public IEnumerable<Customer> FindAll()
        {
            using (var database = TreyResearchModelFactory.CreateContext())
            {
                return
                    this.sqlCommandRetryPolicy.ExecuteAction(
                        () =>
                        database.Customers.Select(
                            c =>
                            new Customer
                                {
                                    CustomerId = c.CustomerId,
                                    UserName = c.UserName,
                                    FirstName = c.FirstName,
                                    LastName = c.LastName,
                                    Address = c.Address,
                                    City = c.City,
                                    State = c.State,
                                    PostalCode = c.PostalCode,
                                    Country = c.Country,
                                    Phone = c.Phone,
                                    Email = c.Email
                                }));
            }
        }
    }
}