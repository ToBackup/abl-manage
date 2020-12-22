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
            IEDSett set = GetEDSett();
            Port port;


            switch (name)
            {
                case "I":
                    port = new Port()
                    {
                        CellHelper = new CellHelperI(),
                        EvFile = new FileValidation(),
                        EvTarget = new TargetValidation(),
                        EvSample = new SampleValidation(),
                        Group = new GroupI(),
                        Calculation = new CalculateI(),
                        Output = new OutPutI(),
                        ExcIns = excIns,
                        DbIns = dbIns,
                        JoinDb = Convert.ToBoolean(set.Read("JoinDb"))
                    };
                    port.CqHelper = new CqHelperI(port.EvTarget, port.EvSample);
                    load = new Load.LoadI(port);
                    break;
                case "II":
                    port = new Port()
                    {
                        CellHelper = new CellHelperI(),
                        EvFile = new FileValidation(),
                        EvTarget = new TargetValidation(),
                        EvSample = new SampleValidation(),
                        Group = new GroupII(),
                        Calculation = new CalculateI(),
                        Output = new OutPutII(),
                        ExcIns = excIns,
                        DbIns = dbIns,
                        JoinDb = Convert.ToBoolean(set.Read("JoinDb"))
                    };
                    port.CqHelper = new CqHelperII(port.EvTarget, port.EvSample);

                    load = new LoadII(port);break;
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
