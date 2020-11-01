using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core.Model
{
    public class EDArgs
    {
        public string DllName { get; set; }
        public string ClassName { get; set; }
        public object[] Args { get; set; }
    }
}
