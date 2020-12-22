using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Model
{
    class Record
    {
        public Record()
        {
            ID = -1;
            Date = DateTime.MinValue;
            Value = 0;
        }
        public int ID { get; set; }
        public string FID { get; set; }
        public string File { get; set; }
        public DateTime Date { get; set; }
        public string TargetKey { get; set; }
        public string TargetName { get; set; }
        public string SampleKey { get; set; }
        public string SampleName { get; set; }
        public double Value { get; set; }

        public static implicit operator double(Record r)
        {
            return r.Value;
        }

        public override string ToString()
        {
            return string.Join("_", FID, TargetName, SampleName, Value);
        }
    }
}
