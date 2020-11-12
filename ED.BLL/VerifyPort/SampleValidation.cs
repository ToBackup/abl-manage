using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;

namespace ED.BLL.VerifyPort
{
    class SampleValidation : IExtractValidation
    {
        public string Extract(string name)
        {
            string sp = null;

            if(!string.IsNullOrEmpty(name))
            {
                int n = name.IndexOf('-');
                if (n > 0)
                    sp = name.Remove(n);
                else
                    sp = name;
            }
            return sp;
        }

        public bool IsMeet(string name)
        {
            Regex reg = new Regex(".+");

            return reg.IsMatch(name);
        }
    }
}
