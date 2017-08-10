using System;
using System.Configuration;
using System.Collections.Generic;

namespace Nsquared2.Web.Mvc
{

    public class RouteChildElement : ConfigurationElement
    {
        private Dictionary<string, string> attributes = new Dictionary<string, string>();


        public Dictionary<string, string> Attributes
        {
            get { return this.attributes; }
        }


        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            attributes.Add(name, value);
            return true;
        }
    }
}