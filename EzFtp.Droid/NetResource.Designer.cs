﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EzFtp.Droid {
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
    internal class NetResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NetResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EzFtp.Droid.NetResource", typeof(NetResource).Assembly);
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
        ///   Looks up a localized string similar to Phone.
        /// </summary>
        internal static string MyPhone {
            get {
                return ResourceManager.GetString("MyPhone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Local.
        /// </summary>
        internal static string PhoneStroage {
            get {
                return ResourceManager.GetString("PhoneStroage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SD Card.
        /// </summary>
        internal static string SDCardStroage {
            get {
                return ResourceManager.GetString("SDCardStroage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FTP Server Is Running.
        /// </summary>
        internal static string ServerRunningTitle {
            get {
                return ResourceManager.GetString("ServerRunningTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Stop Ftp Server.
        /// </summary>
        internal static string StopFtpServer {
            get {
                return ResourceManager.GetString("StopFtpServer", resourceCulture);
            }
        }
    }
}
