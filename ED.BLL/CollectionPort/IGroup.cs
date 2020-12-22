using ED.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CollectionPort
{
    interface IGroup
    {
        LinkedList<string> Samples { get; }
        Key FirstKey { get; }
        Key LastKey { get; }
        Dictionary<Key,LinkedListNode<CqCollection>> Buoy { get;}
        LinkedList<CqCollection> Lists { get;}
        Key GetKey(Record cq);
        bool HasKey(Key key);
        Key AddKey(Key key);
        void InsertCq(Record cq,Key key);
    }
}
