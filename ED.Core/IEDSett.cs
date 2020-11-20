using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{
    public interface IEDSett
    {
        string Read(string key);
        void Write(string key, string value);
    }
}
