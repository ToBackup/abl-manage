using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.DbRely.Model
{
    public class DbParams
    {
        public DbParams() { }
        public DbParams(string name,dynamic value)
        {
            this.Name = name;
            this.Value = value;
        }
        public string Name { get; set; }
        public dynamic Value { get; set; }
    }
}
