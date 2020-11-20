using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ED.DbRely.Model
{
    public class AddedArgs
    {
        public AddedArgs() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="count">参数个数</param>
        public AddedArgs(int count)
        {
            Param = new DbParams[count];
        }
        public AddedArgs(DbParams[] param)
        {
            this.Param = param;
        }

        public DbParams this[int index] => index >= 0 && index < Param?.Length ? Param[index] : null;
        public DbParams[] Param { get; set; }
        public int Length => Param?.Length ?? 0;
    }
}
