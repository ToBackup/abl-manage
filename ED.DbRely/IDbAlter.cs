using ED.DbRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.DbRely
{
    public interface IDbAlter : IDbQuery
    {
        int Insert(AddedArgs args);
        int Delete(AlterArgs args);
        int Update(AlterArgs args);
    }
}
