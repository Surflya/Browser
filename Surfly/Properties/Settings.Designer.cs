//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Surfly.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SelectDownloadLocationInEveryDownload {
            get {
                return ((bool)(this["SelectDownloadLocationInEveryDownload"]));
            }
            set {
                this["SelectDownloadLocationInEveryDownload"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DefaultDownloadLocation {
            get {
                return ((string)(this["DefaultDownloadLocation"]));
            }
            set {
                this["DefaultDownloadLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DuckDuckGo (default)")]
        public string DefaultSearchEngine {
            get {
                return ((string)(this["DefaultSearchEngine"]));
            }
            set {
                this["DefaultSearchEngine"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LoadPage")]
        public string AtStart {
            get {
                return ((string)(this["AtStart"]));
            }
            set {
                this["AtStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://duckduckgo.com")]
        public string AtStartPage {
            get {
                return ((string)(this["AtStartPage"]));
            }
            set {
                this["AtStartPage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool BlockTracking {
            get {
                return ((bool)(this["BlockTracking"]));
            }
            set {
                this["BlockTracking"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int BlockedTrackers {
            get {
                return ((int)(this["BlockedTrackers"]));
            }
            set {
                this["BlockedTrackers"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50, 50")]
        public global::System.Drawing.Point LastLocation {
            get {
                return ((global::System.Drawing.Point)(this["LastLocation"]));
            }
            set {
                this["LastLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LastTimeWasMaximized {
            get {
                return ((bool)(this["LastTimeWasMaximized"]));
            }
            set {
                this["LastTimeWasMaximized"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1270, 900")]
        public global::System.Drawing.Size LastSize {
            get {
                return ((global::System.Drawing.Size)(this["LastSize"]));
            }
            set {
                this["LastSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string InternalProfileNameForDownloads {
            get {
                return ((string)(this["InternalProfileNameForDownloads"]));
            }
            set {
                this["InternalProfileNameForDownloads"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://duckcuckgo.com/?q=")]
        public string SearchUrl {
            get {
                return ((string)(this["SearchUrl"]));
            }
            set {
                this["SearchUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.DateTime LastStart {
            get {
                return ((global::System.DateTime)(this["LastStart"]));
            }
            set {
                this["LastStart"] = value;
            }
        }
    }
}
