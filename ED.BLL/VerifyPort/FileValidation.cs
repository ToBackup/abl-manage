using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ED.BLL.VerifyPort
{
    class FileValidation : IExtractValidation
    {
        public string Extract(string name)
        {
            string match = @"[0-9]{4}-[0-9]{2}-[0-9]{2}";
            Regex reg = new Regex(match);
            string str = reg.Match(name).Value;
            return reg.Match(name).Value;
        }

        public bool IsMeet(string name)
        {
            string match = @".*[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}-[0-9]{2}-[0-9]{2}.*";
            Regex reg = new Regex(match);
            bool meet = reg.IsMatch(name);
            return meet;
        }
    }
}
