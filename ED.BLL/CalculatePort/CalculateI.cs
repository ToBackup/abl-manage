using ED.BLL.CollectionPort;
using ED.BLL.Model;
using ED.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LLNCC = System.Collections.Generic.LinkedListNode<ED.BLL.CollectionPort.CqCollection>;

namespace ED.BLL.CalculatePort
{
    class CalculateI :ProgressAbstract, ICalculation
    {
        private Action<string, LLNCC, LLNCC,ResultCollection> BranchResult;

        public override string Tag { get; set; } = "计算";

        public CalculateI()
        {
            BranchResult += BranchResultI;
        }

        private void BranchResultI(string sp,LLNCC sNode,LLNCC eNode,ResultCollection rc)
        {
            int count = 0;
            ResultSetI result = new ResultSetI(sNode.Value.Key, sp);
            double sum = 0;
            
            while (sNode != null && sNode != eNode.Next)
            {
                CqCollection cc = sNode.Value;
                if (cc.SamplesExist.Contains(sp))
                {
                    sum += cc[sp];
                    result.Node = sNode;
                    count++;
                }
                else
                    break;

                sNode = sNode.Next;
            }

            if (sum != 0)
            {
                double avg = sum / count;
                result.Result[0, 0] = avg;

                rc.SetResult(result);
            }
        }

        public ResultCollection ToCalculate(IGroup group)
        {
            ResultCollection rc = new ResultCollection();
            LLNCC node = group.Lists.Last;

            LLNCC fNode = node, nNode=node, bNode=node;

            double single = (double)(Maximum-Progress) / group.Lists.Count;
            while(node !=null)
            {
                LLNCC preNode = node.Previous;
                if (preNode == null)
                    BranchToDeal(node,ref bNode, group.Samples, rc);
                else
                {
                    CqCollection preCqs = preNode.Value;

                    CqCollection cqs = node.Value;
                    int mark = cqs.Key.CompareTo(preCqs.Key);

                    string name = cqs[cqs.SamplesExist[0]].TargetName;

                    switch (mark)
                    {
                        case 3:
                        case -3: //Branch不同
                            BranchToDeal(node,ref bNode, group.Samples, rc);
                            break;
                        case 2:
                        case -2: //FID不同
                            fNode = preNode;
                            BranchToDeal(node,ref bNode, group.Samples, rc);
                            break;
                        case 1:
                        case -1: //Name不同
                            fNode = preNode;
                            BranchToDeal(node,ref bNode, group.Samples, rc);
                            nNode = preNode;
                            break;
                        case 0: //相同
                            break;
                    }
                }
                node = node.Previous;

                Progress += single;
            }

            return rc;
        }


        private void BranchToDeal(LLNCC sNode, ref LLNCC eNode, LinkedList<string> sps, ResultCollection rc)
        {
            foreach (string sp in sps)
            {
                BranchResult?.Invoke(sp, sNode, eNode,rc);
            }

            eNode = sNode.Previous;
        }
    }
}
