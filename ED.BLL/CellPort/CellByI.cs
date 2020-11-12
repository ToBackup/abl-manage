using ED.BLL.Model;
using ED.ExcRely;
using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CellPort
{
    class CellByI : ICellHelper
    {
        private int[] indexs;
        private string[] _toObtainColumn;

        public string[] ToObtainColumn
        {
            get => _toObtainColumn;
            set
            {
                _toObtainColumn = value;
                indexs = null;
                IsFindColumn = false;
            }
        }

        public bool IsFindColumn { get; private set; }

        public void FindColumnIndex(IEnumerable<Cell> columns)
        {
            indexs = new int[ToObtainColumn.Length];

            for (int i = 0; i < ToObtainColumn.Length; i++)
            {
                Cell cell = columns.FirstOrDefault(c => { return c == ToObtainColumn[i]; });
                indexs[i] = cell?.ColumnIndex ?? -1;
            }

            IsFindColumn = indexs.All(x => { return x > 0; });
        }

        public CellArgs GetArgs(IExcReader reader)
        {
            Dictionary<string, Cell> cells = new Dictionary<string, Cell>();
            for (int i = 0; i < indexs.Length; i++)
            {
                Cell cell = reader[indexs[i]];
                cells.Add(ToObtainColumn[i], cell);
            }
            
            CellArgs ca = new CellArgs()
            {
                Cells = cells
            };

            return ca;
        }

    }
}
