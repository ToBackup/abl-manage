using ED.BLL.Load;
using ED.BLL.Model;
using ED.BLL.Sett;
using ED.Core;
using ED.Core.Model;
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
        public Export(PortArgs xls,PortArgs db)
        {
            Port excPort = new Port(xls.DllName, xls.ClassName, xls.Args);
            excIns = excPort.HasFile ? excPort.GetInstance<IExcInstance>() : null;
            Port dbPort = new Port(db.DllName, xls.ClassName, xls.Args);
            dbIns = dbPort.HasFile ? dbPort.GetInstance<IDbInstance>() : null;
        }

        public IEDLoad GetEDLoad(string name)
        {
            IEDLoad load = null;
            switch (name)
            {
                case "I":load = new LoadI(GetEDSett(),excIns,dbIns);break;

                default:
                    break;
            }

            return load;
        }

        public IEDSett GetEDSett()
        {
            IEDSett set = null;
            
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            if (dbIns!=null)
            {
            }
            else
                set = new UserSett();

            return set;
        }
    }
}
