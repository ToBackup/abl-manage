using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.VerifyPort
{
    interface IExtractValidation
    {
        bool IsMeet(string name);
        string Extract(string name);
    }
}
