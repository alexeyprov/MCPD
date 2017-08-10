using System.Configuration;
using MvcRouteConfig.Elements;

namespace MvcRouteConfig
{
    public class RouteSection : ConfigurationSection
    {
        [ConfigurationProperty("routes", IsDefaultCollection = false)]
        public RouteCollection Routes
        {
            get { return base["routes"] as RouteCollection; }
        }
    }
}