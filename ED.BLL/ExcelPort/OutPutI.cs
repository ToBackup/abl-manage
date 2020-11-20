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
    class OutPutI : ProgressAbstract, IOutPut
    {
        IExcWriter writer;

        public override string Tag { get; set; } = "输出到 Excel";

        public void ToExcel(IGroup group,IExcWriter writer, ResultCollection result)
        {
            this.writer = writer;
            double single = (double)(Maximum-Progress) / group.Lists.Count;
            
            LLNCC node = group.Lists.First;
            writer.Open();

            int row = 0;
            Dictionary<string, int> kv = WriteHeader( group.Samples, ref row);
            while (node != null)
            {
                CqCollection cqs = node.Value;

                WriteCol( row, kv, node, result);

                LLNCC next = node.Next;
                if (next != null)
                {
                    int mark = cqs.Key.CompareTo(next.Value.Key);
                    switch (mark)
                    {
                        case 3:
                        case -3:
                            break;
                        case 2:
                        case -2:
                            break;
                        case 1:
                        case -1:
                            //创建表头
                            row += 7;
                            WriteHeader( group.Samples, ref row);
                            row--;
                            break;
                        case 0:
                            break;
                    }
                }
                row++;
                node = node.Next;

                Progress += single;
            }

            writer.Save();
            writer.Close();
        }

        private Dictionary<string,int> WriteHeader(LinkedList<string> samples, ref int row)
        {
            Dictionary<string, int> spKV = new Dictionary<string, int>();
            int col = 0;
            Write("来源及姓名", row,col++);
            Write("日期", row, col++);
            Write("破碎", row, col++);
            Write("Target", row, col++);
            Write("诊断结果", row, col++);

            foreach (string sp in samples)
            {
                spKV.Add(sp, col);
                Write( "Sample", row, col++);
                Write( "Cq", row, col++);
                col += 2;
            }

            row++;
            return spKV;
        }
        private void WriteCol(int row, Dictionary<string,int> samples, LLNCC cqsNode, ResultCollection result)
        {
            CqCollection cqs = cqsNode.Value;
            Cq first = cqs[cqs.SamplesExist[0]];

            Write(first.Target.Branch, row, 3);
            Write( first.File.Date.ToString("yyyy-MM-dd"), row, 1);


            foreach (KeyValuePair<string,int> sp in samples)
            {
                Write( sp.Key, row, sp.Value);

                Cq cq = cqs[sp.Key];
                if (cq!=null)
                {

                    if(cq.Sample.Name !=cq.Sample.Branch)
                        Write( cq.Sample.Branch, row, sp.Value);

                    Write( cq.Value, row, sp.Value + 1);
                }

                ResultSetI rsi = new ResultSetI(cqs.Key, sp.Key);
                string key = rsi.Key;
                if (result.Results.ContainsKey(rsi.Key))
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
                                if(value>0)
                                    Write( value, row + y, col + x);
                            }
                        }
                    }
                }
            }
        }

        private void Write(dynamic v,int r,int c)
        {
            Cell cell = new Cell(v, r, c++);
            writer.Write(cell);
        }
    }
}
