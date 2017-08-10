//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense
{
    using System;
    using System.Text;
    using System.Web;

    public partial class Site : System.Web.UI.MasterPage
    {
        protected void FederatedPassiveSignInStatus1SignedOut(object sender, EventArgs e)
        {
            HttpRequest request = HttpContext.Current.Request;
            Uri requestUrl = request.Url;
            StringBuilder wreply = new StringBuilder();

            wreply.Append(requestUrl.Scheme);     // e.g. "http" or "https"
            wreply.Append("://");
            wreply.Append(request.Headers["Host"] ?? requestUrl.Authority);
            wreply.Append(request.ApplicationPath);

            if (!request.ApplicationPath.EndsWith("/"))
            {
                wreply.Append("/");
            }

            this.FederatedPassiveSignInStatus1.SignOutPageUrl = wreply.ToString();

            this.Session.Abandon();
            var sessionCookie = new HttpCookie("ASP.NET_SessionId", string.Empty);
            sessionCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(sessionCookie);
        }
    }
}
