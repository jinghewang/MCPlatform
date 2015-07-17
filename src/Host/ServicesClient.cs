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
    ///  Service Client 部分
    /// </summary>
    public partial class Services : IServices
    {
        #region Register
        /// <summary>
        /// 注册client
        /// </summary>
        public void Register()
        {
            ICallBackServices client = OperationContext.Current.GetCallbackChannel<ICallBackServices>();
            string sessionid = OperationContext.Current.SessionId;//获取当前机器Sessionid--------------------------如果多个客户端在同一台机器，就使用此信息。
            string ClientHostName = OperationContext.Current.Channel.RemoteAddress.Uri.Host;//获取当前机器名称-----多个客户端不在同一台机器上，就使用此信息。
            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);//注册客户端关闭触发事件
            if (SendMessageType.ToUpper() == "SESSIONID")
            {
                DicHostSess.Add(sessionid, client);//添加
            }
            else
            {
                DicHost.Add(ClientHostName, client); //添加
            }
            //RegList.Add(client);//添加
        }

        /// <summary>
        /// 注册client
        /// </summary>
        public void RegisterByClientNo(string clientNo)
        {
            ICallBackServices client = OperationContext.Current.GetCallbackChannel<ICallBackServices>();
            string sessionid = OperationContext.Current.SessionId;//获取当前机器Sessionid--------------------------如果多个客户端在同一台机器，就使用此信息。
            string ClientHostName = OperationContext.Current.Channel.RemoteAddress.Uri.Host;//获取当前机器名称-----多个客户端不在同一台机器上，就使用此信息。
            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);//注册客户端关闭触发事件
            if (SendMessageType.ToUpper() == "SESSIONID")
            {
                DicHostSess.Add(clientNo, client);//添加
            }
            else
            {
                //DicHost.Add(ClientHostName, client); //添加
            }
            //RegList.Add(client);//添加
        }
        #endregion

        #region Channel_Closing
        private void Channel_Closing(object sender, EventArgs e)
        {
            lock (InstObj)//加锁，处理并发
            {
                //if (RegList != null && RegList.Count > 0)
                //    RegList.Remove((ICallBackServices)sender); 
                if (SendMessageType.ToUpper() == "SESSIONID")
                {
                    if (DicHostSess != null && DicHostSess.Count > 0)
                    {
                        foreach (var d in DicHostSess)
                        {
                            if (d.Value == (ICallBackServices)sender)//删除此关闭的客户端信息
                            {
                                DicHostSess.Remove(d.Key);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (DicHost != null && DicHost.Count > 0) //同上
                    {
                        foreach (var d in DicHost)
                        {
                            if (d.Value == (ICallBackServices)sender)
                            {
                                DicHost.Remove(d.Key);
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region BroadToAgent
        /// <summary>
        /// 广播到代理端
        /// </summary>
        public int BroadToAgent(string agentNo,string msg)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(agentNo))
                {
                    var agentQuery = from a in AgentDicHostSess
                                     where a.Key == agentNo
                                     select a;

                    foreach (var agent in agentQuery)
                    {
                        if (agent.Key != null)
                        {
                            string text = string.Format("[Called from Client] BroadToAgent:agentNo:{0},msg:{1}", agent.Key,msg);
                            int iresult = agent.Value.SendMessage(text);
                        }
                    }
                }
                else
                {
                    var agentQuery = from a in AgentDicHostSess
                                     //where a.Key == agentNo
                                     select a;

                    foreach (var agent in agentQuery)
                    {
                        if (agent.Key != null)
                        {
                            string text = string.Format("[Called from Client] BroadToAgent:agentNo:{0},msg:{1}", agent.Key, msg);
                            int iresult = agent.Value.SendMessage(text);
                        }
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

        #region CheckAgent
        /// <summary>
        /// 检测代理端
        /// </summary>
        public int CheckAgent(string agentNo)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(agentNo))
                {
                    var agentQuery = from a in AgentDicHostSess
                                     where a.Key == agentNo
                                     select a;
                    return agentQuery.Count() > 0 ? 1 : 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region ChangeAgentMode
        /// <summary>
        /// 更改代理端工作模式（主要 1｜被动 0）
        /// </summary>
        public int ChangeAgentMode(string agentNo, int mode)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(agentNo))
                {
                    var agentQuery = from a in AgentDicHostSess
                                     where a.Key == agentNo
                                     select a;

                    foreach (var agent in agentQuery)
                    {
                        if (agent.Key != null)
                        {
                            string text = string.Format("[Called from Client] ChangeAgentMode:agentNo:{0} mode:{1}", agent.Key, mode);
                            agent.Value.SendMessage(text);
                            int iresult = agent.Value.SendChangeAgentMode(mode);
                        }
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
