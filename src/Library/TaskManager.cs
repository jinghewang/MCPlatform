using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Library
{
    public class TaskManager
    {

        /// <summary>
        /// 加载任务相关配置
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public Task LoadTask(string taskNo)
        {
            Task task = new Task()
            {
                Id = taskNo,
                Name = "name:" + taskNo,
                Desc = "desc",
                Status = 0,
                SubTask = new List<SubTask>() {
                    new SubTask{ Id="subtask-1", Name="subtask-1", Desc="desc", TypeName="Library.TaskTest", MethodName="CollectData", Param=new object[]{"A10"}},
                    new SubTask{ Id="subtask-2", Name="subtask-2", Desc="desc", TypeName="Library.TaskTest", MethodName="ComputeData", Param=new object[]{"A10"}},
                    new SubTask{ Id="subtask-3", Name="subtask-3", Desc="desc", TypeName="Library.TaskTest", MethodName="WriteData", Param=new object[]{"A10"}},
                    new SubTask{ Id="subtask-4", Name="subtask-4", Desc="desc", TypeName="Library.TaskTest", MethodName="SendDataFresh", Param=new object[]{"A10"}}
                }
            };

            return task;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public int ExecuteTask(Task task)
        {
            for (int i = 0; i < task.SubTask.Count; i++)
            {
                SubTask subTask = task.SubTask[i];
                object result = TaskManager.InvokeMethod(subTask.TypeName, subTask.MethodName, subTask.Param);

            }
            return 1;
        }


        public int LoadAndExecuteTask(string taskNo)
        {
            var task = this.LoadTask(taskNo);
            return this.ExecuteTask(task);
        }


        #region InvokeMethod
        public static object CreateInstance(string typeName)
        {
            Type t = Type.GetType(typeName);
            return Activator.CreateInstance(t);
        }

        public static object InvokeMethod(string typeName, string methodName, params object[] args)
        {
            object target = CreateInstance(typeName);
            MethodInfo methodInfo = target.GetType().GetMethod(methodName);
            return methodInfo.Invoke(target, args);
        }

        public static object InvokeMethod(object target, string methodName, params object[] args)
        {
            MethodInfo methodInfo = target.GetType().GetMethod(methodName);
            return methodInfo.Invoke(target, args);
        }

        //public object InvokeMethod2(object target,string methodName, params object[] args)
        //{
        //    object obj = target.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, target, args);
        //    return obj;
        //} 
        #endregion

    }
}
