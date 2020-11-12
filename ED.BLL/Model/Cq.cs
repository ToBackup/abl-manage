using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Model
{
    class Cq
    {
        public Cq()
        {
            Value = 0;
            CID = -1;
            File = null;
            Sample = null;
            Target = null;
        }
        public double Value { get; set; }
        public int CID { get; set; }
        public File File { get; set; }
        public Sample Sample { get; set; }
        public Target Target { get; set; }

        public static implicit operator double(Cq cq)
        {
            return cq?.Value ?? 0;
        }
        public override string ToString()
        {
            return string.Format("File:{0}|Target:{1}|Sample:{2}|Cq:{3}", File, Target, Sample, Value);
        }
    }
}
