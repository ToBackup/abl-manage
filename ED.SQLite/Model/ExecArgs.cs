using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ED.SQLite.Model
{
    class ExecArgs
    {
        public string Text { get; set; }
        public CommandType Type { get; set; }
        public SQLiteParameter[] Param { get; set; }
    }
}
