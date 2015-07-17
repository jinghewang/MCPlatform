using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class SubTask
    {
        private string _id = null;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name = null;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _desc = null;

        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        private object[] param = null;

        public object[] Param
        {
            get { return param; }
            set { param = value; }
        }

        private string typeName;

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private string methodName;

        public string MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }

    }
}
