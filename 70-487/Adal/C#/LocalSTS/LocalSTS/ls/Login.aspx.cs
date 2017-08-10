﻿using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace LocalSTS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
        }

        protected void Login1_Authenticate( object sender, AuthenticateEventArgs e )
        {
            if ( Membership.ValidateUser(Login1.UserName, Login1.Password ) )
            {
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false );
            }
        }
    }
}