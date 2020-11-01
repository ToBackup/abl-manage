using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.ExcRely.Model
{
    public class ExcArgs
    {
        public string DllName { get; set; }
        public string ClassName { get; set; }
        public object[] Args { get; set; }
    }
}
