using ED.BLL.CollectionPort;
using ED.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CqPort
{
    interface ICqHelper
    {
        Record ConvertToCq(CellArgs args);
        void InsertToCollection(Record cq,IGroup clt);
    }
}
