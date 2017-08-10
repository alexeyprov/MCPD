using System;
using System.Web.Routing;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Nsquared2.Web.Mvc
{

    public static class RouteTableManager
    {
        public static void RegisterRoutes()
        {
            RouteTableManager.RegisterRoutes(RouteTable.Routes);
        }


        public static void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            RouteTableSection routesTableSection = GetRouteTableConfigurationSection();

            if (routesTableSection != null && routesTableSection.Routes.Count > 0)
            {
                for (int routeIndex = 0; routeIndex < routesTableSection.Routes.Count; routeIndex++)
                {
                    RouteConfigElement route = routesTableSection.Routes[routeIndex] as RouteConfigElement;

                    routes.Add(route.Name, new Route(
                                                route.Url,
                                                GetDefaults(route),
                                                GetConstraints(route),
                                                GetInstanceOfRouteHandler(route)));
                }
            }
        }


        private static RouteTableSection GetRouteTableConfigurationSection()
        {
            RouteTableSection routesTableSection;

            try
            {
                routesTableSection = (RouteTableSection)WebConfigurationManager.GetSection("routeTable");
                return routesTableSection;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Can't find section <routeTable> in the configuration file", e);
            }
        }


        private static IRouteHandler GetInstanceOfRouteHandler(RouteConfigElement route)
        {
            IRouteHandler routeHandler;

            if (string.IsNullOrEmpty(route.RouteHandlerType))
                routeHandler = new MvcRouteHandler();
            else
            {
                try
                {
                    Type routeHandlerType = Type.GetType(route.RouteHandlerType);
                    routeHandler = Activator.CreateInstance(routeHandlerType) as IRouteHandler;
                }
                catch (Exception e)
                {
                    throw new ApplicationException(
                                 string.Format("Can't create an instance of IRouteHandler {0}", route.RouteHandlerType),
                                 e);
                }

            }

            return routeHandler;
        }


        private static RouteValueDictionary GetConstraints(RouteConfigElement route)
        {
            return GetDictionaryFromAttributes(route.Constraints.Attributes);
        }


        private static RouteValueDictionary GetDefaults(RouteConfigElement route)
        {
            return GetDictionaryFromAttributes(route.Defaults.Attributes);
        }


        private static RouteValueDictionary GetDataTokens(RouteConfigElement route)
        {
            return GetDictionaryFromAttributes(route.DataTokens.Attributes);
        }


        private static RouteValueDictionary GetDictionaryFromAttributes(Dictionary<string, string> attributes)
        {
            RouteValueDictionary dataTokensDictionary = new RouteValueDictionary();

            foreach (var dataTokens in attributes)
                dataTokensDictionary.Add(dataTokens.Key, dataTokens.Value);

            return dataTokensDictionary;

        }
    }
}