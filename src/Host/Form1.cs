using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;

namespace Host
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static readonly object InstObj = new object();
        private static readonly object AgentInstObj = new object();
        private static bool isval = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            ServiceHost host = new ServiceHost(typeof(Services));
            host.Open();
            this.Text = "wcf starte aucceeded !";

            #region init listbox
            Thread thread = new Thread(new ThreadStart(delegate   ///监听所有客户端连接，并添加到ListBox控件里
            {
                lock (InstObj)//加锁
                {
                    while (true)
                    {

                        if (Services.SendMessageType.ToUpper() == "SESSIONID")
                        {
                            if (Services.DicHostSess != null || Services.DicHostSess.Count > 0)
                            {
                                this.Invoke(new MethodInvoker(delegate { this.listBox1.Items.Clear(); }));
                                foreach (var l in Services.DicHostSess)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        this.listBox1.Items.Add(l.Key);
                                    }));
                                }
                            }
                        }
                        else
                        {
                            if (Services.DicHost != null || Services.DicHost.Count > 0)
                            {
                                this.Invoke(new MethodInvoker(delegate { this.listBox1.Items.Clear(); }));
                                foreach (var l in Services.DicHost)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        this.listBox1.Items.Add(l.Key);
                                    }));
                                }
                            }
                        }
                        Thread.Sleep(1000 * 4);
                    }
                }
            }));
            thread.IsBackground = true;
            thread.Start();
            #endregion

            #region init listbox2
            Thread thread2 = new Thread(new ThreadStart(delegate   ///监听所有客户端连接，并添加到ListBox控件里
            {
                lock (AgentInstObj)//加锁
                {
                    while (true)
                    {

                        if (Services.SendMessageType.ToUpper() == "SESSIONID")
                        {
                            if (Services.AgentDicHostSess != null || Services.AgentDicHostSess.Count > 0)
                            {
                                this.Invoke(new MethodInvoker(delegate { this.listBox2.Items.Clear(); }));
                                foreach (var l in Services.AgentDicHostSess)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        this.listBox2.Items.Add(l.Key);
                                    }));
                                }
                            }
                        }
                        else
                        {
                            if (Services.AgentDicHost != null || Services.AgentDicHost.Count > 0)
                            {
                                this.Invoke(new MethodInvoker(delegate { this.listBox2.Items.Clear(); }));
                                foreach (var l in Services.AgentDicHost)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        this.listBox2.Items.Add(l.Key);
                                    }));
                                }
                            }
                        }
                        Thread.Sleep(1000 * 4);
                    }
                }
            }));
            thread2.IsBackground = true;
            thread2.Start();
            #endregion

        }

        #region Client
        
        #region 推送方式
        private void button1_Click(object sender, EventArgs e)
        {
            if (Services.DicHostSess == null || Services.DicHostSess.Count > 0)
            {
                if (this.listBox1.SelectedItem != null)
                {
                    if (this.listBox1.SelectedItem.ToString() != "")
                    {
                        foreach (var d in Services.DicHostSess)
                        {
                            if (d.Key == this.listBox1.SelectedItem.ToString())
                            {
                                d.Value.SendMessage(string.Format("Client Push {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox1.Text));
                            }
                        }
                    }
                }
                else { MessageBox.Show("请选择要推送给哪台客户端"); return; }
            }
            if (Services.DicHost != null || Services.DicHost.Count > 0)
            {
                if (this.listBox1.SelectedItem != null)
                {
                    if (this.listBox1.SelectedItem.ToString() != "")
                    {
                        foreach (var d in Services.DicHost)
                        {
                            if (d.Key == this.listBox1.SelectedItem.ToString())
                            {
                                d.Value.SendMessage(string.Format("Client Push {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox1.Text));
                            }
                        }
                    }
                }
                else { MessageBox.Show("请选择要推送给哪台客户端"); return; }
            }
        }
        #endregion

        #region 广播方式
        private void button2_Click(object sender, EventArgs e)
        {
            if (Services.SendMessageType.ToUpper() == "SESSIONID")//类型
            {
                foreach (var d in Services.DicHostSess)
                {
                    d.Value.SendMessage(string.Format("Client Broadcast {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox1.Text));
                }
            }
            else
            {
                foreach (var d in Services.DicHost)
                {
                    d.Value.SendMessage(string.Format("Client Broadcast {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox1.Text));
                }
            }
        }
        #endregion

        #endregion


        #region 代理
        
        #region 推送模式
        private void button3_Click(object sender, EventArgs e)
        {
            if (Services.AgentDicHostSess == null || Services.AgentDicHostSess.Count > 0)
            {
                if (this.listBox2.SelectedItem != null)
                {
                    if (this.listBox2.SelectedItem.ToString() != "")
                    {
                        foreach (var d in Services.AgentDicHostSess)
                        {
                            if (d.Key == this.listBox2.SelectedItem.ToString())
                            {
                                d.Value.SendMessage(string.Format("Client Push {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox2.Text));
                            }
                        }
                    }
                }
                else { MessageBox.Show("请选择要推送给哪台代理"); return; }
            }
            if (Services.AgentDicHost != null || Services.AgentDicHost.Count > 0)
            {
                if (this.listBox2.SelectedItem != null)
                {
                    if (this.listBox2.SelectedItem.ToString() != "")
                    {
                        foreach (var d in Services.AgentDicHost)
                        {
                            if (d.Key == this.listBox2.SelectedItem.ToString())
                            {
                                d.Value.SendMessage(string.Format("Client Push {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox2.Text));
                            }
                        }
                    }
                }
                else { MessageBox.Show("请选择要推送给哪台代理"); return; }
            }
        }
        #endregion

        #region 广播模式
        private void button4_Click(object sender, EventArgs e)
        {
            if (Services.SendMessageType.ToUpper() == "SESSIONID")//类型
            {
                foreach (var d in Services.AgentDicHostSess)
                {
                    d.Value.SendMessage(string.Format("Agent Broadcast {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox2.Text));
                }
            }
            else
            {
                foreach (var d in Services.AgentDicHost)
                {
                    d.Value.SendMessage(string.Format("Agent Broadcast {0:HH:mm:ss} message {1}", DateTime.Now, this.textBox2.Text));
                }
            }
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("icount:{0}", Services.icount);
        }

        #endregion

    }
}