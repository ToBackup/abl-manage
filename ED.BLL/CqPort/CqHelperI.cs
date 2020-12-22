using ED.BLL.CollectionPort;
using ED.BLL.Model;
using ED.BLL.VerifyPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CqPort
{
    class CqHelperI : CqHelperBase
    {
        public CqHelperI(IExtractValidation evTarget,IExtractValidation evSample) : base(evTarget, evSample) { }

        public override Record ConvertToCq(CellArgs args)
        {
            Record cq = null;
            string strTarget = args["Target"];
            string strSample = args["Sample"];
            string strCq = args["Cq"];

            string tagName, spName;
            //Target 是否满足要求并截取 Target 编号
            tagName = TagEV.IsMeet(strTarget) ? TagEV.Extract(strTarget) : null;
            bool tnok = !string.IsNullOrEmpty(tagName);

            //Sample 是否满足要求并截取 Sample 编号
            spName = SpEV.IsMeet(strSample) ? SpEV.Extract(strSample) : null;
            bool snok = !string.IsNullOrEmpty(spName);
            // Cq 是否满足要求
            double dCq = 0;
            bool cqok = !string.IsNullOrEmpty(strCq) && double.TryParse(strCq, out dCq) && dCq > 0;

            if(tnok && snok && cqok)
            {
                cq = new Record()
                {
                    TargetKey = tagName,
                    TargetName = strTarget,
                    SampleKey = spName,
                    SampleName = strSample,
                    Value = dCq,
                    FID = args.File.FID,
                    Date = args.File.Date,
                    File = args.File.FileName
                };
            }

            return cq;
        }

    }
}
