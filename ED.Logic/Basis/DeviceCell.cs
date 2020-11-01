using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ED.Logic.Basis
{
    class DeviceCell : ICellHelper
    {
        private string fileName;
        public DeviceCell(string fileName)
        {
            this.fileName = fileName;
        }
        public void ByDictionary(Dictionary<string, Cell> cells)
        {

        }

        public void BySingle(Cell cell)
        {

        }

        public bool FindTags(string[] tags, IEnumerable<Cell> cells, out int[] colIndex)
        {
            bool has = false;
            colIndex = new int[tags.Length];

            for (int i = 0; i < tags.Length; i++)
            {
                Cell cell = cells.FirstOrDefault(c => { return c == tags[i]; });
                colIndex[i] = cell?.ColumnIndex ?? -1;
            }

            return has;
        }
    }
}
