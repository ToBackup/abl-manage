using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ED.DbRely;
using ED.SQLite;
//using ED.ExcRely;
//using ED.ExcRely.Model;
using ED.Core;
using ED.Logic;
using ED.Logic.Achieve;
using ED.DbRely.Model;
using System.Data;

namespace ED.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string xls = @"E:\Den_2020-09-14\Den\App\数据2020-8-31 174255\2020.08.30-3(E008-E009后4对，纯化，不纯化，1-10，无破碎）\2020.08.30-3结果分析.xls";
            string xlsx = @"E:\Den_2020-09-14\Den\App\QPCR结果 - 1\2019.07.10\2019.7.10-上午-Quantification Cq Results.xlsx";
            string dic = @"E:\100-项目代码箱\110-进行中项目\QPCR数据\仪器Excel";//@"E:\Den_2020-09-14\Den\App\数据2020-8-31 174255";
            IEDSett set = new DbSett();

            IEDLoad load = EDInstance.GetLoad("Device");
            load.ProgressEvent += Load_ProgressEvent;
            load.Load(dic);

            Console.ReadKey();
        }

        private static void Load_ProgressEvent(double pro)
        {
            Console.WriteLine(Math.Round(pro)+"%");
        }
    }

    public class Config
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    
}
