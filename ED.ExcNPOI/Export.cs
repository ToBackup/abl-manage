using ED.ExcRely;
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
            Type type = Type.GetType("ED.NPOI.Achieve.Reader");
            IExcReader reader = Activator.CreateInstance(type) as IExcReader;

            return reader;
        }

        public IExcWriter GetWriter()
        {
            Type type = Type.GetType("ED.NPOI.Achieve.Writer");
            IExcWriter writer = Activator.CreateInstance(type) as IExcWriter;

            return writer;
        }
    }
}
