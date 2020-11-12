using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ED.Core
{

    public abstract class ProgressAbstract : IProgress
    {
        private double _progress;
        public int Maximum { get; set; } = 100;

        public abstract string Tag { get; set; }

        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value > Maximum ? Maximum : value < _progress ? _progress : value;
                ProgressEvent?.Invoke(_progress, Tag);
            }

        }
        public event ProgressOfDelegate ProgressEvent;

        public void SetProgress(int start, int maximum)
        {
            Maximum = maximum;
            Progress = start;
        }
    }

}
