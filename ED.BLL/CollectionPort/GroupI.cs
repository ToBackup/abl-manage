using ED.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using LLNCC = System.Collections.Generic.LinkedListNode<ED.BLL.CollectionPort.CqCollection>;

namespace ED.BLL.CollectionPort
{
    class GroupI : GroupBase
    {
        public override Key GetKey(Record cq)
        {
            Key key = new Key();

            string tagName = cq.TargetKey;
            string tagBranch = cq.TargetName;
            string fileDate = cq.Date.ToString("yyyy-MM-dd");

            byte[] buffer = Encoding.UTF8.GetBytes(tagName);
            foreach (byte buf in buffer)
            {
                key.LevelI += buf.ToString("X2");
            }
            buffer = Encoding.UTF8.GetBytes(fileDate);
            foreach (byte buf in fileDate)
            {
                key.LevelII += buf.ToString("X2");
            }
            buffer = Encoding.UTF8.GetBytes(tagBranch);
            foreach (var item in buffer)
            {
                key.LevelIII += item.ToString("X2");
            }
            Key isHas = Buoy.Keys.FirstOrDefault(k => { return k.Equals(key); });
            if (isHas == null)
                return key;
            else
                return isHas;
        }

    }
}
