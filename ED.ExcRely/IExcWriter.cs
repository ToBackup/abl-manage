using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.ExcRely
{
    public interface IExcWriter : IExcStream
    {
        bool Write(Cell cell);
        bool Save(string newPath = null);
    }
}
