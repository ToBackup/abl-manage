using ED.BLL.CalculatePort;
using ED.BLL.CellPort;
using ED.BLL.CollectionPort;
using ED.BLL.CqPort;
using ED.BLL.ExcelPort;
using ED.BLL.VerifyPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Model
{
    static class PortArgs
    {
        static bool JoinDb { get; set; }
        static ICellHelper CellHelper { get; set; }  //单元格接口
        static ICqHelper CqHelper { get; set; } //Cq接口
        static IExtractValidation EvFile { get; set; }
        static IExtractValidation EvTarget { get; set; }
        static IExtractValidation EvSample { get; set; } //验证接口
        static IGroup Group { get; set; } //分组集合接口
        static ICalculation Calculation { get; set; } //计算接口
        static IOutPut OutPut { get; set; } //输出接口
    }
}
