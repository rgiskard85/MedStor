﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilityWebServices.EncryptDataReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EncryptDataReference.IEncryptData")]
    public interface IEncryptData {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEncryptData/Encrypt", ReplyAction="http://tempuri.org/IEncryptData/EncryptResponse")]
        string Encrypt(string clearText);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEncryptData/Encrypt", ReplyAction="http://tempuri.org/IEncryptData/EncryptResponse")]
        System.Threading.Tasks.Task<string> EncryptAsync(string clearText);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEncryptDataChannel : UtilityWebServices.EncryptDataReference.IEncryptData, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EncryptDataClient : System.ServiceModel.ClientBase<UtilityWebServices.EncryptDataReference.IEncryptData>, UtilityWebServices.EncryptDataReference.IEncryptData {
        
        public EncryptDataClient() {
        }
        
        public EncryptDataClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EncryptDataClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EncryptDataClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EncryptDataClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Encrypt(string clearText) {
            return base.Channel.Encrypt(clearText);
        }
        
        public System.Threading.Tasks.Task<string> EncryptAsync(string clearText) {
            return base.Channel.EncryptAsync(clearText);
        }
    }
}
