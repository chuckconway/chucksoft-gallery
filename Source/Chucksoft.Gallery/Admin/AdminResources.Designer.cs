﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3031
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chucksoft.Admin {
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
    public class AdminResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AdminResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Chucksoft.Admin.AdminResources", typeof(AdminResources).Assembly);
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
        ///   Looks up a localized string similar to ~/Admin/Default.aspx.
        /// </summary>
        public static string AdminHomepage {
            get {
                return ResourceManager.GetString("AdminHomepage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AdminSession.
        /// </summary>
        public static string AdminSessionCookieName {
            get {
                return ResourceManager.GetString("AdminSessionCookieName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to photogallery.
        /// </summary>
        public static string AuthenticationCookieName {
            get {
                return ResourceManager.GetString("AuthenticationCookieName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Selected galleries have been deleted..
        /// </summary>
        public static string DeletedGalleries {
            get {
                return ResourceManager.GetString("DeletedGalleries", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you wish to delete selected galleries? Photos from the deleted galleries will be moved to the first created gallery that was created..
        /// </summary>
        public static string DeleteGalleryConfirmMessage {
            get {
                return ResourceManager.GetString("DeleteGalleryConfirmMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The selected phtos have been deleted..
        /// </summary>
        public static string DeletePhotos {
            get {
                return ResourceManager.GetString("DeletePhotos", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Username or Password..
        /// </summary>
        public static string LoginInvalidCredentials {
            get {
                return ResourceManager.GetString("LoginInvalidCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Settings have been saved..
        /// </summary>
        public static string SettingsSaved {
            get {
                return ResourceManager.GetString("SettingsSaved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; has successfully been created..
        /// </summary>
        public static string SuccessfulAlbumAdd {
            get {
                return ResourceManager.GetString("SuccessfulAlbumAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; has successfully been added..
        /// </summary>
        public static string SuccessfulGalleryAdd {
            get {
                return ResourceManager.GetString("SuccessfulGalleryAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your photo has been successfully added..
        /// </summary>
        public static string SuccessfulPhotoAdd {
            get {
                return ResourceManager.GetString("SuccessfulPhotoAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} has been added..
        /// </summary>
        public static string SuccessfulUserAdd {
            get {
                return ResourceManager.GetString("SuccessfulUserAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User settings have been saved..
        /// </summary>
        public static string SuccessfulUserUpdate {
            get {
                return ResourceManager.GetString("SuccessfulUserUpdate", resourceCulture);
            }
        }
    }
}