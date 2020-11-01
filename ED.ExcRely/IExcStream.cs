using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.ExcRely
{
    public interface IExcStream : IDisposable
    {
        bool Open(string path);
        void Close();
    }
}
