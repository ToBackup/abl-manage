using ED.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Logic
{
    public class Export : IEDInstance
    {
        public IEDLoad GetEDLoad(string name)
        {
            Type type = Type.GetType(string.Format("ED.Logic.Achieve.{0}Load",name));
            IEDLoad load = Activator.CreateInstance(type) as IEDLoad;

            return load;
        }

        public IEDSett GetEDSett(string name)
        {
            Type type = Type.GetType(string.Format("ED.Logic.Achieve.{0}Sett" , name??"Xml"));
            IEDSett set = Activator.CreateInstance(type) as IEDSett;

            return set;

        }
    }
}
