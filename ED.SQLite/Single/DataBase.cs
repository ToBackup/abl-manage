using ED.SQLite.Basis;
using ED.SQLite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.SQLite.Single
{
    class DataBase : DbHelper
    {
        public DataBase(string db) : base(db) { }

        public void CreateTable(params ExecArgs[] args)
        {
            foreach (ExecArgs arg in args)
            {
                ExecEditor(arg.Text, arg.Type, arg.Param);
            }
        }
    }
}
