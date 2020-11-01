using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ED.ExcRely.Model;
using ED.NPOI;
using ED.NPOI.Achieve;
using NPOI.SS.UserModel;

namespace ED.NPOI.Model
{
    class CellEnum :Stream, IEnumerator<Cell>
    {
        List<ICell> _cells;
        public int position = -1;

        public CellEnum(List<ICell> cells)
        {
            _cells = cells;
        }

        Cell IEnumerator<Cell>.Current => Get();

        public object Current => Get();

        public bool MoveNext()
        {
            position++;
            return position < _cells?.Count;
        }

        public void Reset()
        {
            position = -1;
        }

        public Cell Get()
        {
            ICell icell = _cells[position];

            Cell cell = new Cell(GetValue(icell), icell.RowIndex, icell.ColumnIndex);

            return cell;
        }

    }
}
