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
        public ICellHelper cc = null;  //单元格接口
        public ICqHelper cqGrouper = null; //Cq接口
        public IExtractValidation evFile/*, evTarget, evSample*/; //验证接口
        public IGroup clt; //分组集合接口
        public ICalculation ccl = null; //计算接口
        public IOutPut output = null; //输出接口
        public IExcInstance excIns = null;
        public IDbInstance dbIns = null;
        public IExtractValidation evTarget = new TargetValidation();
        public IExtractValidation evSample = new SampleValidation();
        public IEDSett Set;
    }
}
