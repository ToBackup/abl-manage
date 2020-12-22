using ED.BLL.CalculatePort;
using ED.BLL.Model;
using ED.DbRely;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Load
{
    class LoadII : LoadBase
    {
        public LoadII(Port port) : base(port)
        {
        }

        protected override ResultCollection GetResult(int minPro, int maxPro)
        {
            return null;
        }
        protected override void InsertToCollection(int minPro, int maxPro, string[] paths)
        {
            if (JoinDb)
            {
                using (IDbQuery query = DbIns.GetQuery("Cq"))
                {
                    query.Open();
                    List<Record> cqs = query.SelectToList<Record>(null);

                    SetProgress(0, 10);
                    Tag = "读数据库";
                    double single = 10d / cqs.Count;
                    foreach (Record cq in cqs)
                    {
                        CqHelper.InsertToCollection(cq, Group);
                        Progress += single;
                    }
                }
            }
            base.InsertToCollection(minPro, maxPro, paths);
        }
    }
}
