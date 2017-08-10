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
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Security;
    using Helpers;
    using Microsoft.IdentityModel.Claims;
    using Microsoft.IdentityModel.Web;
    using Orders.Shared;
    using Orders.Website.CustomAttributes;
    using Orders.Website.DataStores;
    using Orders.Website.Models;

    [LogAction]
    public class AccountController : Controller
    {
        private readonly ICustomerStore customerStore;
        private readonly ICartStore cartStore;
        private readonly ICountryStore countryStore;
        private readonly IStateStore stateStore;

        public AccountController(ICustomerStore customerStore, ICartStore cartStore, ICountryStore countryStore, IStateStore stateStore)
        {
            Guard.CheckArgumentNull(customerStore, "customerStore");
            Guard.CheckArgumentNull(cartStore, "cartStore");
            Guard.CheckArgumentNull(countryStore, "countryStore");
            Guard.CheckArgumentNull(stateStore, "stateStore");

            this.customerStore = customerStore;
            this.cartStore = cartStore;
            this.countryStore = countryStore;
            this.stateStore = stateStore;
        }
        
        [HttpGet]
        public ActionResult LogOn()
        {
            ViewData["realm"] = this.GetRealm();
            ViewData["returnUrl"] = Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : this.GetRealm();
            ViewData["acsNamespace"] = CloudConfiguration.GetConfigurationSetting("acsNamespace", null);

            return View();
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            ShoppingCart.ClearCart(this.HttpContext);

            if (User.Identity.IsAuthenticated)
            {
                WSFederationAuthenticationModule fam = FederatedAuthentication.WSFederationAuthenticationModule;
                FormsAuthentication.SignOut();
                fam.SignOut(true); 
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            var identity = User.Identity as IClaimsIdentity;
            Debug.Assert(identity != null, "Identity should not be null");
            var email = identity.Name;
            var issuer = identity.GetOriginalIssuer();
            
            if (issuer.Contains("WindowsLiveID"))
            {
                // Windows Live does not offer the user's email address as a claim
                email = string.Empty;
            }

            var countries = this.countryStore.FindAll();
            ViewData["countries"] = countries;

            return View(new RegisterModel { Email = email });
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Guard.CheckArgumentNull(model, "model");

                // Attempt to register the user
                var identity = User.Identity as IClaimsIdentity;
                var userName = identity.GetFederatedUsername();
                this.UpdateShoppingCart(userName);
                
                var customer = new Customer
                {
                    UserName = userName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    PostalCode = model.PostalCode,
                    Country = model.Country.ToString(CultureInfo.InvariantCulture),
                    Phone = model.Phone,
                    Email = model.Email
                };

                this.customerStore.Add(customer);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Store");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult Privacy()
        {
            return View();
        }

        public PartialViewResult LoadState(string country)
        {
            ViewData["states"] = this.stateStore.FindAll();
            return PartialView(new Order { Country = country });
        }

        [NonAction]
        public void UpdateShoppingCart(string userName)
        {
            // Associate shopping cart items with logged-in user
            var cartId = ShoppingCart.GetCartId(this.HttpContext);
            this.cartStore.UpdateCart(userName, cartId);

            ShoppingCart.UpdateCartId(this.HttpContext, userName);
        }

        private string GetRealm()
        {
            Uri reqUrl = this.Request.Url;
            var realm = new StringBuilder();
            Debug.Assert(reqUrl != null, "reqUrl Should not be null");
            realm.Append(reqUrl.Scheme);
            realm.Append("://");
            realm.Append(this.Request.Headers["Host"] ?? reqUrl.Authority);
            realm.Append(this.Request.ApplicationPath);
            var applicationPath = this.Request.ApplicationPath;
            if (applicationPath != null && !applicationPath.EndsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                realm.Append("/");
            }

            return realm.ToString();
        }
    }
}
