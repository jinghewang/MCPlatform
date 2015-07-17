#region << 版 本 注 释 >>
/****************************************************
* 文 件 名：IServices.cs
* Copyright(c) 2011-2012 JiangGuoLiang
* CLR 版本: 4.0.30319.235 
* 创 建 人：Server126
* 创建日期：2011-8-10 15:00:55
*******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Host
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(ICallBackServices))]
    public interface IServices : IAgentServices,IClientServices
    {

        #region 测试
        [OperationContract(IsOneWay = false)]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        int TransactionTest1(string agentNo, string msg);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int TransactionTest2(string agentNo, string msg);


        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        int TransactionTest3(string agentNo, string msg);
        #endregion


    } //End Host

    
} // End IServices
