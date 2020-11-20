using ED.ExcRely.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.ExcRely
{
    public interface IExcReader : IExcStream
    {
        Cell this[int index] { get; }
        int Position { get; set; }
        int RowCount { get; }
        int ColumnCount { get; }
        IEnumerable<Cell> Columns { get; }
        bool Read();
    }
}
