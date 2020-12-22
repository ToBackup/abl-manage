using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace ED.BLL.CollectionPort
{
    class Key : IComparable<Key>
    {
        public Key Previous { get; set; }

        public Key Next { get; set; }
        public string LevelI { get; set; }
        public string LevelII { get; set; }
        public string LevelIII { get; set; }

        public int CompareTo(Key other)
        {
            //Type type = this.GetType();
            //PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //foreach (PropertyInfo prop in props)
            //{
            //    if(prop.Name.Contains("Level"))
            //    {
            //        string v1 = Convert.ToString(prop.GetValue(this, null));
            //        string v2 = Convert.ToString(prop.GetValue(other, null));

            //        if (v1.CompareTo(v2) > 0)
            //            return 1;
            //        else if (v1.CompareTo(v2) < 0)
            //            return -1;
            //    }
            //}
            //return 0;

            //一级判断
            if (LevelI?.CompareTo(other.LevelI) > 0)
                return 1;
            else if (LevelI?.CompareTo(other.LevelI) < 0)
                return -1;
            //二级判断
            if (LevelII?.CompareTo(other.LevelII) > 0)
                return 2;
            else if (LevelII?.CompareTo(other.LevelII) < 0)
                return -2;

            //三级判断
            if (LevelIII?.CompareTo(other.LevelIII) > 0)
                return 3;
            else if (LevelIII?.CompareTo(other.LevelIII) < 0)
                return -3;
            else
                return 0;
        }
        public override bool Equals(object obj)
        {
            if (obj is Key)
            {
                Key key = (Key)obj;
                return this.CompareTo(key) == 0;
            }
            return false;

        }
        public override int GetHashCode()
        {
            int number = 0;
            //string str = ToString();
            //byte[] buffer = Encoding.UTF8.GetBytes(str);

            //foreach (var item in buffer)
            //{
            //    number += item;
            //}

            Type type = this.GetType();
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.Contains("Level"))
                {
                    string value = prop.GetValue(this, null)?.ToString();

                    char[] c = value?.ToCharArray();

                    for (int i = 0; i < c?.Length; i += 2)
                    {
                        string x2 = string.Format("{0}{1}", c[i], c[i + 1]);
                        number = int.Parse(x2, System.Globalization.NumberStyles.HexNumber);
                    }
                }
            }

            return number;
        }
        public override string ToString()
        {
            string str = string.Format("{0}_{1}_{2}", LevelI, LevelII, LevelIII);
            return str;
        }

    }
}
