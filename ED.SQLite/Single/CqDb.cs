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
    class CqDb : DbHelper, IDbAlter
    {
        public int Delete(AlterArgs args)
        {
            ExecArgs ea = ArgsHelper.Delete("Cq", args);
            int count = ExecEditor(ea.Text, ea.Type, ea.Param);
            return count;
        }

        public int Insert(AddedArgs args)
        {
            ExecArgs ea = ArgsHelper.Insert("Cq", args);
            int count = ExecEditor(ea.Text, ea.Type, ea.Param);
            return count;
        }
        public DataTable SelectToDT(QueryArgs args)
        {
            ExecArgs ea = ArgsHelper.Select("View_Cq", args);
            DataTable dt = ExecQuery(ea.Text, ea.Type, ea.Param);
            return dt;
        }
        public int Insert<T>(T tag)
        {
            throw new NotImplementedException();
        }


        public List<T> SelectToList<T>(QueryArgs args) where T : class
        {
            throw new NotImplementedException();
        }

        public T SelectToObject<T>(QueryArgs args) where T : class
        {
            throw new NotImplementedException();
        }

        public object SelectToValue(QueryArgs args)
        {
            throw new NotImplementedException();
        }

        public int Update(AlterArgs args)
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T tag, AlterArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
