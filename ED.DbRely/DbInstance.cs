using ED.DbRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Configuration;
using System.Xml;
using System.Collections;

namespace ED.DbRely
{
    public static class DbInstance
    {
        private static IDbInstance GetInstance(DbArgs args)
        {
            if(args == null)
            {
                var cfa = ConfigurationManager.GetSection("External/DataBase") as IDictionary;
                string[] keys = new string[cfa.Keys.Count];
                cfa.Keys.CopyTo(keys, 0);
                string db = (string)cfa[keys[0]];

                string[] sargs = db.Split('|');
                if(sargs.Length>0)
                {
                    args = new DbArgs()
                    {
                        DllName = keys[0],
                        ClassName = sargs[0],
                        Args = sargs.Length > 1 && !string.IsNullOrEmpty(sargs[1]) ? sargs[1]?.Split(',') : null
                    };
                }
            }

            Type type = Assembly.LoadFrom(args.DllName)?.GetType(args.ClassName);
            IDbInstance instance = Activator.CreateInstance(type, args.Args) as IDbInstance;
            return instance;
        }
        public static IDbAlter GetAlter(string name,DbArgs args = null)
        {
            IDbInstance instance = GetInstance(args);
            return instance.GetAlter(name);
        }
        public static IDbQuery GetQuery(string name, DbArgs args = null)
        {
            IDbInstance instance = GetInstance(args);
            return instance.GetQuery(name);
        }
        public static IDbTrans GetTrans(DbArgs args = null)
        {
            IDbInstance instance = GetInstance(args);
            return instance.GetTrans();
        }

    }
}
