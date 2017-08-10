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
    using System.Web.Security;

    public partial class SimulatedWindowsAuthentication : System.Web.UI.Page
    {
        protected void ContinueButtonClick(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectFromLoginPage(this.UserList.SelectedValue, false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.RedirectFromLoginPage(this.User.Identity.Name, false);
            }

            if (!Page.IsPostBack)
            {
                if (Request.IsAuthenticated && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                {
                    this.Response.Redirect("~/401.htm");
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.ViewStateUserKey = this.User.Identity.Name;
        }
    }
}
