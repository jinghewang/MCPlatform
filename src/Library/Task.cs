using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Library
{
    public class Task
    {

        public List<SubTask> SubTask = new List<SubTask>();

        #region Property
        private string id = null;
        /// <summary>
        /// 任务ID
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name = null;
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string desc = null;
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        private int status = 0;
        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        private int executeType = 0;
        /// <summary>
        /// 执行方式 0串行(默认) 1并行
        /// </summary>
        public int ExecuteType
        {
            get { return executeType; }
            set { executeType = value; }
        }
        #endregion

        /// <summary>
        /// 执行子任务
        /// </summary>
        /// <returns></returns>
        //public int ExecuteSubTask()
        //{
        //    return 0;
        //}


    }
}
