using ED.ExcRely.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.ExcRely
{
    public static class ExcInstance
    {
        private static IExcInstance GetInstance(ExcArgs args)
        {
            if(args == null)
            {
                var cfa = ConfigurationManager.GetSection("External/Excel") as IDictionary;
                string[] keys = new string[cfa.Keys.Count];
                cfa.Keys.CopyTo(keys, 0);
                string db = (string)cfa[keys[0]];

                string[] sargs = db.Split('|');
                if (sargs.Length > 0)
                {
                    args = new ExcArgs()
                    {
                        DllName = keys[0],
                        ClassName = sargs[0],
                        Args = sargs.Length > 1 && !string.IsNullOrEmpty(sargs[1]) ? sargs[1]?.Split(',') : null
                    };
                }
            }
            Type type = Assembly.LoadFrom(args.DllName)?.GetType(args.ClassName);
            IExcInstance instance = Activator.CreateInstance(type, args.Args) as IExcInstance;

            return instance;
        }
        public static IExcReader GetReader(ExcArgs args = null)
        {
            IExcInstance instance = GetInstance(args);

            return instance.GetReader();
        }

        public static IExcWriter GetWriter(ExcArgs args = null)
        {
            IExcInstance instance = GetInstance(args);
            return instance.GetWriter();
        }
    }
}
