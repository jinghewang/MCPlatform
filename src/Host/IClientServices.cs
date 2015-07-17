using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Host
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IClientServices
    {
        /// <summary>
        /// 注册客户端信息
        /// </summary>
        [OperationContract(IsOneWay = false)]
        void Register();

        [OperationContract(IsOneWay = false)]
        void RegisterByClientNo(string clientNo);

        [OperationContract(IsOneWay = false)]
        int BroadToAgent(string agentNo, string msg);

        [OperationContract(IsOneWay = false)]
        int CheckAgent(string agentNo);

        [OperationContract(IsOneWay = false)]
        int ChangeAgentMode(string agentNo, int mode);

    }
}
