﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfLocalizationResx.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WpfLocalizationResx.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello great World.
        /// </summary>
        public static string HelloWorld {
            get {
                return ResourceManager.GetString("HelloWorld", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 10.
        /// </summary>
        public static string HelloWorldMargin {
            get {
                return ResourceManager.GetString("HelloWorldMargin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 20.
        /// </summary>
        public static string HelloWorldWidth {
            get {
                return ResourceManager.GetString("HelloWorldWidth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please note that some properties bound to x:Static bindings will NOT update automatically, while others other that are bound to the ResExtension do. This is done on purpose to demonstrate the difference in behavior. To see the x:Static bindings change exit this form and re-load it from the main menu..
        /// </summary>
        public static string LanguageChangedMessageResxForm {
            get {
                return ResourceManager.GetString("LanguageChangedMessageResxForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Double similar to 20.
        /// </summary>
        public static double lblHelloWorld_Width {
            get {
                object obj = ResourceManager.GetObject("lblHelloWorld.Width", resourceCulture);
                return ((double)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ready.
        /// </summary>
        public static string Ready {
            get {
                return ResourceManager.GetString("Ready", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Today.
        /// </summary>
        public static string Today {
            get {
                return ResourceManager.GetString("Today", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Yesterday.
        /// </summary>
        public static string Yesterday {
            get {
                return ResourceManager.GetString("Yesterday", resourceCulture);
            }
        }
    }
}
