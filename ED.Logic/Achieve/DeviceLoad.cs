using ED.Core;
using ED.ExcRely;
using ED.ExcRely.Model;
using ED.Logic.Basis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ED.Logic.Achieve
{
    public class DeviceLoad : IEDLoad
    {
        private double progress;
        private int maxPro;
        public double Progress 
        { 
            get => progress; 
            private set
            {
                progress = value > maxPro ? maxPro : value < 0 ? 0 : value;
                ProgressEvent?.Invoke(progress);
            }
        }

        public event ProgressOfDelegate ProgressEvent;

        public void Load(string path,int maxPro)
        {
            this.maxPro = maxPro;

            if(Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                ReadDir(dir, maxPro);
            }
            else if (File.Exists(path))
            {
                ReadFile(path);
            }

        }

        private void ReadDir(DirectoryInfo root,double maxPro)
        {
            DirectoryInfo[] dirs = root.GetDirectories();

            double single = maxPro / (dirs.Length + 1);
            Progress += single;

            FileInfo[] files = root.GetFiles("*Cq Results*.xls*");
            foreach (FileInfo file in files)
            {
                if(!file.FullName.Contains("~$"))
                ReadFile(file.FullName);
            }

            foreach (DirectoryInfo dir in dirs)
            {
                ReadDir(dir,single);
            }

        }

        private void ReadFile(string fileName)
        {
            using (IExcReader reader = ExcInstance.GetReader())
            {
                reader.Open(fileName);

                int[] vs = null;
                string[] tags = new string[] { "Target", "Sample", "Cq" };

                ICellHelper ch = new DeviceCell(fileName);//null;
                while (reader.Read())
                {

                    //找标记列
                    if (vs == null)
                        ch?.FindTags(tags, reader.Columns, out vs);
                    else
                    {
                        Dictionary<string, Cell> dCells = new Dictionary<string, Cell>();

                        for (int i = 0; i < tags.Length; i++)
                        {
                            Cell cell = reader[vs[i]];
                            dCells.Add(tags[i], cell);
                        }

                        ch?.ByDictionary(dCells);
                    }
                    
                    //Thread.Sleep(100);
                }

            }

        }
    }
}
