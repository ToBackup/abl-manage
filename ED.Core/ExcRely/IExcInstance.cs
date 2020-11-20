using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.ExcRely
{
    public interface IExcInstance
    {
        IExcReader GetReader();
        IExcWriter GetWriter();
    }
}
