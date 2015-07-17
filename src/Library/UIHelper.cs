using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library
{
    public class UIHelper
    {
        public static void ShowMsg(string msg = "操作成功")
        {
            MessageBox.Show(msg, "信息提示");
        }

        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "错误提示");
        }
    }
}
