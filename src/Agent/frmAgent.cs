using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;

using System.ServiceModel;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using Library;

namespace Agent
{
    public partial class frmAgent : Form
    {
        public frmAgent()
        {
            InitializeComponent();
        }

        ServiceReference1.ServicesClient client = null;
        public static int num1 = 0;
        public static int num2 = 0;
        public static ArrayList msglist = new ArrayList();
        public static List<Task> taskList = new List<Task>();

        /// <summary>
        /// 1主动 0被动（默认）
        /// </summary>
        public static int agentMode = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            tbAgentNo.Text = string.Format("A{0}", rand.Next(1, 10));

            #region 任务处理
            //主动模式运行－－自动加载分配给此代理任务
            string taskNo = tbTaskNo.Text;//通过配置取得
            LoadTask(taskNo);
            #endregion

            #region MessageList
            Thread thread = new Thread(new ThreadStart(delegate()
            {
                lock (msglist.SyncRoot)
                {
                    while (true)
                    {
                        this.Invoke(new Action(delegate()
                        {
                            listBox1.Items.Clear();
                        }));

                        this.Invoke(new MethodInvoker(delegate()
                        {
                            listBox1.Items.AddRange(msglist.ToArray());
                        }));

                        Thread.Sleep(1000 * 3);
                    }
                }

            }));
            thread.IsBackground = true;
            thread.Start();
            #endregion
        }



        private class CallBack : ServiceReference1.IServicesCallback
        {

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendMessage(string Message)
            {
                string msg = string.Format("[Agent] {0}", Message);
                Console.WriteLine(msg);

                msglist.Add(msg);

                return 1;
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendDataFreshMessage(string eventNo, string key)
            {
                throw new NotImplementedException();
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendAgentControl(string msg)
            {
                throw new NotImplementedException();
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendAgentCollect(string msg)
            {
                throw new NotImplementedException();
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendChangeAgentMode(int mode)
            {
                agentMode = mode;
                return 1;
            }

            #region 测试
            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendTransactionTest1(string msg)
            {
                Console.WriteLine(msg);

                if (msg == "-1")
                {
                    string exstr = string.Format("[Agent] SendTransactionTest1:{0} 出现异常", msg);
                    msglist.Add(exstr);
                    throw new FaultException(exstr);
                }

                string text = string.Format("[Agent] SendTransactionTest1:{0}", msg);
                msglist.Add(text);

                //string sql = string.Format("insert into test(msg) values('{0}')", text);
                //int iresult = Library.SQLHelper.ExecuteNonQuery(sql);

                return 1;
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendTransactionTest2(string msg)
            {
                Console.WriteLine(msg);
                if (msg == "-1")
                {
                    string exstr = string.Format("[Agent] SendTransactionTest2:{0} 出现异常", msg);
                    msglist.Add(exstr);
                    throw new FaultException(exstr);
                }

                string text = string.Format("[Agent] SendTransactionTest2:{0}", msg);
                msglist.Add(text);

                //string sql = string.Format("insert into test(msg) values('{0}')", text);
                //int iresult = Library.SQLHelper.ExecuteNonQuery(sql);

                return 1;
            }
            #endregion

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendStartTask(string taskNo, string param)
            {
                //进行实现
                TaskManager taskManager = new TaskManager();
                if (agentMode == 0)
                {
                    //被动模式－直接执行
                    msglist.Add("被动模式－直接执行 LoadAndExecuteTask");
                    int iResult = taskManager.LoadAndExecuteTask(taskNo);
                    return iResult;
                }
                else
                {
                    //主动模式－自动加载分配给此代理任务
                    return 1;
                }
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendStopTask(string taskNo, string param)
            {
                //进行实现
                throw new NotImplementedException();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //注册到服务端
            try
            {

                msglist.Add("create agent object...");
                CallBack back = new CallBack();
                InstanceContext context = new InstanceContext(back);
                client = new ServiceReference1.ServicesClient(context);
                msglist.Add("regist agent.....");
                client.RegisterAgentByAgentNo(tbAgentNo.Text);
                msglist.Add("aucceeded");

                //UIHelper.ShowMsg("操作成功");

            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //关闭
                client.Close();
                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            //using (TransactionScope ts = new TransactionScope(TranscationScopeOption.RequireNew))
            //{
            //    client.DoSomething();

            //    ts.Complete();
            //}
            //client.Close();

        }


        private void button6_Click(object sender, EventArgs e)
        {

        }

        TaskManager taskManager = new TaskManager();
        private void timer1_Tick(object sender, EventArgs e)
        {
            labMode.Text = agentMode == 1 ? "主动" : "被动";
            if (agentMode == 1)
            {
                //主动模式运行
                msglist.Add("---------主动模式执行------------");

                for (int i = 0; i < taskList.Count; i++)
                {
                    Task task = taskList[i];
                    int iResult = taskManager.ExecuteTask(task);
                    msglist.Add("ExecuteTask..." + task.Name);
                }

                //发送数据更新通知
                msglist.Add("发送数据更新通知...");
                //int iResult = client.DataFreshMessage("e10", "10");

                msglist.Add("---------主动模式执行 完成---------");

                //Thread.Sleep(1000 * 20);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //发送数据更新通知

            try
            {
                int iResult = client.DataFreshMessage("e10", "10");
                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //清空
            msglist.Clear();
        }



       

        private void button5_Click(object sender, EventArgs e)
        {
            //加载任务
            try
            {
                LoadTask(tbTaskNo.Text);
                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //取消任务
            try
            {
                RemoveTask(tbTaskNo.Text);
                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //取消全部任务
            try
            {
                RemoveAllTask(tbTaskNo.Text);
                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }


        private void LoadTask(string taskNo)
        {
            taskList.Add(taskManager.LoadTask(taskNo));
        }

        private void RemoveAllTask(string taskNo)
        {
            try
            {
                taskList.Clear();
            }
            catch (Exception ex)
            {

            }
        }

        private void RemoveTask(string taskNo)
        {
            try
            {
                taskList.Remove(taskList.First(t => t.Id == taskNo));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
