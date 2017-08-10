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
    using System.Web;

    public partial class Site : System.Web.UI.MasterPage
    {
        protected void LoginStatusOnLoggedOut(object sender, EventArgs e)
        {
            this.Session.Abandon();
            var sessionCookie = new HttpCookie("ASP.NET_SessionId", string.Empty);
            sessionCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(sessionCookie);
            Response.Redirect("default.aspx");
        }
    }
}
