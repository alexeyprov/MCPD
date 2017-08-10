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
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Helpers;
    using Microsoft.IdentityModel.Claims;
    using Orders.Shared;
    using Orders.Website.CustomAttributes;
    using Orders.Website.DataStores;
    using Orders.Website.Models;

    [LogAction]
    [AuthorizeAndRegisterUser]
    public class CheckoutController : Controller
    {
        private readonly IOrderStore ordersStore;
        private readonly ICustomerStore customerStore;
        private readonly ICartStore cartStore;
        private readonly ICountryStore countryStore;
        private readonly IStateStore stateStore;

        public CheckoutController(IOrderStore ordersStore, ICustomerStore customerStore, ICartStore cartStore, ICountryStore countryStore, IStateStore stateStore)
        {
            Guard.CheckArgumentNull(ordersStore, "ordersStore");
            Guard.CheckArgumentNull(customerStore, "customerStore");
            Guard.CheckArgumentNull(cartStore, "cartStore");
            Guard.CheckArgumentNull(countryStore, "countryStore");
            Guard.CheckArgumentNull(stateStore, "stateStore");

            this.ordersStore = ordersStore;
            this.customerStore = customerStore;
            this.cartStore = cartStore;
            this.countryStore = countryStore;
            this.stateStore = stateStore;
        }

        [HttpGet]
        public ActionResult AddressAndPayment()
        {
            var cartId = ShoppingCart.GetCartId(this.HttpContext);
            var cartItems = this.cartStore.FindCartItems(cartId);
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            var identity = User.Identity as IClaimsIdentity;
            var customer = this.customerStore.FindOne(identity.GetFederatedUsername());

            var order = new Order
            {
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
                Phone = customer.Phone,
                Email = customer.Email,
            };

            var countries = this.countryStore.FindAll();
            ViewData["countries"] = countries;

            return View(order);
        }

        [HttpPost]
        [HandleError]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            this.TryUpdateModel(order);

            var identity = User.Identity as IClaimsIdentity;
            var userName = identity.GetFederatedUsername();

            var cartId = ShoppingCart.GetCartId(this.HttpContext);
            var cartItems = this.cartStore.FindCartItems(cartId);

            order.OrderDetails = cartItems.Select(i => new OrderDetail { ProductId = i.ProductId, Quantity = i.Count, Product = i.Product });
            order.UserName = userName;
            order.OrderDate = DateTime.Now;

            // Save Order
            this.ordersStore.Add(order);

            this.cartStore.DeleteItems(cartId);

            return RedirectToAction("Complete", "Checkout", new { orderId = order.OrderId });
        }

        [HttpGet]
        public ActionResult Complete(Guid? orderId)
        {
            if (orderId.HasValue)
            {
                return View(orderId.Value);
            }
            return View(string.Empty);
        }

        public PartialViewResult LoadState(string country, string state)
        {
            ViewData["states"] = this.stateStore.FindAll();
            return PartialView(new Order { Country = country, State = state });
        }
    }
}
