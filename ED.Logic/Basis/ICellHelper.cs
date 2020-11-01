using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Logic.Basis
{
    interface ICellHelper
    {
        bool FindTags(string[] tags,IEnumerable<Cell> cells,out int[] colIndex);
        void ByDictionary(Dictionary<string, Cell> cells);
        void BySingle(Cell cell);
    }
}
