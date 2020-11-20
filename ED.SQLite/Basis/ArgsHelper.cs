using ED.DbRely.Model;
using ED.SQLite.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.SQLite.Basis
{
    static class ArgsHelper
    {
        public static ExecArgs Delete(string table, AlterArgs args)
        {

            SQLiteParameter[] param = null;
            if(args !=null && args.Length>0)
            {
                param = new SQLiteParameter[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    param[i] = new SQLiteParameter(args[i].Name, args[i].Value);
                }
            }
            

            string cmdText = string.Format("Delete From {0} Where {1}", table, args?.Where);

            ExecArgs ea = new ExecArgs()
            {
                Text = cmdText,
                Type = CommandType.Text,
                Param = param
            };

            return ea;
        }

        public static ExecArgs Insert(string table, AddedArgs args)
        {
            string skey = string.Empty;
            string svalue = string.Empty;
            SQLiteParameter[] param = null;
            if(args!=null && args.Length>0)
            {
                param = new SQLiteParameter[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    skey += string.Format("{0},", args[i].Name);
                    svalue += string.Format("@{0},", args[i].Name);
                    param[i] = new SQLiteParameter(args[i].Name, args[i].Value);
                }

                skey = skey.Remove(skey.Length - 1);
                svalue = svalue.Remove(svalue.Length - 1);

            }
            

            string cmdText = string.Format("Insert Into {0} ({1}) Values ({2})", table, skey, svalue);

            ExecArgs ea = new ExecArgs()
            {
                Text = cmdText,
                Type = CommandType.Text,
                Param = param
            };

            return ea;

        }

        public static ExecArgs Insert<T>(T tag)
        {
            Type tTag = tag.GetType();
            string skey = string.Empty;
            string svalue = string.Empty;
            SQLiteParameter[] param = null;

            List<SQLiteParameter> list = new List<SQLiteParameter>();
            PropertyInfo[] ppts = tTag.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo ppt in ppts)
            {
                object obj = ppt.GetValue(tag, null);
                if (obj == null)
                    continue;
                else if(obj.GetType() == typeof(int))
                {
                    int iPpt = Convert.ToInt32(obj);
                    if(iPpt>=0)
                        list.Add(new SQLiteParameter(ppt.Name, iPpt));
                }
                else if(obj.GetType() == typeof(DateTime))
                {
                    DateTime dtPpt = Convert.ToDateTime(obj);
                    if (dtPpt > DateTime.MinValue)
                        list.Add(new SQLiteParameter(ppt.Name, dtPpt));
                }
                else
                {
                    string sPpt = obj.ToString();
                    list.Add(new SQLiteParameter(ppt.Name, sPpt));
                }

                skey += string.Format("{0},", ppt.Name);
                svalue += string.Format("@{0},", ppt.Name);
            }
            param = list.ToArray();

            string cmdText = string.Format("Insert Into {0} ({1}) Values ({2})", tTag.Name, skey, svalue);

            ExecArgs ea = new ExecArgs()
            {
                Text = cmdText,
                Type = CommandType.Text,
                Param = param
            };

            return ea;
        }

        public static ExecArgs Update(string table, AlterArgs args)
        {
            SQLiteParameter[] param = null;
            string set = string.Empty;
            if(args !=null && args.Length>0)
            {
                param = new SQLiteParameter[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    param[i] = new SQLiteParameter(args[i].Name, args[i].Value);
                    if(!args.Where.Contains(args[i].Name))
                    set += string.Format("{0}=@{0},", args[i].Name);
                }

                set = set.Remove(set.Length - 1);
            }

            string cmdText = string.Format("Update {0} Set {1} Where {2}", table, set, args?.Where);

            ExecArgs ea = new ExecArgs()
            {
                Text = cmdText,
                Type = CommandType.Text,
                Param = param
            };

            return ea;
        }

        public static ExecArgs Select(string table, QueryArgs args)
        {
            SQLiteParameter[] param = null;
            if (args != null && args.Length > 0)
            {
                param = new SQLiteParameter[args.Length];
                for (int i = 0; i < args?.Length; i++)
                {
                    param[i] = new SQLiteParameter(args[i].Name, args[i].Value);
                }
            }

            string row = string.IsNullOrEmpty(args?.Rows) ? "*" : args?.Rows;
            string where = !string.IsNullOrEmpty(args?.Where) ? string.Format("Where {0}", args.Where) : args?.Where;
            string group = !string.IsNullOrEmpty(args?.Group) ? string.Format("Group By {0}", args.Group) : args?.Group;
            string order = !string.IsNullOrEmpty(args?.Order) ? string.Format("Order By {0}", args.Order) : args?.Order;

            string cmdText = string.Format("Select {0} From {1} {2} {3} {4}", row, table, where, group, order);

            ExecArgs ea = new ExecArgs()
            {
                Text = cmdText,
                Type = CommandType.Text,
                Param = param
            };
            return ea;
        }

        public static ExecArgs Create(string type,string name,AddedArgs args)
        {
            string rows = string.Empty;

            if(args!=null && args.Length>0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    rows += string.Format("{0} {1}, ", args[i].Name, args[i].Value);
                }
                rows = rows.Remove(rows.Length - 2);
            }
            
            string cmdText = string.Format("Create {0} {1} ({2})",type, name, rows);

            ExecArgs ea = new ExecArgs()
            {
                Text = cmdText,
                Type = CommandType.Text
            };

            return ea;
        }
    }
}
