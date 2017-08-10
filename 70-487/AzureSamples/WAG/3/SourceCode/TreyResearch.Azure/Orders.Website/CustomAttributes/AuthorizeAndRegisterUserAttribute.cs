//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.CustomAttributes
{
    using System.Diagnostics;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Helpers;
    using Microsoft.IdentityModel.Claims;
    using Orders.Shared;
    using Orders.Website.DataStores;

    public class AuthorizeAndRegisterUserAttribute : AuthorizeAttribute
    {
        private readonly ICustomerStore customerStore;

        public AuthorizeAndRegisterUserAttribute() : this(new CustomerStore())
        {
        }

        public AuthorizeAndRegisterUserAttribute(ICustomerStore customerStore)
        {
            this.customerStore = customerStore;            
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            Guard.CheckArgumentNull(filterContext, "filterContext");

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.OnAuthorization(filterContext);
                return;
            }

            var federatedUsername = ((IClaimsIdentity)filterContext.HttpContext.User.Identity).GetFederatedUsername();
            var customer = this.customerStore.FindOne(federatedUsername);
            if (customer == null)
            {
                // Redirect to registration page.
                Debug.Assert(filterContext.HttpContext.Request.Url != null, "Url should not be null");
                var redirectInfo = new RouteValueDictionary
                    {
                        { "controller", "Account" }, 
                        { "action", "Register" }, 
                        { "returnUrl", filterContext.HttpContext.Request.Url.AbsolutePath }
                    };

                filterContext.Result = new RedirectToRouteResult(redirectInfo);
            }
        }
    }
}