using ED.BLL.Model;
using ED.Core;
using ED.Core.Model;
using ED.DbRely.Model;
using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.BLL.Sett
{
    class UserSett : IEDSett
    {
        public T Read<T>(T tag) where T : SettBase
        {
            Type type = GetSett(tag);

            Exchange(tag, type);

            return tag;
        }

        public void Write<T>(T set) where T : SettBase
        {
            Type type = GetSett(set);

            DeExchange(set, type);
        }

        private Type GetSett<T>(T tag) where T:SettBase
        {
            Type type = null;

            //if(tag)
            switch (tag.Tag)
            {
                case "Config":
                    type = typeof(Config);
                    break;
                default:
                    break;
            }
            return type;
        }

        private void Exchange(SettBase sb, Type tr)
        {
            if(sb.Tag == "Config")
            {
                Type tt = sb.GetType();
                Setts set = new Setts();
                PropertyInfo[] pts = tt.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo pt in pts)
                {
                    if(pt.Name !="Tag")
                    {
                        string value = set[pt.Name];
                        pt.SetValue(sb, value, null);
                    }
                }
            }
        }
        private void DeExchange(SettBase sb,Type tr)
        {
            if (sb.Tag == "Config")
            {
                Type tt = sb.GetType();
                Setts set = new Setts();
                PropertyInfo[] pts = tt.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo pt in pts)
                {
                    if(pt.Name != "Tag")
                    {
                        string value = pt.GetValue(sb, null) as string;
                        set.Set(pt.Name, value);
                    }
                    
                }
            }
        }

        public string Read(string key)
        {
            Setts set = new Setts();
            return set[key];
        }

        public void Write(string key, string value)
        {
            Setts set = new Setts();
            set.Set(key, value);
        }
    }
}
