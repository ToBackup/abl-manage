using ED.DbRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.DbRely
{
    public interface IDbInstance
    {
        IDbAlter GetAlter(string name);
        IDbQuery GetQuery(string name);
        IDbTrans GetTrans();
    }
}
