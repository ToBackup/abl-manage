using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CalculatePort
{
    abstract class ResultSet
    {
        public abstract int X { get; }
        public abstract int Y { get; }
        public abstract string Key { get; }
        public double[,] Result { get; set; }
    }
}
