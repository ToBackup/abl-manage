using ED.DbRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Configuration;
using System.Xml;
using System.Collections;
using System.IO;

namespace ED.DbRely
{
    public static class DbInstance
    {
        private static string dll;
        private static string cls;
        private static string[] args;
        private static bool? binded;

        public static bool Binded => binded ?? !string.IsNullOrEmpty(dll) && !string.IsNullOrEmpty(cls) && File.Exists(dll); 
        private static IDbInstance GetInstance()
        {
            //DbArgs args = new DbArgs();

            Type type = Assembly.LoadFrom(dll)?.GetType(cls);
            IDbInstance instance = Activator.CreateInstance(type, args) as IDbInstance;
            return instance;
        }
        public static IDbAlter GetAlter(string name)
        {
            IDbInstance instance = GetInstance();
            return instance.GetAlter(name);
        }
        public static IDbQuery GetQuery(string name)
        {
            IDbInstance instance = GetInstance();
            return instance.GetQuery(name);
        }
        public static IDbTrans GetTrans()
        {
            IDbInstance instance = GetInstance();
            return instance.GetTrans();
        }

        public static void Setting(string dll, string cls, params string[] args)
        {
            //DbArgs dbArgs = new DbArgs();
            //dbArgs.DllName = dll;
            //dbArgs.ClassName = cls;
            //dbArgs.Args = args;
            //dbArgs.Save();
            DbInstance.dll = dll;
            DbInstance.cls = cls;
            DbInstance.args = args;
        }
    }
}
