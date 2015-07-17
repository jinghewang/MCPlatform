using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Host
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAgentServices
    {
        
        /// <summary>
        /// 注册代理端信息
        /// </summary>
        [OperationContract(IsOneWay = false)]
        void RegisterAgent();

        /// <summary>
        /// 注册代理端信息
        /// </summary>
        [OperationContract(IsOneWay = false)]
        void RegisterAgentByAgentNo(string agentNo);

        /// <summary>
        /// 启动任务
        /// </summary>
        [OperationContract(IsOneWay = false)]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int StartTask(string agentNo, string taskNo, string param);

        /// <summary>
        /// 取消任务
        /// </summary>
        [OperationContract(IsOneWay = false)]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int StopTask(string agentNo, string taskNo, string param);

        /// <summary>
        /// 数据刷新消息
        /// </summary>
        /// <param name="eventNo"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = false)]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int DataFreshMessage(string eventNo, string key);

        
    }
}
