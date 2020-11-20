using ED.ExcRely;
using ED.NPOI.Achieve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.NPOI
{
    public class Export : IExcInstance
    {
        public IExcReader GetReader()
        {
            IExcReader reader = new Reader();
            return reader;
        }

        public IExcWriter GetWriter()
        {
            IExcWriter writer = new Writer();
            return writer;
        }
    }
}
