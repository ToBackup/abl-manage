using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{
    public interface IEDSett
    {
        string Read(string key);
        bool Write(string key, string value);

        string[] Reads(string contains);
    }
}
