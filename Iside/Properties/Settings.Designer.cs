﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Iside.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0, 0")]
        public global::System.Drawing.Point FolderLocation {
            get {
                return ((global::System.Drawing.Point)(this["FolderLocation"]));
            }
            set {
                this["FolderLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MD5")]
        public global::LLCryptoLib.Hash.AvailableHash DefaultHash {
            get {
                return ((global::LLCryptoLib.Hash.AvailableHash)(this["DefaultHash"]));
            }
            set {
                this["DefaultHash"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SHA1")]
        public global::LLCryptoLib.Hash.AvailableHash AlternativeHash {
            get {
                return ((global::LLCryptoLib.Hash.AvailableHash)(this["AlternativeHash"]));
            }
            set {
                this["AlternativeHash"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MD5")]
        public global::LLCryptoLib.Hash.AvailableHash PrimaryHash {
            get {
                return ((global::LLCryptoLib.Hash.AvailableHash)(this["PrimaryHash"]));
            }
            set {
                this["PrimaryHash"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("UNIX")]
        public global::LLCryptoLib.Utils.HexEnum HashStyle {
            get {
                return ((global::LLCryptoLib.Utils.HexEnum)(this["HashStyle"]));
            }
            set {
                this["HashStyle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color LeftColor {
            get {
                return ((global::System.Drawing.Color)(this["LeftColor"]));
            }
            set {
                this["LeftColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color RightColor {
            get {
                return ((global::System.Drawing.Color)(this["RightColor"]));
            }
            set {
                this["RightColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-1")]
        public double IsideWinLeft {
            get {
                return ((double)(this["IsideWinLeft"]));
            }
            set {
                this["IsideWinLeft"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-1")]
        public double IsideWinTop {
            get {
                return ((double)(this["IsideWinTop"]));
            }
            set {
                this["IsideWinTop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DOUBLE")]
        public global::IsideLogic.FormWidth SizeForm {
            get {
                return ((global::IsideLogic.FormWidth)(this["SizeForm"]));
            }
            set {
                this["SizeForm"] = value;
            }
        }
    }
}
