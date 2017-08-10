using System;
using System.Configuration;
using System.Collections;


namespace Nsquared2.Web.Mvc
{
    public class RouteTableSection : ConfigurationSection
    {

        RouteConfigElement url;

        public RouteTableSection()
        {
            url = new RouteConfigElement();
        }

        
        [ConfigurationProperty("routes", IsDefaultCollection = false)]
        public RouteCollection Routes
        {
            get
            {
                RouteCollection urlsCollection =
                (RouteCollection)base["routes"];
                return urlsCollection;
            }
        }


        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
        }


        protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            return base.SerializeSection(parentElement, name, saveMode);
        }

    }
}
