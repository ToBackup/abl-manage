using ED.NPOI.Model;
using ED.ExcRely;
using ED.ExcRely.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSCell = NPOI.SS.UserModel.ICell;

namespace ED.NPOI.Achieve
{
    class Reader : Stream, IExcReader
    {
        IRow row = null;
        int position = -1;

        public Cell this[int index] => new Cell(GetValue(row?.GetCell(index)),position,index);
        public int Position
        {
            get => position;
            set
            {
                if (value >= 0)
                {
                    row = sheet?.GetRow(value);
                    if (row?.ZeroHeight ?? false)
                    {
                        if (value < RowCount)
                            Position++;
                    }
                    position = value;
                }
            }
        }

        public int RowCount => (sheet?.LastRowNum??-1) + 1;

        public int ColumnCount => (row?.LastCellNum??-1) + 1;

        public IEnumerable<Cell> Columns => new CellCollection(row?.Cells);

        public bool Read()
        {
            Position++;
            bool isHasRow = false;
            if (Position < RowCount)
                isHasRow = true;
            return isHasRow;
        }
        public List<SSCell> Get()
        {
            
            return row.Cells;
        }
    }
}
