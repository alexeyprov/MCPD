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
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Web;
    using System.Web.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.IdentityModel.Web;
    using Microsoft.IdentityModel.Web.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
    using Microsoft.WindowsAzure;

    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            FederatedAuthentication.ServiceConfigurationCreated += this.OnServiceConfigurationCreated;
        }
        
        private void Application_EndRequest(object sender, EventArgs e)
        {
            if (this.Response.StatusCode == 401)
            {
                this.Response.ClearContent();
                this.Server.Transfer("~/401.htm");
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "This is what the framework expects.")]
        private void Application_Error(object sender, EventArgs e)
        {
            // Get reference to the source of the exception chain
            Exception ex = this.Server.GetLastError();
            ExceptionPolicy.HandleException(ex, "Log Policy");
        }

        private void OnServiceConfigurationCreated(object sender, ServiceConfigurationCreatedEventArgs e)
        {
            // Use the <serviceCertificate> to protect the cookies that are
            // sent to the client.
            List<CookieTransform> sessionTransforms =
                new List<CookieTransform>(
                    new CookieTransform[]
                        {
                            new DeflateCookieTransform(),
                            new RsaEncryptionCookieTransform(e.ServiceConfiguration.ServiceCertificate),
                            new RsaSignatureCookieTransform(e.ServiceConfiguration.ServiceCertificate)
                        });
            SessionSecurityTokenHandler sessionHandler = new SessionSecurityTokenHandler(sessionTransforms.AsReadOnly());

            e.ServiceConfiguration.SecurityTokenHandlers.AddOrReplace(sessionHandler);
        }

        private void WSFederationAuthenticationModule_RedirectingToIdentityProvider(object sender, RedirectingToIdentityProviderEventArgs e)
        {
            // In the Windows Azure environment, build a wreply parameter for  the SignIn request
            // that reflects the real address of the application.
            HttpRequest request = HttpContext.Current.Request;
            Uri requestUrl = request.Url;
            StringBuilder wreply = new StringBuilder();

            wreply.Append(requestUrl.Scheme); // e.g. "http" or "https"
            wreply.Append("://");
            wreply.Append(request.Headers["Host"] ?? requestUrl.Authority);
            wreply.Append(request.ApplicationPath);

            if (!request.ApplicationPath.EndsWith("/"))
            {
                wreply.Append("/");
            }

            e.SignInRequestMessage.Reply = wreply.ToString();
        }
    }
}