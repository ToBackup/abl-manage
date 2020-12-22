using ED.DbRely;
using ED.DbRely.Model;
using ED.SQLite.Basis;
using ED.SQLite.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ED.SQLite.Single
{
    class Cq : DbHelper, IDbAlter
    {
        public Cq(string db) : base(db) { }
        public int Delete(AlterArgs args)
        {
            ExecArgs ea = ArgsHelper.Delete("Record", args);
            int count = ExecEditor(ea.Text, ea.Type, ea.Param);
            return count;
        }

        public int Insert(AddedArgs args)
        {
            ExecArgs ea = ArgsHelper.Insert("Record", args);
            int count = ExecEditor(ea.Text, ea.Type, ea.Param);
            return count;
        }
        public DataTable SelectToDT(QueryArgs args)
        {
            ExecArgs ea = ArgsHelper.Select("Record", args);
            DataTable dt = ExecQuery(ea.Text, ea.Type, ea.Param);
            return dt;
        }
        public int Insert<T>(T tag,params string[] keys)
        {
            ExecArgs ea = ArgsHelper.Insert<T>(tag,keys);
            int count = ExecEditor(ea.Text, ea.Type, ea.Param);
            return count;
        }
        public List<T> SelectToList<T>(QueryArgs args) where T : class
        {
            ExecArgs ea = ArgsHelper.Select("Record", args);
            List<T> list = ExecQuery<T>(ea.Text, ea.Type, ea.Param);
            return list;
        }

        public T SelectToObject<T>(QueryArgs args) where T : class
        {
            ExecArgs ea = ArgsHelper.Select("Record", args);
            List<T> list = ExecQuery<T>(ea.Text, ea.Type, ea.Param);
            return list[0];
        }

        public object SelectToValue(QueryArgs args)
        {
            ExecArgs ea = ArgsHelper.Select("Record", args);
            object obj = ExecScalar(ea.Text, ea.Type, ea.Param);

            return obj;
        }

        public int Update(AlterArgs args)
        {
            ExecArgs ea = ArgsHelper.Update("Record", args);
            int count = ExecEditor(ea.Text, ea.Type, ea.Param);
            return count;
        }

        public int Update<T>(T tag, AlterArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
