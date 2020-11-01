using ED.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace ED.Logic.Achieve
{
    public class XmlSett : IEDSett
    {
        public string Read(string key)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string value = cfa.AppSettings.Settings[key]?.Value;
            return value;
        }

        public bool Write(string key, string value)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (cfa.AppSettings.Settings.AllKeys.Contains(key))
                cfa.AppSettings.Settings.Remove(key);
            cfa.AppSettings.Settings.Add(key, value);

            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");


            return false;

        }
    }
}
