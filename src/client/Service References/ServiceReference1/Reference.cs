﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace client.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServices", CallbackContract=typeof(client.ServiceReference1.IServicesCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IServices {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentServices/RegisterAgent", ReplyAction="http://tempuri.org/IAgentServices/RegisterAgentResponse")]
        void RegisterAgent();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentServices/RegisterAgentByAgentNo", ReplyAction="http://tempuri.org/IAgentServices/RegisterAgentByAgentNoResponse")]
        void RegisterAgentByAgentNo(string agentNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentServices/StartTask", ReplyAction="http://tempuri.org/IAgentServices/StartTaskResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Mandatory)]
        int StartTask(string agentNo, string taskNo, string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentServices/StopTask", ReplyAction="http://tempuri.org/IAgentServices/StopTaskResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int StopTask(string agentNo, string taskNo, string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentServices/DataFreshMessage", ReplyAction="http://tempuri.org/IAgentServices/DataFreshMessageResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int DataFreshMessage(string eventNo, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientServices/Register", ReplyAction="http://tempuri.org/IClientServices/RegisterResponse")]
        void Register();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientServices/RegisterByClientNo", ReplyAction="http://tempuri.org/IClientServices/RegisterByClientNoResponse")]
        void RegisterByClientNo(string clientNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientServices/BroadToAgent", ReplyAction="http://tempuri.org/IClientServices/BroadToAgentResponse")]
        int BroadToAgent(string agentNo, string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientServices/CheckAgent", ReplyAction="http://tempuri.org/IClientServices/CheckAgentResponse")]
        int CheckAgent(string agentNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IClientServices/ChangeAgentMode", ReplyAction="http://tempuri.org/IClientServices/ChangeAgentModeResponse")]
        int ChangeAgentMode(string agentNo, int mode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/TransactionTest1", ReplyAction="http://tempuri.org/IServices/TransactionTest1Response")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Mandatory)]
        int TransactionTest1(string agentNo, string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/TransactionTest2", ReplyAction="http://tempuri.org/IServices/TransactionTest2Response")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int TransactionTest2(string agentNo, string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/TransactionTest3", ReplyAction="http://tempuri.org/IServices/TransactionTest3Response")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int TransactionTest3(string agentNo, string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicesCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendMessage", ReplyAction="http://tempuri.org/IServices/SendMessageResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendMessage(string Message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendAgentControl", ReplyAction="http://tempuri.org/IServices/SendAgentControlResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendAgentControl(string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendAgentCollect", ReplyAction="http://tempuri.org/IServices/SendAgentCollectResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendAgentCollect(string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendChangeAgentMode", ReplyAction="http://tempuri.org/IServices/SendChangeAgentModeResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendChangeAgentMode(int mode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendStartTask", ReplyAction="http://tempuri.org/IServices/SendStartTaskResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendStartTask(string taskNo, string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendStopTask", ReplyAction="http://tempuri.org/IServices/SendStopTaskResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendStopTask(string taskNo, string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendDataFreshMessage", ReplyAction="http://tempuri.org/IServices/SendDataFreshMessageResponse")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendDataFreshMessage(string eventNo, string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendTransactionTest1", ReplyAction="http://tempuri.org/IServices/SendTransactionTest1Response")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendTransactionTest1(string msg);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServices/SendTransactionTest2", ReplyAction="http://tempuri.org/IServices/SendTransactionTest2Response")]
        [System.ServiceModel.TransactionFlowAttribute(System.ServiceModel.TransactionFlowOption.Allowed)]
        int SendTransactionTest2(string msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicesChannel : client.ServiceReference1.IServices, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicesClient : System.ServiceModel.DuplexClientBase<client.ServiceReference1.IServices>, client.ServiceReference1.IServices {
        
        public ServicesClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServicesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServicesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServicesClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServicesClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void RegisterAgent() {
            base.Channel.RegisterAgent();
        }
        
        public void RegisterAgentByAgentNo(string agentNo) {
            base.Channel.RegisterAgentByAgentNo(agentNo);
        }
        
        public int StartTask(string agentNo, string taskNo, string param) {
            return base.Channel.StartTask(agentNo, taskNo, param);
        }
        
        public int StopTask(string agentNo, string taskNo, string param) {
            return base.Channel.StopTask(agentNo, taskNo, param);
        }
        
        public int DataFreshMessage(string eventNo, string key) {
            return base.Channel.DataFreshMessage(eventNo, key);
        }
        
        public void Register() {
            base.Channel.Register();
        }
        
        public void RegisterByClientNo(string clientNo) {
            base.Channel.RegisterByClientNo(clientNo);
        }
        
        public int BroadToAgent(string agentNo, string msg) {
            return base.Channel.BroadToAgent(agentNo, msg);
        }
        
        public int CheckAgent(string agentNo) {
            return base.Channel.CheckAgent(agentNo);
        }
        
        public int ChangeAgentMode(string agentNo, int mode) {
            return base.Channel.ChangeAgentMode(agentNo, mode);
        }
        
        public int TransactionTest1(string agentNo, string msg) {
            return base.Channel.TransactionTest1(agentNo, msg);
        }
        
        public int TransactionTest2(string agentNo, string msg) {
            return base.Channel.TransactionTest2(agentNo, msg);
        }
        
        public int TransactionTest3(string agentNo, string msg) {
            return base.Channel.TransactionTest3(agentNo, msg);
        }
    }
}
