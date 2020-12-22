using ED.BLL.CalculatePort;
using ED.BLL.CollectionPort;
using ED.BLL.Model;
using ED.Core;
using ED.ExcRely;
using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LLNCC = System.Collections.Generic.LinkedListNode<ED.BLL.CollectionPort.CqCollection>;

namespace ED.BLL.ExcelPort
{
    abstract class OutPutBase : ProgressAbstract, IOutPut
    {
        public override string Tag { get; set; } = "输出到 Excel";
        protected abstract IExcWriter Writer { get; }
        public abstract void ToExcel(IGroup group, IExcWriter writer, ResultCollection result);
        /// <summary>
        /// 写入标题
        /// </summary>
        /// <param name="samples">Sample集合</param>
        /// <param name="row">行索引</param>
        /// <returns>Sample 所在列索引</returns>
        protected abstract Dictionary<string, int> WriteHeader(LinkedList<string> samples, int row);

        protected virtual void WriteRow(int row, Dictionary<string, int> samples, LLNCC cqsNode, ResultCollection result)
        {
            CqCollection cqs = cqsNode.Value;

            foreach (KeyValuePair<string, int> sp in samples)
            {
                Write(sp.Key, row, sp.Value);

                Record cq = cqs[sp.Key];
                if (cq != null)
                {

                    if (cq.SampleKey != cq.SampleName)
                        Write(cq.SampleName, row, sp.Value);

                    Write(cq.Value, row, sp.Value + 1);
                }

                ResultSetI rsi = new ResultSetI(cqs.Key, sp.Key);
                string key = rsi.Key;
                
                if (result!=null && result.Results.ContainsKey(rsi.Key))
                {
                    rsi = result.Results[rsi.Key] as ResultSetI;

                    if (rsi.Node == cqsNode)
                    {
                        int col = sp.Value + 2;
                        for (int x = 0; x < rsi.X; x++)
                        {
                            for (int y = 0; y < rsi.Y; y++)
                            {
                                double value = rsi.Result[x, y];
                                if (value > 0)
                                    Write(value, row + y, col + x);
                            }
                        }
                    }
                }
            }
        }
        protected virtual void Write(dynamic v, int r, int c)
        {
            Cell cell = new Cell(v, r, c++);
            Writer.Write(cell);
        }
    }
}
