﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Farcaster.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Farcaster.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setting property {0}({1}).
        /// </summary>
        internal static string CallingProperty {
            get {
                return ResourceManager.GetString("CallingProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Site name for component cannot be changed..
        /// </summary>
        internal static string CannotChangeComponentSiteName {
            get {
                return ResourceManager.GetString("CannotChangeComponentSiteName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In class &apos;{0}&apos;, cannot inject value on property &apos;{1}&apos; because it is read-only..
        /// </summary>
        internal static string CannotInjectReadOnlyProperty {
            get {
                return ResourceManager.GetString("CannotInjectReadOnlyProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to While resolving dependencies for {2}, the provided type {1} is not compatible with {0}..
        /// </summary>
        internal static string TypeNotCompatible {
            get {
                return ResourceManager.GetString("TypeNotCompatible", resourceCulture);
            }
        }
    }
}
