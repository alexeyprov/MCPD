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
    using System.Linq;
    using System.Web.Mvc;
    using Helpers;
    using Microsoft.IdentityModel.Claims;
    using Orders.Shared;
    using Orders.Website.CustomAttributes;
    using Orders.Website.DataStores;

    [LogAction]
    [AuthorizeAndRegisterUser]
    public class MyOrdersController : Controller
    {
        private readonly IOrderStore orderStore;

        public MyOrdersController(IOrderStore orderStore)
        {
            Guard.CheckArgumentNull(orderStore, "orderStore");

            this.orderStore = orderStore;
        }

        public ActionResult Index()
        {
            var identity = User.Identity as IClaimsIdentity;
            var orders = this.orderStore.FindByUser(identity.GetFederatedUsername());
            return View(orders.OrderBy(o => o.OrderDate));
        }
    }
}
