﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UtilityWebServices.GetDBConnectionReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GetDBConnectionReference.IGetDBConnection")]
    public interface IGetDBConnection {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetDBConnection/GetDBConnectionString", ReplyAction="http://tempuri.org/IGetDBConnection/GetDBConnectionStringResponse")]
        string GetDBConnectionString();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetDBConnection/GetDBConnectionString", ReplyAction="http://tempuri.org/IGetDBConnection/GetDBConnectionStringResponse")]
        System.Threading.Tasks.Task<string> GetDBConnectionStringAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGetDBConnectionChannel : UtilityWebServices.GetDBConnectionReference.IGetDBConnection, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetDBConnectionClient : System.ServiceModel.ClientBase<UtilityWebServices.GetDBConnectionReference.IGetDBConnection>, UtilityWebServices.GetDBConnectionReference.IGetDBConnection {
        
        public GetDBConnectionClient() {
        }
        
        public GetDBConnectionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GetDBConnectionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GetDBConnectionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GetDBConnectionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetDBConnectionString() {
            return base.Channel.GetDBConnectionString();
        }
        
        public System.Threading.Tasks.Task<string> GetDBConnectionStringAsync() {
            return base.Channel.GetDBConnectionStringAsync();
        }
    }
}