using ED.DbRely;
using ED.DbRely.Model;
using ED.SQLite.Basis;
using ED.SQLite.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.SQLite.Single
{
    class Configure : DbHelper, IDbAlter
    {
        public Configure(string db):base(db)
        { }
        public int Delete(AlterArgs args)
        {
            ExecArgs ea = ArgsHelper.Delete("Config", args);

            int count = this.ExecEditor(ea.Text, ea.Type, ea.Param);

            return count;
        }

        public int Insert(AddedArgs args)
        {
            ExecArgs ea = ArgsHelper.Insert("Config", args);

            return this.ExecEditor(ea.Text, ea.Type, ea.Param);
        }


        public DataTable SelectToDT(QueryArgs args)
        {
            ExecArgs ea = ArgsHelper.Select("Config", args);

            return ExecQuery(ea.Text, ea.Type, ea.Param);
        }

        public List<T> SelectToList<T>(QueryArgs args) where T : class
        {
            ExecArgs ea = ArgsHelper.Select("Config", args);

            return ExecQuery<T>(ea.Text, ea.Type, ea.Param);
        }

        public T SelectToObject<T>(QueryArgs args) where T : class
        {
            ExecArgs ea = ArgsHelper.Select("Config", args);

            return ExecQuery<T>(ea.Text, ea.Type, ea.Param)[0];
        }

        public object SelectToValue(QueryArgs args)
        {
            ExecArgs ea = ArgsHelper.Select("Config", args);

            object value = this.ExecScalar(ea.Text, ea.Type, ea.Param);

            return value;
        }

        public int Update(AlterArgs args)
        {
            ExecArgs ea = ArgsHelper.Update("Config", args);

            return ExecEditor(ea.Text, ea.Type, ea.Param);
        }

        public void Open()
        {
            OpenConn();
        }

        public void Close()
        {
            Dispose();
        }

        public int Insert<T>(T tag)
        {
            ExecArgs ea = ArgsHelper.Insert(tag);

            return ExecEditor(ea.Text, ea.Type, ea.Param);

        }
    }
}
