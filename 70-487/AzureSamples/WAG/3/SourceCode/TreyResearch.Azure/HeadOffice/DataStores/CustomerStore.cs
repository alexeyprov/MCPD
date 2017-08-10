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
    using HeadOffice.DataStores.Entities;
    using Customer = HeadOffice.Models.Customer;

    public class CustomerStore : ICustomerStore
    {
        public void Add(Customer customer)
        {
            Guard.CheckArgumentNull(customer, "customer");

            using (var database = new TreyResearchDataModelContainer())
            {
                var customerToSave = new Entities.Customer
                                         {
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

                database.Customer.AddObject(customerToSave);
                database.SaveChanges();
            }
        }

        public IEnumerable<Customer> FindAll()
        {
            using (var database = new TreyResearchDataModelContainer())
            {
                return database.Customer.Select(c => new Customer
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
                                                          }).ToList();
            }
        }

        public Customer FindOne(string userName)
        {
            Guard.CheckArgumentNullOrEmpty(userName, "userName");

            using (var database = new TreyResearchDataModelContainer())
            {
                var customer = database.Customer.SingleOrDefault(c => c.UserName == userName);
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
    }
}