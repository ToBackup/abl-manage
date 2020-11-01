using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{

    public delegate void ProgressOfDelegate(double pro);
    /// <summary>
    /// 加载 Excel 文件
    /// </summary>
    public interface IEDLoad
    {
        double Progress { get; }
        event ProgressOfDelegate ProgressEvent;
        void Load(string path,int maxPro = 100);

    }
}
