using ED.BLL.CollectionPort;
using ED.BLL.Model;
using ED.BLL.VerifyPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CqPort
{
    abstract class CqHelperBase : ICqHelper
    {
        protected IExtractValidation TagEV { get; }
        protected IExtractValidation SpEV { get;}
        public CqHelperBase(IExtractValidation evTarget, IExtractValidation evSample)
        {
            TagEV = evTarget;
            SpEV = evSample;
        }

        public abstract Record ConvertToCq(CellArgs args);

        public virtual void InsertToCollection(Record cq, IGroup clt)
        {
            Key key = clt.GetKey(cq);

            if (!clt.HasKey(key))
                key = clt.AddKey(key);

            clt.InsertCq(cq, key);

        }
    }
}
