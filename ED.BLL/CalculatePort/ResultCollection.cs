using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using LLNCC = System.Collections.Generic.LinkedListNode<ED.BLL.CollectionPort.CqCollection>;

namespace ED.BLL.CalculatePort
{
    class ResultCollection
    {
        public Dictionary<string, ResultSet> Results { get; set; } = new Dictionary<string, ResultSet>();


        public void SetResult(ResultSet result)
        {
            if (Results.ContainsKey(result.Key))
            {
                ResultSet old = Results[result.Key];

                if (old.X == result.X && old.Y == result.Y)
                {
                    for (int x = 0; x < old.X; x++)
                    {
                        for (int y = 0; y < old.Y; y++)
                        {
                            double r = old.Result[x, y];
                            if (r == 0)
                                old.Result[x, y] = result.Result[x, y];
                        }
                    }
                }
                else
                    throw new Exception("出错");
            }
            else
                Results.Add(result.Key, result);
        }
    }
}
