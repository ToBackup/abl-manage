using ED.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CollectionPort
{
    class GroupII : GroupBase
    {
        public override Key GetKey(Record cq)
        {
            Key key = new Key();

            byte[] buffer = Encoding.UTF8.GetBytes(cq.TargetName);
            foreach (byte b in buffer)
            {
                key.LevelI += b.ToString("X2");
            }

            return key;
        }

    }
}
