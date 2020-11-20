using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ED.Core.Model
{
    public class Setts : ApplicationSettingsBase
    {
        private Dictionary<string, string> dics;

        public Setts()
        {
            dics = ToDics(Configs);
        }
        public new string this[string key] => dics.ContainsKey(key) ? dics[key] : null;
        public void Set(string key, string value)
        {
            if (!dics.ContainsKey(key))
                dics.Add(key, value);
            else
                dics[key] = value;


            base["Configs"] = ToList(dics);
            this.Save();
        }

        [UserScopedSetting]
        public Config[] Configs
        {
            get => (Config[])base["Configs"];
        }

        private Config[] ToList(Dictionary<string, string> dics)
        {
            List<Config> list = new List<Config>();
            if (dics != null)
                foreach (KeyValuePair<string, string> item in dics)
                {
                    list.Add(new Config()
                    {
                        Key = item.Key,
                        Value = item.Value
                    });
                }
            return list.ToArray();
        }
        private Dictionary<string, string> ToDics(Config[] configs)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();

            if (configs != null)
                foreach (Config item in configs)
                {
                    dics.Add(item.Key, item.Value);
                }
            return dics;
        }
    }
}
