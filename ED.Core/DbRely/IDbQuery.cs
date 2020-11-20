using ED.DbRely.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ED.DbRely
{
    public interface IDbQuery : IDisposable
    {
        void Open();
        void Close();
        DataTable SelectToDT(QueryArgs args);

        List<T> SelectToList<T>(QueryArgs args) where T : class;

        T SelectToObject<T>(QueryArgs args) where T : class;

        object SelectToValue(QueryArgs args);
    }
}
