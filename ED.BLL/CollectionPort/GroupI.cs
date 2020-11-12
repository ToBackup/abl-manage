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
    class GroupI : IGroup
    {
        public LinkedList<string> Samples { get; private set; } = new LinkedList<string>();
        public Dictionary<Key, LLNCC> Buoy { get; private set; } = new Dictionary<Key, LLNCC>();
        public LinkedList<CqCollection> Lists { get; private set; } = new LinkedList<CqCollection>();

        public Key FirstKey { get; private set; }

        public Key LastKey { get; private set; }

        public Key AddKey(Key key)
        {
            //var keys = from kv in Buoy
            //           where kv.Value != null
            //           orderby kv.Key descending
            //           select kv.Key;
            //foreach (var item in keys)
            //{

            //}
            var keys = from kv in Buoy
                       where kv.Key.CompareTo(key) < 0
                       orderby kv.Key descending
                       select kv.Key;
            Key pk = keys.FirstOrDefault();

            if (pk != null)
                pk.Next = key;
            else
                FirstKey = key;
            key.Previous = pk;

            keys = from kv in Buoy
                   where kv.Key.CompareTo(key) > 0
                   orderby kv.Key
                   select kv.Key;
            pk = keys.FirstOrDefault();
            if (pk != null)
                pk.Previous = key;
            else
                LastKey = key;
            key.Next = pk;

            Buoy.Add(key, null);

            return key;
        }
        public Key GetKey(Cq cq)
        {

            Key key = new Key();
            //{
            //    //LevelI = cq.Target.Name,
            //    //LevelIII = cq.Target.Branch,
            //    //LevelII = cq.File.FID
            //};

            string tagName = cq.Target.Name;
            string tagBranch = cq.Target.Branch;
            string fileDate = cq.File.Date.ToString("yyyy-MM-dd");

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

        public bool HasKey(Key key)
        {
            return Buoy.Keys.Any(k => { return k.Equals(key); });
        }

        public void InsertCq(Cq cq, Key key)
        {
            string strSp = cq.Sample.Name;
            //是否需要添加 Sample 到集合中
            if (!Samples.Contains(strSp))
                InsertSample(strSp);


            LLNCC node = Buoy[key];
            //根据 Key 找出 Cq 集合
            CqCollection cqs = null;

            //判断 Buoy 是否有指向
            if (node == null)//Buoy 没指向
            {
                //实例新的集合
                cqs = new CqCollection()
                {
                    Key = key
                };

                cqs.Samples = Samples;
                cqs[strSp] = cq;
                if (key.Previous != null)
                    node = Lists.AddAfter(Buoy[key.Previous], cqs);
                else
                    node = Lists.AddFirst(cqs);
                Buoy[key] = node;
            }
            else//Buoy 有指向
            {
                LLNCC preNode = null;
                if (key.Previous != null)
                    preNode = Buoy[key.Previous].Next;
                else
                    preNode = Lists.First;


                while(preNode !=null && preNode != node.Next)
                {
                    cqs = preNode.Value;
                    if(cqs[strSp] == null)
                    {
                        cqs[strSp] = cq;
                        break;
                    }

                    preNode = preNode.Next;
                }

                if(node.Next == preNode)
                {
                    cqs = new CqCollection()
                    {
                        Key = key
                    };
                    cqs.Samples = Samples;
                    cqs[strSp] = cq;
                    node = Lists.AddAfter(node, cqs);
                    Buoy[key] = node;
                }
            }
        }

        private void InsertSample(string sp)
        {
            LinkedListNode<string> node = Samples.First;
            do
            {
                if (node == null)
                {
                    node = Samples.AddFirst(sp);
                    break;
                }
                else if (node.Value.CompareTo(sp) > 0)
                {
                    node = Samples.AddBefore(node, sp);
                    break;
                }
                node = node.Next;
            } while (node != null);
            if (node == null)
                node = Samples.AddLast(sp);
        }

    }
}
