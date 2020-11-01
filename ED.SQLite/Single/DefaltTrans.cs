using ED.DbRely;
using ED.SQLite.Basis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.SQLite.Single
{
    class DefaltTrans : DbHelper, IDbTrans
    {
        public DefaltTrans() { }
        public DefaltTrans(string db) : base(db) { }
        public void Begin()
        {
            BeginTrans();
        }

        public void Commit()
        {
            CommitTrans();
        }

        public void Rollback()
        {
            RollbackTrans();
        }
    }
}
