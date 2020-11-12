using ED.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ED.BLL
{
    class Each : ProgressAbstract
    {
        public string Pattern { get; set; } = "*Cq Results*.xls*";
        public override string Tag { get; set; } = "读取文件";


        public event Action<FileInfo> FileHandleEvent;
        void ToDir(DirectoryInfo root,double maximum)
        {
            FileInfo[] files = root.GetFiles(Pattern, SearchOption.AllDirectories);
            double single = maximum / files.Length;

            foreach (FileInfo file in files)
            {
                Progress += single;
                FileHandleEvent?.Invoke(file);
            }
        }

        public void DirsEach(DirectoryInfo[] dirs)
        {
            double single = (double)(Maximum - Progress) / dirs.Length;
            foreach (DirectoryInfo dir in dirs)
            {
                ToDir(dir, single);
            }
        }
    }
}
