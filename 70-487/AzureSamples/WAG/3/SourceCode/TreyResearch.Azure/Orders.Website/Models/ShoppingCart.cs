//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.Models
{
    using System;
    using System.Web;
    using Orders.Shared;

    public static class ShoppingCart
    {
        public const string CartSessionKey = "CartId";

        // We're using HttpContextBase to allow access to cookies.
        public static string GetCartId(HttpContextBase context)
        {
            Guard.CheckArgumentNull(context, "context");

            if (context.Request.Cookies[CartSessionKey] == null)
            {
                var cookie = new HttpCookie(CartSessionKey)
                    {
                        Expires = DateTime.Now.AddDays(1)
                    };

                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    cookie.Value = context.User.Identity.Name;
                    context.Response.Cookies.Add(cookie);
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    cookie.Value = tempCartId.ToString();
                    context.Response.Cookies.Add(cookie);
                }
            }

            return context.Request.Cookies[CartSessionKey].Value;
        }

        public static void UpdateCartId(HttpContextBase context, string userName)
        {
            Guard.CheckArgumentNull(context, "context");

            if (context.Request.Cookies[CartSessionKey] != null)
            {
                context.Response.Cookies[CartSessionKey].Value = userName;
            }
        }

        public static void ClearCart(HttpContextBase context)
        {
            Guard.CheckArgumentNull(context, "context");

            if (context.Request.Cookies[CartSessionKey] != null)
            {
                var cookie = context.Request.Cookies[CartSessionKey];
                cookie.Expires = DateTime.Now.AddYears(-1);
                context.Response.Cookies.Set(cookie);
            }
        }
    }
}