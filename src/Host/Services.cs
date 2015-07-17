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
    ///  实例使用Single，共享一个
    ///  并发使用Mutiple, 支持多线程访问(一定要加锁)
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, ReleaseServiceInstanceOnTransactionComplete = false)]
    public partial class Services : IServices
    {

        #region Field
        public static readonly string SendMessageType = ConfigurationManager.ConnectionStrings["SendMessageType"].ToString();
        private static readonly object InstObj = new object();//单一实例
        private static readonly object AgentInstObj = new object();//Agent单一实例

        //public static List<ICallBackServices> RegList = null;

        //client
        public static Dictionary<string, ICallBackServices> DicHost = null; //记录机器名称
        public static Dictionary<string, ICallBackServices> DicHostSess = null;//记录Sessionid

        //agent
        public static Dictionary<string, ICallBackServices> AgentDicHost = null; //记录机器名称
        public static Dictionary<string, ICallBackServices> AgentDicHostSess = null;//记录Sessionid 
        #endregion

        #region Ctor
        public Services()
        {
            //client
            //RegList = new List<ICallBackServices>();
            DicHost = new Dictionary<string, ICallBackServices>();
            DicHostSess = new Dictionary<string, ICallBackServices>();

            //agent
            AgentDicHost = new Dictionary<string, ICallBackServices>();
            AgentDicHostSess = new Dictionary<string, ICallBackServices>();
        }
        #endregion

        #region SendTransactionTest1
        [OperationBehavior(TransactionScopeRequired = true)]
        public int TransactionTest1(string agentNo, string msg)
        {
            try
            {
                if (agentNo == "-1")
                {
                    throw new FaultException("[Service] 出现异常TransactionTest1");
                }

                var agentQuery = from a in AgentDicHostSess
                                 where a.Key == agentNo
                                 select a;

                var agent = agentQuery.FirstOrDefault();
                if (agent.Key != null)
                {
                    agent.Value.SendTransactionTest1(msg);
                }

                return 1;

            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region TransactionTest2
        [OperationBehavior(TransactionScopeRequired = true)]
        public int TransactionTest2(string agentNo, string msg)
        {
            try
            {
                if (agentNo == "-1")
                {
                    throw new FaultException("[Service] 出现异常TransactionTest3");
                }

                var agentQuery = from a in AgentDicHostSess
                                 where a.Key == agentNo
                                 select a;

                var agent = agentQuery.FirstOrDefault();
                if (agent.Key != null)
                {
                    agent.Value.SendTransactionTest2(msg);
                }
                icount++;

                return 1;

            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
        #endregion

        #region TransactionTest3
        [OperationBehavior(TransactionScopeRequired = true)]
        public int TransactionTest3(string agentNo, string msg)
        {
            try
            {
                if (agentNo == "-1")
                {
                    throw new FaultException("[Service] 出现异常TransactionTest3");
                }

                string text = string.Format("TransactionTest3:agentNo:{0} {1}", agentNo, msg);
                //string sql = string.Format("insert into test(msg) values('{0}')", text);
                //int iresult = Library.SQLHelper.ExecuteNonQuery(sql);

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
