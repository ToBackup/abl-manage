using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ED.DbRely.Model
{
    public class DbArgs : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [DefaultSettingValue("ED.SQLite.dll")]
        public string DllName { get => (string)this["DLLName"]; set => this["DLLName"] = value; }

        [UserScopedSetting]
        [DefaultSettingValue("ED.SQLite.Export")]
        public string ClassName { get => (string)this["ClassName"]; set => this["ClassName"] = value; }

        [UserScopedSetting]
        public string[] Args { get => (string[])this["Args"]; set => this["Args"] = value; }

    }
}
