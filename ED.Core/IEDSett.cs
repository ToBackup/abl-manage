using ED.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{
    public interface IEDSett
    {
        T Read<T>(T tag) where T : SettBase;
        void Write<T>(T set) where T : SettBase;
        string Read(string key);
        void Write(string key, string value);
    }
}
