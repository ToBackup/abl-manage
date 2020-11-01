using ED.ExcRely.Model;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.NPOI.Model
{
    class CellCollection : IEnumerable<Cell>
    {
        List<ICell> _cells;
        public CellCollection(List<ICell> cells)
        {
            _cells = cells;
        }

        public CellEnum GetCells()
        {
            return new CellEnum(_cells);
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return GetCells();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetCells();
        }
    }
}
