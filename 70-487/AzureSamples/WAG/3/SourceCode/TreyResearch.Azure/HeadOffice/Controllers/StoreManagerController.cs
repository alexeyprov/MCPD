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
    using HeadOffice;
    using HeadOffice.DataStores;
    using HeadOffice.Models;

    [HandleError]
    public class StoreManagerController : Controller
    {
        private readonly IProductStore productStore;

        public StoreManagerController() : this(new ProductStore())
        {
        }

        public StoreManagerController(IProductStore productStore)
        {
            Guard.CheckArgumentNull(productStore, "productStore");

            this.productStore = productStore;
        }

        public ActionResult Index()
        {
            var products = this.productStore.FindAll();
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                this.productStore.Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var p = this.productStore.FindOne(id);
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            this.productStore.Update(product);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var p = this.productStore.FindOne(id);
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.productStore.Delete(id);
            return RedirectToAction("Index");
        }

        public ViewResult Details(int id)
        {
            var p = this.productStore.FindOne(id);
            return View(p);
        }
    }
}
