using ED.BLL.Model;
using ED.Core;
using ED.DbRely;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.Load
{
    class LoadI : LoadBase
    {
        public LoadI(Port port) : base(port)
        {
        }

        protected override void InsertToCollection(int minPro, int maxPro, string[] paths)
        {
            if(JoinDb)
            {
                using (IDbQuery query = DbIns.GetQuery("Cq"))
                {
                    query.Open();
                    List<Record> cqs = query.SelectToList<Record>(null);

                    SetProgress(0, 40);
                    Tag = "读数据库";
                    double single = 40d / cqs.Count;
                    foreach (Record cq in cqs)
                    {
                        CqHelper.InsertToCollection(cq, Group);
                        Progress += single;
                    }
                }
                base.InsertToCollection(40, maxPro, paths);
            }
            else
            {
                base.InsertToCollection(0, maxPro, paths);
            }

        }

    }
}
