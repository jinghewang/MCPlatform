using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Host
{

    public interface ICallBackServices
    {
        /// <summary>
        /// 服务像客户端发送信息(同步)
        /// </summary>
        /// <param name="Message"></param>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendMessage(string Message);

        /// <summary>
        /// 服务像客户端发送信息(异步)
        /// </summary>
        /// <param name="Message"></param>
        //[OperationContract(IsOneWay = true)]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        //int SendMessageAsyn(string Message);

        
        /// <summary>
        /// 发送代理操控指令
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendAgentControl(string msg);

        /// <summary>
        /// 发送代理采集指令
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendAgentCollect(string msg);

        /// <summary>
        /// 更改代理运行模式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendChangeAgentMode(int mode);

        /// <summary>
        /// 启动任务
        /// </summary>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendStartTask(string taskNo, string param);

        /// <summary>
        /// 取消任务
        /// </summary>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendStopTask(string taskNo, string param);

        /// <summary>
        /// 数据刷新通知
        /// </summary>
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendDataFreshMessage(string eventNo, string key);

        
        #region 测试
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendTransactionTest1(string msg);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int SendTransactionTest2(string msg); 
        #endregion


    }
}
