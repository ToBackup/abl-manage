using ED.BLL.CalculatePort;
using ED.BLL.CollectionPort;
using ED.BLL.Model;
using ED.ExcRely;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LLNCC = System.Collections.Generic.LinkedListNode<ED.BLL.CollectionPort.CqCollection>;

namespace ED.BLL.ExcelPort
{
    class OutPutII : OutPutBase
    {
        private IExcWriter _writer;
        int rep = 1;

        protected override IExcWriter Writer => _writer;

        public override void ToExcel(IGroup group, IExcWriter writer, ResultCollection result)
        {
            _writer = writer;
            double single = (double)(Maximum - Progress) / group.Lists.Count;
            _writer.Open();
            int row = 0;
            Dictionary<string, int> spIndex = WriteHeader(group.Samples, row++);

            LLNCC node = group.Lists.First;
            while(node !=null)
            {
                CqCollection cqs = node.Value;
                LLNCC next = node.Next;

                WriteRow(row++, spIndex, node, result);
                if (next !=null)
                {
                    int mark = cqs.Key.CompareTo(next.Value.Key);

                    switch (mark)
                    {
                        case 1:
                        case -1:
                            rep = 1;
                            break;
                        default:
                            rep++;
                            break;
                    }
                }

                node = node.Next;
                Progress += single;
            }

            _writer.Save();
            _writer.Close();
        }

        protected override Dictionary<string, int> WriteHeader(LinkedList<string> samples, int row)
        {
            Dictionary<string, int> spIndex = new Dictionary<string, int>();
            int col = 0;
            Write("pcr", row, col++);
            Write("sample", row, col++);
            Write("object", row, col++);
            Write("sampleType", row, col++);
            Write("rep", row, col++);
            Write("age", row, col++);
            Write("in_conc", row, col++);
            Write("result", row, col++);
            Write("sonicate", row, col++);

            foreach (string sp in samples)
            {
                spIndex.Add(sp, col);
                Write(sp, row, col++);

            }

            return spIndex;
        }
        protected override void WriteRow(int row, Dictionary<string, int> samples, LinkedListNode<CqCollection> cqsNode, ResultCollection result)
        {
            CqCollection cqs = cqsNode.Value;
            Record r = cqs[cqs.SamplesExist[0]];

            Write(r.TargetName, row, 1);
            Write(rep, row, 4);

            foreach (Record item in cqs.Records)
            {
                Write(r.Value, row, samples[item.SampleName]);
            }
        }

    }
}
