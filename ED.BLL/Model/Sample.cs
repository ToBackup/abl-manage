using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Model
{
    class Sample
    {
        public Sample()
        {
            SID = -1;
            Name = null;
            Branch = null;
        }
        public int SID { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }

        public override string ToString()
        {
            return Branch;
        }
    }
}
