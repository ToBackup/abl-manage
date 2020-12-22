using ED.BLL.Model;
using ED.BLL.VerifyPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CqPort
{
    class CqHelperII : CqHelperBase
    {
        public CqHelperII(IExtractValidation evTarget, IExtractValidation evSample) : base(evTarget, evSample)
        {
        }

        public override Record ConvertToCq(CellArgs args)
        {
            Record r = null;
            string strTarget = args["Target"];
            string strSample = args["Sample"];
            string strCq = args["Cq"];

            //Target 是否满足要求并截取 Target 编号
            string tagName = TagEV.IsMeet(strTarget) ? TagEV.Extract(strTarget) : null;
            bool tnok = !string.IsNullOrEmpty(tagName);

            bool snok = !string.IsNullOrEmpty(strSample); 
            double dCq = 0;
            bool cqok = !string.IsNullOrEmpty(strCq) && double.TryParse(strCq, out dCq) && dCq > 0;

            if(tnok && snok && cqok)
            {
                r = new Record()
                {
                    TargetKey = tagName,
                    Date = args.File.Date,
                    SampleKey = strSample,
                    SampleName = strSample,
                    FID = args.File.FID,
                    File = args.File.FileName,
                    TargetName = strTarget,
                    Value = dCq
                };
            }
            return r;
        }
    }
}
