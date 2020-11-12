using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{

    /// <summary>
    /// 加载 Excel 文件
    /// </summary>
    public interface IEDLoad
    {
        event ProgressOfDelegate ProgressEvent;
        void Load(int maxPro = 100, params string[] paths);

    }
}
