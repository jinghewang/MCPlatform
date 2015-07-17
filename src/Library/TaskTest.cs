using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class TaskTest
    {

        public string CollectData(string agentNo)
        {
            Console.WriteLine("收集数据 agentNo:" + agentNo);
            return "收集数据 agentNo:" + agentNo;
        }

        public string ComputeData(string agentNo)
        {
            Console.WriteLine("相关运算 agentNo:" + agentNo);
            return "相关运算 agentNo:" + agentNo;
        }

        public string WriteData(string agentNo)
        {
            Console.WriteLine("写入数据 agentNo:" + agentNo);
            return "写入数据 agentNo:" + agentNo;
        }

        public string SendDataFresh(string agentNo)
        {
            Console.WriteLine("发送数据更新通知 agentNo:" + agentNo);
            return "发送数据更新通知 agentNo:" + agentNo;
        }

    }
}
