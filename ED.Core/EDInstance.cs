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
        private static IEDInstance GetInstance()
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //Type type = Assembly.LoadFrom(args.DllName)?.GetType(args.ClassName);
            //IEDInstance instance = Activator.CreateInstance(type, args.Args) as IEDInstance;

            //return instance;
            return null;
        }
        public static IEDLoad GetLoad(string name)
        {
            IEDInstance instance = GetInstance();

            return instance.GetEDLoad(name);
        }
        public static IEDSett GetSett(string name)
        {
            IEDInstance instance = GetInstance();

            return instance.GetEDSett();
        }

    }
}
