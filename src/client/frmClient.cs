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
using System.Collections;
using System.Threading;
using Library;

namespace client
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
        }

        ServiceReference1.ServicesClient client = null;
        public static ArrayList msglist = new ArrayList();
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            tbClientNo.Text = string.Format("C{0}", rand.Next(1, 10));

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
                string msg = string.Format("[Client] {0}", Message);
                Console.WriteLine(msg);
                //lock (msglist.SyncRoot)
                //{
                msglist.Add(msg);
                //}

                return 1;
            }


            [OperationBehavior(TransactionScopeRequired = true)]
            public int ChangeAgentMode(string agentNo, int mode)
            {
                throw new NotImplementedException();
            }


            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendDataFreshMessage(string eventNo, string key)
            {
                string msg = string.Format("[Client] 数据更新通知 SendDataFreshMessage{0} {1}", eventNo, key);
                Console.WriteLine(msg);
                //lock (msglist.SyncRoot)
                //{
                msglist.Add(msg);
                //}

                return 1;
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
                throw new NotImplementedException();
            }

            #region 测试

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendTransactionTest1(string msg)
            {
                throw new NotImplementedException();
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendTransactionTest2(string msg)
            {
                throw new NotImplementedException();
            }
            #endregion

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendStartTask(string taskNo, string param)
            {
                throw new NotImplementedException();
            }

            [OperationBehavior(TransactionScopeRequired = true)]
            public int SendStopTask(string taskNo, string param)
            {
                throw new NotImplementedException();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //注册到服务端
            try
            {
                msglist.Add("create object...");
                CallBack back = new CallBack();
                InstanceContext context = new InstanceContext(back);
                client = new ServiceReference1.ServicesClient(context);
                msglist.Add("regist.....");
                client.RegisterByClientNo(tbClientNo.Text);
                msglist.Add("aucceeded");

                //client.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //Console.ReadKey();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                client.Close();

                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //主要模式
            try
            {
                int result = client.ChangeAgentMode(tbAgentID.Text.Trim(), 1);
                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            //被动模式
            try
            {
                int result = client.ChangeAgentMode(tbAgentID.Text.Trim(), 0);
                UIHelper.ShowMsg("操作成功");
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }

        }



        private void button4_Click(object sender, EventArgs e)
        {
            //Commit
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    int result = client.TransactionTest1(tbAgentID.Text.Trim(), "TransactionTest1");

                    int result2 = client.TransactionTest2(tbAgentID.Text.Trim(), "TransactionTest2");

                    int result3 = client.TransactionTest3(tbAgentID.Text.Trim(), "TransactionTest3");

                    ts.Complete();

                    UIHelper.ShowMsg("操作成功");
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Rollback
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    int result = client.TransactionTest1(tbAgentID.Text.Trim(), "TransactionTest1");

                    int result2 = client.TransactionTest2(tbAgentID.Text.Trim(), "TransactionTest2");

                    int result3 = client.TransactionTest3(tbAgentID.Text.Trim(), "TransactionTest3");

                    //ts.Complete();
                }

                UIHelper.ShowMsg("未提交事务");

            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //执行过程中代理端异常
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    int result = client.TransactionTest1(tbAgentID.Text.Trim(), "TransactionTest1");

                    int result2 = client.TransactionTest2(tbAgentID.Text.Trim(), "-1");

                    int result3 = client.TransactionTest3(tbAgentID.Text.Trim(), "TransactionTest3");

                    ts.Complete();
                }

                UIHelper.ShowMsg("提交事务");

            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //执行过程中服务端异常
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    int result = client.TransactionTest1(tbAgentID.Text.Trim(), "TransactionTest1");

                    int result2 = client.TransactionTest2(tbAgentID.Text.Trim(), "TransactionTest2");

                    int result3 = client.TransactionTest3("-1", "TransactionTest3");

                    ts.Complete();
                }

                UIHelper.ShowMsg("提交事务");

            }
            catch (Exception ex)
            {
                UIHelper.ShowError(ex.Message);
            }
        }

        private void frmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                client.Close();
            }
            catch
            {
            }
        }




        private void button7_Click(object sender, EventArgs e)
        {
            //向指定代理推送
            try
            {
                int iResult = client.BroadToAgent(tbAgentID.Text.Trim(), tbMsg.Text);
                UIHelper.ShowMsg("操作成功 iresult:" + iResult);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError("操作失败，原因：" + ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //向所有代理端广播
            try
            {
                int iResult = client.BroadToAgent(string.Empty, tbMsg.Text);
                UIHelper.ShowMsg("操作成功 iresult:" + iResult);
            }
            catch (Exception ex)
            {
                UIHelper.ShowError("操作失败，原因：" + ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //检测代理
            try
            {
                int iResult = client.CheckAgent(tbAgentID.Text.Trim());
                UIHelper.ShowMsg("操作结果：iResult:" + (iResult > 0));
            }
            catch (Exception ex)
            {
                UIHelper.ShowError("操作失败，原因：" + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //清空
            msglist.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //开启任务
            try
            {
                using (var tx = new TransactionScope())
                {
                    int iResult = client.StartTask(this.tbAgentID.Text, tbTaskNo.Text, tbParam.Text);
                    UIHelper.ShowMsg("操作结果：iResult:" + (iResult > 0));
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowError("操作失败，原因：" + ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //停止任务
            try
            {
                using (var tx = new TransactionScope())
                {
                    int iResult = client.StopTask(this.tbAgentID.Text, tbTaskNo.Text, tbParam.Text);
                    UIHelper.ShowMsg("操作结果：iResult:" + (iResult > 0));
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowError("操作失败，原因：" + ex.Message);
            }
        }









    }
}
