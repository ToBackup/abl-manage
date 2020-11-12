using ED.BLL.Load;
using ED.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL
{
    public class Export : IEDInstance
    {
        public IEDLoad GetEDLoad(string name)
        {
            IEDLoad load = null;
            switch (name)
            {
                case "I":load = new LoadI();break;

                default:
                    break;
            }

            return load;
        }

        public IEDSett GetEDSett(string name)
        {
            throw new NotImplementedException();
        }
    }
}
