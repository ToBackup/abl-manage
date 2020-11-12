using ED.BLL.Model;
using ED.ExcRely;
using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CellPort
{
    interface ICellHelper
    {
        string[] ToObtainColumn { get; set; }

        bool IsFindColumn { get;}

        void FindColumnIndex(IEnumerable<Cell> columns);
        CellArgs GetArgs(IExcReader reader);
    }
}
