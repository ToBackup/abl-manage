using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.DbRely
{
    public interface IDbTrans
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}
