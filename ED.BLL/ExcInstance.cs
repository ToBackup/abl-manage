using ED.ExcRely.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.ExcRely
{
    public static class ExcInstance
    {
        private static string dll;
        private static string cls;
        private static string[] args;
        private static bool? binded;
        public static bool Binded => binded ?? !string.IsNullOrEmpty(dll) && !string.IsNullOrEmpty(cls) && File.Exists(dll);
        private static IExcInstance GetInstance()
        {
            Type type = Assembly.LoadFrom(dll)?.GetType(cls);
            IExcInstance instance = Activator.CreateInstance(type, args) as IExcInstance;

            return instance;
        }
        public static IExcReader GetReader()
        {
            IExcInstance instance = GetInstance();

            return instance.GetReader();
        }

        public static IExcWriter GetWriter()
        {
            IExcInstance instance = GetInstance();
            return instance.GetWriter();
        }
        public static void Setting(string dll, string cls, params string[] args)
        {
            ExcInstance.dll = dll;
            ExcInstance.cls = cls;
            ExcInstance.args = args;
        }
    }
}
