using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.DbRely.Model
{
    public class QueryArgs : AlterArgs
    {
        public QueryArgs() { }
        public QueryArgs(int count):base(count)
        { }
        public string Rows { get; set; }
        public string Group { get; set; }
        public string Order { get; set; }
    }
}
