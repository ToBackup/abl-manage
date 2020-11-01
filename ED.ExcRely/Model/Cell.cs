using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ED.ExcRely.Model
{
    public class Cell
    {
        public Cell(dynamic value,int rowIndex,int columnindex)
        {
            this.Value = value;
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnindex;
        }
        public dynamic Value { get; private set; }
        public int RowIndex { get; private set; }
        public int ColumnIndex { get; private set; }
        
        public static implicit operator string(Cell cell)
        {
            if (cell.Value.GetType() == typeof(string))
                return cell.Value;
            else
                return cell.Value.ToString();
        }
        public static explicit operator double(Cell cell)
        {
            double result;
            if (cell.Value.GetType() == typeof(double))
                result = cell.Value;
            else if (!double.TryParse(cell, out result))
                result = -1;
            return result;
        }
        public static explicit operator DateTime(Cell cell)
        {
            DateTime dt;
            if (cell.Value.GetType() == typeof(DateTime))
                dt = cell.Value;
            else if (!DateTime.TryParse(cell, out dt))
                dt = DateTime.MinValue;

            return dt;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
