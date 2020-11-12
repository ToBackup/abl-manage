using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{
    public delegate void ProgressOfDelegate(double pro, string tag);
    public interface IProgress
    {
        int Maximum { get; }
        double Progress { get;}
        event ProgressOfDelegate ProgressEvent;

        void SetProgress(int start, int maximum);
    }
}
