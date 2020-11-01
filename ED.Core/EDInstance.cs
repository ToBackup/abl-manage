using ED.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.Core
{
    public static class EDInstance
    {
        private static IEDInstance GetInstance(EDArgs args)
        {
            if (args == null)
            {
                var cfa = ConfigurationManager.GetSection("External/Logic") as IDictionary;
                string[] keys = new string[cfa.Keys.Count];
                cfa.Keys.CopyTo(keys, 0);
                string db = (string)cfa[keys[0]];

                string[] sargs = db.Split('|');
                if (sargs.Length > 0)
                {
                    args = new EDArgs()
                    {
                        DllName = keys[0],
                        ClassName = sargs[0],
                        Args = sargs.Length > 1 && !string.IsNullOrEmpty(sargs[1]) ? sargs[1]?.Split(',') : null
                    };
                }
            }
            Type type = Assembly.LoadFrom(args.DllName)?.GetType(args.ClassName);
            IEDInstance instance = Activator.CreateInstance(type, args.Args) as IEDInstance;

            return instance;
        }
        public static IEDLoad GetLoad(string name,EDArgs args = null)
        {
            IEDInstance instance = GetInstance(args);

            return instance.GetEDLoad(name);
        }
        public static IEDSett GetSett(string name,EDArgs args = null)
        {
            IEDInstance instance = GetInstance(args);

            return instance.GetEDSett(name);
        }
    }
}
