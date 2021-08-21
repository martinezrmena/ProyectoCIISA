﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace CIISA.RetailOnLine.Droid.webServiceSROL_totalDownload {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="VendedorEnvioTotalSoap", Namespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/")]
    public partial class VendedorEnvioTotal : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback TotalSendOperationCompleted;
        
        private System.Threading.SendOrPostCallback TotalSend_AutomaticOperationCompleted;
        
        private System.Threading.SendOrPostCallback TotalSendOldOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public VendedorEnvioTotal() {
            this.Url = "http://mobile.crciisa.com/ws-v27042017-srol-prod/VendedorEnvioTotal.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event TotalSendCompletedEventHandler TotalSendCompleted;
        
        /// <remarks/>
        public event TotalSend_AutomaticCompletedEventHandler TotalSend_AutomaticCompleted;
        
        /// <remarks/>
        public event TotalSendOldCompletedEventHandler TotalSendOldCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/TotalSend", RequestNamespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/", ResponseNamespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TotalSend(string pdatosSROL, string ptipoRutero, bool ptomaFisica, SystemCIISA psystemCIISA, bool pwriteDataTables) {
            object[] results = this.Invoke("TotalSend", new object[] {
                        pdatosSROL,
                        ptipoRutero,
                        ptomaFisica,
                        psystemCIISA,
                        pwriteDataTables});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TotalSendAsync(string pdatosSROL, string ptipoRutero, bool ptomaFisica, SystemCIISA psystemCIISA, bool pwriteDataTables) {
            this.TotalSendAsync(pdatosSROL, ptipoRutero, ptomaFisica, psystemCIISA, pwriteDataTables, null);
        }
        
        /// <remarks/>
        public void TotalSendAsync(string pdatosSROL, string ptipoRutero, bool ptomaFisica, SystemCIISA psystemCIISA, bool pwriteDataTables, object userState) {
            if ((this.TotalSendOperationCompleted == null)) {
                this.TotalSendOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTotalSendOperationCompleted);
            }
            this.InvokeAsync("TotalSend", new object[] {
                        pdatosSROL,
                        ptipoRutero,
                        ptomaFisica,
                        psystemCIISA,
                        pwriteDataTables}, this.TotalSendOperationCompleted, userState);
        }
        
        private void OnTotalSendOperationCompleted(object arg) {
            if ((this.TotalSendCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TotalSendCompleted(this, new TotalSendCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/TotalSend_Automatic", RequestNamespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/", ResponseNamespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TotalSend_Automatic(string pdatosSROL, string ptipoRutero, bool ptomaFisica, SystemCIISA psystemCIISA, bool pwriteDataTables) {
            object[] results = this.Invoke("TotalSend_Automatic", new object[] {
                        pdatosSROL,
                        ptipoRutero,
                        ptomaFisica,
                        psystemCIISA,
                        pwriteDataTables});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TotalSend_AutomaticAsync(string pdatosSROL, string ptipoRutero, bool ptomaFisica, SystemCIISA psystemCIISA, bool pwriteDataTables) {
            this.TotalSend_AutomaticAsync(pdatosSROL, ptipoRutero, ptomaFisica, psystemCIISA, pwriteDataTables, null);
        }
        
        /// <remarks/>
        public void TotalSend_AutomaticAsync(string pdatosSROL, string ptipoRutero, bool ptomaFisica, SystemCIISA psystemCIISA, bool pwriteDataTables, object userState) {
            if ((this.TotalSend_AutomaticOperationCompleted == null)) {
                this.TotalSend_AutomaticOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTotalSend_AutomaticOperationCompleted);
            }
            this.InvokeAsync("TotalSend_Automatic", new object[] {
                        pdatosSROL,
                        ptipoRutero,
                        ptomaFisica,
                        psystemCIISA,
                        pwriteDataTables}, this.TotalSend_AutomaticOperationCompleted, userState);
        }
        
        private void OnTotalSend_AutomaticOperationCompleted(object arg) {
            if ((this.TotalSend_AutomaticCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TotalSend_AutomaticCompleted(this, new TotalSend_AutomaticCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/TotalSendOld", RequestNamespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/", ResponseNamespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TotalSendOld(string pdatosSROL, string ptipoRutero, SystemCIISA psystemCIISA) {
            object[] results = this.Invoke("TotalSendOld", new object[] {
                        pdatosSROL,
                        ptipoRutero,
                        psystemCIISA});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TotalSendOldAsync(string pdatosSROL, string ptipoRutero, SystemCIISA psystemCIISA) {
            this.TotalSendOldAsync(pdatosSROL, ptipoRutero, psystemCIISA, null);
        }
        
        /// <remarks/>
        public void TotalSendOldAsync(string pdatosSROL, string ptipoRutero, SystemCIISA psystemCIISA, object userState) {
            if ((this.TotalSendOldOperationCompleted == null)) {
                this.TotalSendOldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTotalSendOldOperationCompleted);
            }
            this.InvokeAsync("TotalSendOld", new object[] {
                        pdatosSROL,
                        ptipoRutero,
                        psystemCIISA}, this.TotalSendOldOperationCompleted, userState);
        }
        
        private void OnTotalSendOldOperationCompleted(object arg) {
            if ((this.TotalSendOldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TotalSendOldCompleted(this, new TotalSendOldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mobile.crciisa.com/WS-SROL-V3CAT-PROD/")]
    public partial class SystemCIISA {
        
        private string _codCompanyField;
        
        private string _codAgentField;
        
        private string _codRuteField;
        
        private string _nameField;
        
        private string _initialsField;
        
        private string _versionField;
        
        /// <remarks/>
        public string _codCompany {
            get {
                return this._codCompanyField;
            }
            set {
                this._codCompanyField = value;
            }
        }
        
        /// <remarks/>
        public string _codAgent {
            get {
                return this._codAgentField;
            }
            set {
                this._codAgentField = value;
            }
        }
        
        /// <remarks/>
        public string _codRute {
            get {
                return this._codRuteField;
            }
            set {
                this._codRuteField = value;
            }
        }
        
        /// <remarks/>
        public string _name {
            get {
                return this._nameField;
            }
            set {
                this._nameField = value;
            }
        }
        
        /// <remarks/>
        public string _initials {
            get {
                return this._initialsField;
            }
            set {
                this._initialsField = value;
            }
        }
        
        /// <remarks/>
        public string _version {
            get {
                return this._versionField;
            }
            set {
                this._versionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void TotalSendCompletedEventHandler(object sender, TotalSendCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TotalSendCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TotalSendCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void TotalSend_AutomaticCompletedEventHandler(object sender, TotalSend_AutomaticCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TotalSend_AutomaticCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TotalSend_AutomaticCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void TotalSendOldCompletedEventHandler(object sender, TotalSendOldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TotalSendOldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TotalSendOldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591