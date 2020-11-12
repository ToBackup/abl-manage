using ED.BLL.CollectionPort;
using ED.BLL.Model;
using ED.BLL.VerifyPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CqPort
{
    class CqByI : ICqHelper
    {
        private IExtractValidation tagEV;
        private IExtractValidation spEV;



        public CqByI(IExtractValidation evTarget,IExtractValidation evSample)
        {
            tagEV = evTarget;
            spEV = evSample;
        }

        public Cq ConvertToCq(CellArgs args)
        {
            Cq cq = null;
            string strTarget = args["Target"];
            string strSample = args["Sample"];
            string strCq = args["Cq"];//Convert.ToDouble();

            string tagName, spName;
            //Target 是否满足要求并截取 Target 编号
            tagName = tagEV.IsMeet(strTarget) ? tagEV.Extract(strTarget) : null;
            bool tnok = !string.IsNullOrEmpty(tagName);

            spEV = new SampleValidation();
            //Sample 是否满足要求并截取 Sample 编号
            spName = spEV.IsMeet(strSample) ? spEV.Extract(strSample) : null;
            bool snok = !string.IsNullOrEmpty(spName);
            // Cq 是否满足要求
            double dCq = 0;
            bool cqok = !string.IsNullOrEmpty(strCq) && double.TryParse(strCq, out dCq) && dCq > 0;

            if(tnok && snok && cqok)
            {
                Target target = new Target()
                {
                    Name = tagName,
                    Branch = strTarget
                };

                Sample sample = new Sample()
                {
                    Name = spName,
                    Branch = strSample
                };

                cq = new Cq()
                {
                    File = args.File,
                    Target = target,
                    Sample = sample,
                    Value = dCq
                };
            }

            return cq;
        }

        public void InsertToCollection(Cq cq, IGroup clt)
        {
            Key key = clt.GetKey(cq);

            if (!clt.HasKey(key))
                key = clt.AddKey(key);

            clt.InsertCq(cq,key);

        }
    }
}
