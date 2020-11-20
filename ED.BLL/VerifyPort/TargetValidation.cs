using ED.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ED.BLL.VerifyPort
{
    class TargetValidation : IExtractValidation
    {
        public string Extract(string name)
        {
            Regex reg = new Regex("[a-zA-Z][0-9]+");
            Match match = reg.Match(name);
            if (match.Success)
                return match.Value;
            else
                return null;
        }
        public bool IsMeet(string name)
        {
            Regex reg = new Regex("P|M.+");
            bool meet = reg.IsMatch(name);
            return meet;
        }
    }
}
