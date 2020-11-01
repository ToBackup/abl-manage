using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.DbRely.Model
{
    public class DbArgs
    {
        public string DllName { get; set; }
        public string ClassName { get; set; }
        public object[] Args { get; set; }
    }
}
