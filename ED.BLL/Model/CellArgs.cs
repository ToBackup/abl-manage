using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Model
{
    class CellArgs
    {
        public Cell this[string name]=>Cells[name];
        public Dictionary<string,Cell> Cells { get; set; }

        public File File { get; set; }

    }
}
