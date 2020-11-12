using ED.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.BLL.CollectionPort
{
    class CqCollection
    {
        Dictionary<string, Cq> _cqs;

        public LinkedList<string> Samples { get; set; }
        public List<string> SamplesExist => _cqs.Keys.ToList();
        public Key Key { get; set; }

        public CqCollection()
        {
            _cqs = new Dictionary<string, Cq>();
            Samples = new LinkedList<string>();
        }

        public Cq this[string sp]
        {
            get => Samples.Contains(sp) && _cqs.ContainsKey(sp) ? _cqs[sp] : null;
            set
            {
                if (Samples.Contains(sp))
                    SetCq(value,sp);
            }
        }


        private void SetCq(Cq value,string sp)
        {
            if (!_cqs.ContainsKey(sp))
                _cqs.Add(sp, value);
            else
                _cqs[sp] = value;
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}
