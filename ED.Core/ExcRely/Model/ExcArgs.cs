using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ED.ExcRely.Model
{
    public class ExcArgs : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [DefaultSettingValue("ED.NPOI.dll")]
        public string DllName { get => (string)this["DLLName"]; set => this["DLLName"] = value; }

        [UserScopedSetting]
        [DefaultSettingValue("ED.NPOI.Export")]
        public string ClassName { get => (string)this["ClassName"]; set => this["ClassName"] = value; }

        [UserScopedSetting]
        public string[] Args { get => (string[])this["Args"]; set => this["Args"] = value; }
    }
}
