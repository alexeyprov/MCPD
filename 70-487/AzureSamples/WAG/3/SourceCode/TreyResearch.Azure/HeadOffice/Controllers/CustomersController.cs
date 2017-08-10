//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice.Controllers
{
    using System.Web.Mvc;
    using HeadOffice.DataStores;

    [HandleError]
    public class CustomersController : Controller
    {
        private readonly ICustomerStore customerStore;

        public CustomersController() : this(new CustomerStore())
        {
        }

        public CustomersController(ICustomerStore customerStore)
        {
            this.customerStore = customerStore;
        }

        public ActionResult Index()
        {
            var customers = this.customerStore.FindAll();
            return View(customers);
        }
    }
}
