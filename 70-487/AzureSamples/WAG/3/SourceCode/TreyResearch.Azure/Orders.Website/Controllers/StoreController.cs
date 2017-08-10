//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.Controllers
{
    using System.Web.Mvc;
    using Orders.Shared;
    using Orders.Website.CustomAttributes;
    using Orders.Website.DataStores;

    [LogAction]
    public class StoreController : Controller
    {
        private readonly IProductStore productStore;

        public StoreController(IProductStore productStore)
        {
            Guard.CheckArgumentNull(productStore, "productStore");

            this.productStore = productStore;
        }

        public ActionResult Index()
        {
            var products = this.productStore.FindAll();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            var p = this.productStore.FindOne(id);
            return View(p);
        }
    }
}
