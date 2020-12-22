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
    class OutPutI : OutPutBase
    {
        private IExcWriter _writer;

        protected override IExcWriter Writer => _writer;

        public override void ToExcel(IGroup group,IExcWriter writer, ResultCollection result)
        {
            _writer = writer;
            double single = (double)(Maximum-Progress) / group.Lists.Count;
            
            LLNCC node = group.Lists.First;
            writer.Open();

            int row = 0;
            Dictionary<string, int> kv = WriteHeader( group.Samples, row++);
            while (node != null)
            {
                CqCollection cqs = node.Value;

                WriteRow( row, kv, node, result);

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
                            WriteHeader( group.Samples, row);
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

        protected override Dictionary<string,int> WriteHeader(LinkedList<string> samples, int row)
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

            return spKV;
        }
        protected override void WriteRow(int row, Dictionary<string,int> samples, LLNCC cqsNode, ResultCollection result)
        {
            CqCollection cqs = cqsNode.Value;
            Record first = cqs[cqs.SamplesExist[0]];
            Write(first.TargetName, row, 3);
            Write( first.Date.ToString("yyyy-MM-dd"), row, 1);

            base.WriteRow(row, samples, cqsNode, result);
        }

    }
}
