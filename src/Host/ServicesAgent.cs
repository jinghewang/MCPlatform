using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Host
{
    /// <summary>
    ///  Service Agent 部分
    /// </summary>
    public partial class Services : IServices
    {

        #region RegisterAgent
        /// <summary>
        /// 注册agent
        /// </summary>
        public void RegisterAgent()
        {
            ICallBackServices client = OperationContext.Current.GetCallbackChannel<ICallBackServices>();
            string sessionid = OperationContext.Current.SessionId;//获取当前机器Sessionid--------------------------如果多个客户端在同一台机器，就使用此信息。
            string ClientHostName = OperationContext.Current.Channel.RemoteAddress.Uri.Host;//获取当前机器名称-----多个客户端不在同一台机器上，就使用此信息。
            OperationContext.Current.Channel.Closing += new EventHandler(AgentChannel_Closing);//注册客户端关闭触发事件
            if (SendMessageType.ToUpper() == "SESSIONID")
            {
                AgentDicHostSess.Add(sessionid, client);//添加
            }
            else
            {
                AgentDicHost.Add(ClientHostName, client); //添加
            }
            //RegList.Add(client);//添加
        }

        /// <summary>
        /// 注册agent
        /// </summary>
        public void RegisterAgentByAgentNo(string agentNo)
        {
            ICallBackServices client = OperationContext.Current.GetCallbackChannel<ICallBackServices>();
            string sessionid = OperationContext.Current.SessionId;//获取当前机器Sessionid--------------------------如果多个客户端在同一台机器，就使用此信息。
            string ClientHostName = OperationContext.Current.Channel.RemoteAddress.Uri.Host;//获取当前机器名称-----多个客户端不在同一台机器上，就使用此信息。
            OperationContext.Current.Channel.Closing += new EventHandler(AgentChannel_Closing);//注册客户端关闭触发事件
            if (SendMessageType.ToUpper() == "SESSIONID")
            {
                AgentDicHostSess.Add(agentNo, client);//添加
            }
            else
            {
                //AgentDicHost.Add(ClientHostName, client); //添加
            }
            //RegList.Add(client);//添加
        }
        #endregion

        #region AgentChannel_Closing
        private void AgentChannel_Closing(object sender, EventArgs e)
        {
            lock (AgentInstObj)//加锁，处理并发
            {
                //if (RegList != null && RegList.Count > 0)
                //    RegList.Remove((ICallBackServices)sender); 
                if (SendMessageType.ToUpper() == "SESSIONID")
                {
                    if (AgentDicHostSess != null && AgentDicHostSess.Count > 0)
                    {
                        foreach (var d in AgentDicHostSess)
                        {
                            if (d.Value == (ICallBackServices)sender)//删除此关闭的客户端信息
                            {
                                AgentDicHostSess.Remove(d.Key);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (AgentDicHost != null && AgentDicHost.Count > 0) //同上
                    {
                        foreach (var d in AgentDicHost)
                        {
                            if (d.Value == (ICallBackServices)sender)
                            {
                                AgentDicHost.Remove(d.Key);
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region StartTask
        [OperationBehavior(TransactionScopeRequired = true)]
        public int StartTask(string agentNo, string taskNo, string param)
        {
            try
            {
                var agentQuery = from a in AgentDicHostSess
                                 //where a.Key == agentNo
                                 select a;

                var agent = agentQuery.FirstOrDefault();
                if (agent.Key != null)
                {
                    string text = string.Format("[Called from Host] StopOperate:agentNo:{0}", agentNo);
                    agent.Value.SendStartTask(taskNo,param);
                }

                return 1;

            }
            catch (Exception ex)
            {
                return 0;
                //throw ex;
            }
        }
        #endregion


        #region StopTask
        public static int icount = 0;
        [OperationBehavior(TransactionScopeRequired = true)]
        public int StopTask(string agentNo, string taskNo,string param)
        {
            try
            {
                var agentQuery = from a in AgentDicHostSess
                                 //where a.Key == agentNo
                                 select a;

                var agent = agentQuery.FirstOrDefault();
                if (agent.Key != null)
                {
                    string text = string.Format("[Called from Host] StopOperate:agentNo:{0}", agentNo);
                    int iresult = agent.Value.SendStopTask(taskNo,param);
                }
                icount++;

                //throw new Exception("余额不足!2233");

                return 1;

            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region DataFreshMessage
        [OperationBehavior(TransactionScopeRequired = true)]
        public int DataFreshMessage(string eventNo, string key)
        {
            try
            {
                var clientQuery = from a in DicHostSess
                                  //where a.Key == agentNo
                                  select a;

                foreach (var client in clientQuery)
                {
                    if (client.Key != null)
                    {
                        string text = string.Format("[Called from Host] DataFreshMessage:eventNo:{0} key {1}", eventNo, key);
                        int iResult = client.Value.SendDataFreshMessage(eventNo, key);
                    }
                }

                return 1;

            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

    }
}
