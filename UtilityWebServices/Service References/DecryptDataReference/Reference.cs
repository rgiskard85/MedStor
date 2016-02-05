﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilityWebServices.DecryptDataReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DecryptDataReference.IDecryptData")]
    public interface IDecryptData {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDecryptData/Decrypt", ReplyAction="http://tempuri.org/IDecryptData/DecryptResponse")]
        string Decrypt(string cipherText);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDecryptData/Decrypt", ReplyAction="http://tempuri.org/IDecryptData/DecryptResponse")]
        System.Threading.Tasks.Task<string> DecryptAsync(string cipherText);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDecryptDataChannel : UtilityWebServices.DecryptDataReference.IDecryptData, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DecryptDataClient : System.ServiceModel.ClientBase<UtilityWebServices.DecryptDataReference.IDecryptData>, UtilityWebServices.DecryptDataReference.IDecryptData {
        
        public DecryptDataClient() {
        }
        
        public DecryptDataClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DecryptDataClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DecryptDataClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DecryptDataClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Decrypt(string cipherText) {
            return base.Channel.Decrypt(cipherText);
        }
        
        public System.Threading.Tasks.Task<string> DecryptAsync(string cipherText) {
            return base.Channel.DecryptAsync(cipherText);
        }
    }
}
