using ED.BLL.CalculatePort;
using ED.BLL.CellPort;
using ED.BLL.CollectionPort;
using ED.BLL.CqPort;
using ED.BLL.ExcelPort;
using ED.BLL.VerifyPort;
using ED.Core;
using ED.DbRely;
using ED.ExcRely;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ED.BLL
{
    class Port
    {
        public IExtractValidation EvTarget { get; set; }
        public IExtractValidation EvSample { get; set; }
        public bool JoinDb { get; set; }
        public ICellHelper CellHelper { get; set; }//单元格接口
        public ICqHelper CqHelper { get; set; } //Cq接口
        public IExtractValidation EvFile { get; set; } //验证接口
        public IGroup Group { get; set; } //分组集合接口
        public ICalculation Calculation { get; set; } //计算接口
        public IOutPut Output { get; set; }//输出接口
        public IExcInstance ExcIns { get; set; }
        public IDbInstance DbIns { get; set; }
        //public IEDSett Set { get; set; }


    }
}
