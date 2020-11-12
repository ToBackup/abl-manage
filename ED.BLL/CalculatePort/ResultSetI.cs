using ED.BLL.CollectionPort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using LLNCC = System.Collections.Generic.LinkedListNode<ED.BLL.CollectionPort.CqCollection>;

namespace ED.BLL.CalculatePort
{
    class ResultSetI : ResultSet
    {
        private string _key;
        private LLNCC _node;
        private Key cqsKey;
        private string _sample;
        public override string Key => string.IsNullOrEmpty(_key)?GetKey():_key;

        public LLNCC Node
        {
            get => _node;
            set
            {
                _node = value;
            }
        }

        public override int X => 2;

        public override int Y => 1;

        public ResultSetI(Key key,string sample)
        {
            Result = new double[2, 1];
            _sample = sample;
            cqsKey = key;
            GetKey();
        }

        private string GetKey()
        {
            MD5 md5 = MD5.Create();
            _key = string.Empty;
            byte[] buffer = Encoding.UTF8.GetBytes(cqsKey.ToString() + "_" + _sample);
            buffer = md5.ComputeHash(buffer);

            foreach (byte item in buffer)
            {
                _key += item.ToString("X2");
            }

            return _key;

        }
    }
}
