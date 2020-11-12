using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Model
{
    class File
    {
        public File()
        {
            Date = DateTime.MinValue;
        }
        public string FID { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return FID;
        }
    }
}
