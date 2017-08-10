//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace HeadOffice
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;
    using HeadOffice.Services;
    using Microsoft.ServiceBus;

    public class MvcApplication : System.Web.HttpApplication
    {
        public static Dictionary<string, CancellationTokenSource> TokenSourceList { get; set; }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
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
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            this.OpenServiceHost();
            TokenSourceList = new Dictionary<string, CancellationTokenSource>();
        }

        protected void Session_Start()
        {
            Session["init"] = 0;
        }

        private void OpenServiceHost()
        {
            //// We are opening a ServiceHost here only for sample purposes. To properly connect a WCF Service in IIS using Service Bus relay, 
            //// please refer to the Exercise 2 in the Hands-On Lab "Part 1: Introduction to the AppFabricService Bus" in the Windows Azure Platform Training Kit
            //// http://msdn.microsoft.com/en-us/windowsazure/wazplatformtrainingcourse_introtoappfabricsb2010part1_topic4#_Toc303678125

            var serviceNamespace = System.Configuration.ConfigurationManager.AppSettings["ServiceBusNamespace"];
            var uriScheme = System.Configuration.ConfigurationManager.AppSettings["UriScheme"];
            var servicePath = System.Configuration.ConfigurationManager.AppSettings["RelayServicePath"];

            var address = ServiceBusEnvironment.CreateServiceUri(uriScheme, serviceNamespace, servicePath);

            var host = new ServiceHost(typeof(OrdersStatistics), address);
            host.Open();
        }
    }
}