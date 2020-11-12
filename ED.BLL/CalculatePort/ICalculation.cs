using ED.BLL.CollectionPort;
using ED.BLL.Model;
using ED.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CalculatePort
{
    interface ICalculation : IProgress
    {
        ResultCollection ToCalculate(IGroup group);
    }
}
