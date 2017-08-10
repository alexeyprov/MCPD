using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace NetHttpBindingLibrary
{
    public class NetHttpBindingConfigurationElement : StandardBindingElement
    {
        public NetHttpBindingConfigurationElement(string configurationName)
            : base(configurationName)
        {
        }

        public NetHttpBindingConfigurationElement()
            : this(null)
        {
        }

        protected override Type BindingElementType
        {
            get
            {
                return typeof(NetHttpBinding);
            }
        }

        [ConfigurationProperty("transferMode", DefaultValue = TransferMode.Buffered)]
        public TransferMode TransferMode
        {
            get
            {
                return ((TransferMode)(base["transferMode"]));
            }
            set
            {
                base["transferMode"] = value;
            }
        }

        [ConfigurationProperty("useDefaultWebProxy", DefaultValue = false)]
        public bool UseDefaultWebProxy
        {
            get
            {
                return ((bool)(base["useDefaultWebProxy"]));
            }
            set
            {
                base["useDefaultWebProxy"] = value;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                ConfigurationPropertyCollection properties = base.Properties;
                properties.Add(new ConfigurationProperty("transferMode", typeof(TransferMode), TransferMode.Buffered));
                properties.Add(new ConfigurationProperty("useDefaultWebProxy", typeof(bool), true));
                return properties;
            }
        }

        protected override void InitializeFrom(Binding binding)
        {
            base.InitializeFrom(binding);
            NetHttpBinding netHttpBinding = ((NetHttpBinding)(binding));
            this.TransferMode = netHttpBinding.TransferMode;
            this.UseDefaultWebProxy = netHttpBinding.UseDefaultWebProxy;
        }

        protected override void OnApplyConfiguration(Binding binding)
        {
            if ((binding == null))
            {
                throw new System.ArgumentNullException("binding");
            }
            if ((binding.GetType() != typeof(NetHttpBinding)))
            {
                throw new System.ArgumentException(
                    string.Format(CultureInfo.CurrentCulture,
                        "Invalid type for binding. Expected type: {0}. Type passed in: {1}.",
                        typeof(NetHttpBinding).AssemblyQualifiedName, binding.GetType().AssemblyQualifiedName));
            }
            NetHttpBinding netHttpBinding = ((NetHttpBinding)(binding));
            netHttpBinding.TransferMode = this.TransferMode;
            netHttpBinding.UseDefaultWebProxy = this.UseDefaultWebProxy;
        }
    }
}
