//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.IdentityModel.Web;
    using Microsoft.IdentityModel.Web.Configuration;
    using Microsoft.Practices.Unity;
    using Microsoft.WindowsAzure.ServiceRuntime;
    using Orders.Shared;
    using Orders.Shared.Helpers;

    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly string Instance = RoleEnvironment.CurrentRoleInstance.Id;
        private IUnityContainer container;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Guard.CheckArgumentNull(filters, "filters");
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        protected void Application_Start()
        {
            TraceHelper.TraceInformation("Starting TreyResearch application");

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            FederatedAuthentication.ServiceConfigurationCreated += OnServiceConfigurationCreated;

            this.SetupDependencies();
        }

        protected void Application_End()
        {
            if (this.container != null)
            {
                this.container.Dispose();
                this.container = null;
            }
        }

        private static void OnServiceConfigurationCreated(object sender, ServiceConfigurationCreatedEventArgs e)
        {
            // Use the <serviceCertificate> to protect the cookies that are
            // sent to the client.
            var sessionTransforms =
                new List<CookieTransform>(
                    new CookieTransform[] 
                    {
                        new DeflateCookieTransform(), 
                        new RsaEncryptionCookieTransform(e.ServiceConfiguration.ServiceCertificate),
                        new RsaSignatureCookieTransform(e.ServiceConfiguration.ServiceCertificate)  
                    });

            var sessionHandler = new SessionSecurityTokenHandler(sessionTransforms.AsReadOnly());
            e.ServiceConfiguration.SecurityTokenHandlers.AddOrReplace(sessionHandler);
        }

        private void SetupDependencies()
        {
            this.container = new UnityContainer();
            ContainerBootstrapper.RegisterTypes(this.container);
            var dependencyResolver = new UnityDependencyResolver(this.container);
            DependencyResolver.SetResolver(dependencyResolver);
        }
    }
}