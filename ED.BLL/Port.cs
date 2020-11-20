using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.BLL
{
    class Port
    {
        string dll, cls;
        string[] args;

        public Port(string dll,string cls, string[] args)
        {
            this.dll = dll;
            this.cls = cls;
            this.args = args;
        }

        public bool HasFile => File.Exists(dll);
        public  T GetInstance<T>() where T : class
        {
            Type type = Assembly.LoadFrom(dll)?.GetType(cls);
            T instance = Activator.CreateInstance(type, args) as T;
            return instance;
        }
    }
}
