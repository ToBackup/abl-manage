using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{
    public interface IComponent
    {
        void Set(ComponentsEnumerat ce, string dll, string cls, params string[] args);
        void Look(ComponentsEnumerat ce, ref string dll, ref string cls, ref string[] args);
    }
    public enum ComponentsEnumerat
    {
        DataBase,
        Excel
    }
}
