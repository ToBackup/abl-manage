using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.DbRely.Model
{
    public class AlterArgs : AddedArgs
    {
        public AlterArgs() { }
        public AlterArgs(int count):base(count)
        { }

        public string Where { get; set; }
    }
}
