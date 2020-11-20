using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{
    public interface IEDInstance
    {
        IEDLoad GetEDLoad(string name);
        IEDSett GetEDSett();
    }
}
