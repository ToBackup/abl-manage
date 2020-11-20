using ED.BLL.CalculatePort;
using ED.BLL.CellPort;
using ED.BLL.CollectionPort;
using ED.BLL.CqPort;
using ED.BLL.ExcelPort;
using ED.BLL.Load;
using ED.BLL.Model;
using ED.BLL.Sett;
using ED.BLL.VerifyPort;
using ED.Core;
using ED.DbRely;
using ED.DbRely.Model;
using ED.ExcRely;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ED.BLL
{
    public class Export : IEDInstance
    {
        IExcInstance excIns;
        IDbInstance dbIns;
        public Export()
        {
            excIns = new NPOI.Export();
            dbIns = new SQLite.Export();
        }

        public IEDLoad GetEDLoad(string name)
        {
            IEDLoad load = null;
            Port port = new Port()
            {
                cc = new CellByI(),
                evFile = new FileValidation(),
                evTarget = new TargetValidation(),
                evSample = new SampleValidation(),
                clt = new GroupI(),
                ccl = new CalculateI(),
                output = new OutPutI(),
                excIns = excIns,
                dbIns = dbIns,
                Set = GetEDSett()
            };
            port.cqGrouper = new CqByI(port.evTarget, port.evSample);

            switch (name)
            {
                case "I":load = new LoadI(port);break;

                default:
                    break;
            }

            return load;
        }


        public IEDSett GetEDSett()
        {
            IEDSett set = null;
            set = new UserSett();
            return set;
        }
    }
}
