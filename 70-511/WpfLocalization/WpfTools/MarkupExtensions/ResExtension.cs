using System;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Resources;
using WpfTools.Properties;
using System.Threading;
using System.Reflection;

namespace WpfTools
{
    /// <summary>
    /// The Res markup extension returns localized values from 
    /// 
    /// To use:
    ///   * Make sure you register the namespace of this extension in the document header
    ///     xmlns:local="clr-namespace:WpfLocalizationResx,WpfLocalizationResx"
    ///   * Embed the extension as follows:
    ///     Content="{local:Res Id=HelloWorld,Default=Hello World}"   // gets from Resources
    ///     Content="{local:Res Id=HelloWorld,Default=Hello World,ResourceSet=WpfLocalizationResx.ResxResource,Format=You said: \{0\}"    
    ///     in addition you can pass Format (string.Format() string) and Convert (WPF Type Converter) as parameters
    ///     
    /// This extension only works with resources specific to the current assembly
    /// This extension requires that you have a Properties.Resources object defined
    /// in order to find the default resource set
    /// </summary>
    [MarkupExtensionReturnType(typeof(object)), Localizability(LocalizationCategory.NeverLocalize)]
    public class ResExtension : MarkupExtension
    {
        /// <summary>
        /// Type in the assembly where where resources are to be loaded from
        /// 
        /// Here we use the Resources property since we're in WPF application
        /// project. If you extract this into a sepearte assembly though you'll
        /// need to explicitly assign a type on application startup.
        /// </summary>
        //protected static Type ResourceType = null;
        protected static Assembly DefaultResourceAssembly = null;

        /// <summary>
        /// Optional default ResourceManager
        /// </summary>
        protected static ResourceManager DefaultResourceManager = null;

        /// <summary>
        /// Initializes the resource manager so that we can find resources.
        /// Specify a default resource manager that can be accessed without 
        /// ResourceSet prefix
        /// </summary>
        /// <param name="defaultResourceManager"></param>
        public static void InitializeResExtension(Type stronglyTypedResources)
        {
            if (stronglyTypedResources == null)
                throw new ArgumentException(Resources.NoStronglyTypedResourcesTypeProvided);

            DefaultResourceManager = stronglyTypedResources.GetProperty("ResourceManager", BindingFlags.Static | BindingFlags.Public).GetValue(stronglyTypedResources, null) as ResourceManager;
            DefaultResourceAssembly = stronglyTypedResources.Assembly;
        }

        /// <summary>
        /// Cached ResourceManagers for each ResourceSet supported requested.       
        /// </summary>
        private static  Dictionary<string,ResourceManager> ResourceManagers = new Dictionary<string,ResourceManager>();

        /// <summary>
        /// Caches the depending target object
        /// </summary>
        private DependencyObject _targetObject;

        /// <summary>
        /// Caches the depending target property
        /// </summary>
        private DependencyProperty _targetProperty;

        /// <summary>
        /// Caches the resolved default type converter
        /// </summary>
        private TypeConverter _typeConverter;

        /// <summary>
        /// Cache the ServiceProvider
        /// </summary>
        private IServiceProvider _serviceProvider;

        
        /// <summary>
        /// 
        /// </summary>
        public ResExtension()
        {            

        }

        static ResExtension()
        {
            //DefaultResourceAssembly = Assembly.GetEntryAssembly();            
        }

        /// <summary>
        /// Allow calling the extension with a default parameter (id).
        /// Content="{localRes HelloWorld}"
        /// </summary>
        /// <param name="param">The key that specifies a localization </param>
        public ResExtension(string id) : this()
        {
            Id = id;
        }


        /// <summary>
        /// The ResourceId to retrieve
        /// </summary>
        /// <value>The key.</value>
        [ConstructorArgument("Id")]
        public string Id { get; set; }


        /// <summary>
        /// A default value that is returned when the the resource
        /// cannot be resolved. Also returned in the designer if
        /// there are no resources available.
        /// </summary>
        /// <value>The default value.</value>
        [ConstructorArgument("Default")]
        public object Default { get; set; }

        /// <summary>
        /// Optional Resource Set Name. If not provided it's
        /// assumed you want to access the global Resources
        /// resource set in Properties.Resources
        /// </summary>
        [ConstructorArgument("ResourceSet")]
        public string ResourceSet { get; set; }

        /// <summary>
        /// A string.Format format string that is applied.
        /// Note when provided the result value is always 
        /// converted into a string.
        /// </summary>
        /// <value>The format.</value>
        [ConstructorArgument("Format")]
        public string Format { get; set; }
        
        /// <summary>
        /// Allows to specify a custom Value Converter using standard WPF syntax.
        /// </summary>
        /// <value>The converter.</value>
        [ConstructorArgument("Converter")]
        public IValueConverter Converter { get; set; }


        /// <summary>
        /// The core method to provide a localized value.
        /// 
        /// Note the value can also be a non-string value to set non-string properties
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Resolve the depending object and property
            if (_targetObject == null)
            {
                
                var targetHelper = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
                _targetObject = targetHelper.TargetObject as DependencyObject;
                _targetProperty = targetHelper.TargetProperty as DependencyProperty;
                _typeConverter = TypeDescriptor.GetConverter(_targetProperty.PropertyType);
            }
            _serviceProvider = serviceProvider;

            return ProvideValueInternal();
        }

        /// <summary>
        /// Retrieves a resource manager for the appropriate ResourceSet
        /// By default the 'global' Resource
        /// </summary>
        /// <param name="resourceSet"></param>
        /// <returns></returns>
        private ResourceManager GetResourceManager(string resourceSet)
        {
            if (string.IsNullOrEmpty(resourceSet))                
                return DefaultResourceManager ?? null;
            
            if (ResExtension.ResourceManagers.ContainsKey(resourceSet))                
                return ResExtension.ResourceManagers[resourceSet];

            ResourceManager man = new ResourceManager(resourceSet, DefaultResourceAssembly);
            ResourceManagers.Add(resourceSet, man);
            return man;
        }

        /// <summary>
        /// Internal value retrieval
        /// </summary>
        /// <returns></returns>
        private object ProvideValueInternal()
        {
            // Get a cached resource manager for this resource set
            ResourceManager resMan = this.GetResourceManager(this.ResourceSet);
            object localized = null;

            bool resError = false;

            // Get the localized value 
            if (resMan != null)
            {                
                try
                {
                    localized = resMan.GetObject(Id);
                }
                catch 
                { 
                    resError = true; 
                }
            }
            
            // Convert if a type converter is availalbe
            if (localized != null && _typeConverter != null && _typeConverter.CanConvertFrom(localized.GetType()))            
                localized = _typeConverter.ConvertFrom(localized);
            

            // If the value is null, use the Default value if available
            if (localized == null && this.Default != null)
                localized = Default;
            
            // If no fallback value is available, return the key
            if (localized == null)
            {
                if (_targetProperty != null && _targetProperty.PropertyType == typeof(string))               
                    // Return the key surrounded by question marks for string type properties
                    localized = string.Concat("?", Id, "?");                
                else                
                    // Return the UnsetValue for all other types of dependency properties
                    return DependencyProperty.UnsetValue;
                
            }

            // Apply a type converter if one was specified
            if (Converter != null)            
                localized = Converter.Convert(localized, _targetProperty.PropertyType, null, CultureInfo.CurrentCulture);
            

            // Format if a format string was provided
            if (Format != null)            
                localized = string.Format(CultureInfo.CurrentUICulture,Format, localized);


            // If there's was an error retrieving resource add a * to the end of string
            if (resError && localized is string)
                localized += "*";
            
            return localized;
        }
    }
}
