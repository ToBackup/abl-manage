using ED.Core;
using ED.DbRely;
using ED.DbRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Logic.Achieve
{
    public class DbSett : IEDSett
    {
        public string Read(string key)
        {
            string value = null;
            using (IDbQuery query = DbInstance.GetQuery("Configure"))
            {
                if (query == null)
                    return null;
                query.Open();
                QueryArgs args = new QueryArgs()
                {
                    Rows = "Value",
                    Where = "Key=@Key",
                    Param = new DbParams[]
                    {
                    new DbParams("Key",key)
                    }
                };
                value = (string)query.SelectToValue(args);
            }
                
            return value;
        }

        public bool Write(string key, string value)
        {
            int count = -1;
            using (IDbAlter alter = DbInstance.GetAlter("Configure"))
            {
                if (alter == null)
                    return false;

                alter.Open();
                QueryArgs args = new QueryArgs()
                {
                    Where = "Key=@Key",
                    Param = new DbParams[]
                       {
                    new DbParams("Key", key),
                    new DbParams("Value", value)
                       },
                    Rows = "Count(1)"
                };

                count = Convert.ToInt32(alter.SelectToValue(args));

                if (count > 0)
                    count = alter.Update(args);
                else
                    count = alter.Insert(args);

            }
                
            return count > 0;
        }
    }
}
