﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImmutableAnalyzer {
    using System;
    using System.Reflection;
    
    
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ImmutableAnalyzer.Resources", typeof(Resources).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to Immutable Types should not have methods.
        /// </summary>
        internal static string AnalyzerDescriptionMethod {
            get {
                return ResourceManager.GetString("AnalyzerDescriptionMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Types must not have seters.
        /// </summary>
        internal static string AnalyzerDescriptionProperty {
            get {
                return ResourceManager.GetString("AnalyzerDescriptionProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Types must not have void methods.
        /// </summary>
        internal static string AnalyzerDescriptionVoidMethod {
            get {
                return ResourceManager.GetString("AnalyzerDescriptionVoidMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Type has method &apos;{0}&apos;.
        /// </summary>
        internal static string AnalyzerMessageFormatMethod {
            get {
                return ResourceManager.GetString("AnalyzerMessageFormatMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Type has seter &apos;{0}&apos;.
        /// </summary>
        internal static string AnalyzerMessageFormatProperty {
            get {
                return ResourceManager.GetString("AnalyzerMessageFormatProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Type has void method &apos;{0}&apos;.
        /// </summary>
        internal static string AnalyzerMessageFormatVoidMethod {
            get {
                return ResourceManager.GetString("AnalyzerMessageFormatVoidMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Type has method.
        /// </summary>
        internal static string AnalyzerTitleMethod {
            get {
                return ResourceManager.GetString("AnalyzerTitleMethod", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Type has seters.
        /// </summary>
        internal static string AnalyzerTitleProperty {
            get {
                return ResourceManager.GetString("AnalyzerTitleProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Immutable Type has void methods.
        /// </summary>
        internal static string AnalyzerTitleVoidMethod {
            get {
                return ResourceManager.GetString("AnalyzerTitleVoidMethod", resourceCulture);
            }
        }
    }
}
