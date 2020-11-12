using ED.BLL.CalculatePort;
using ED.BLL.CollectionPort;
using ED.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.ExcelPort
{
    interface IOutPut :IProgress
    {
        void ToExcel(IGroup group, ResultCollection result);
    }
}
