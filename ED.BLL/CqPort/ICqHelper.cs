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
        Cq ConvertToCq(CellArgs args);
        void InsertToCollection(Cq cq,IGroup clt);
    }
}
