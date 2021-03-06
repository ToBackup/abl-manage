﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ED.DbRely;
//using ED.ExcRely;
//using ED.ExcRely.Model;
using ED.Core;
//using ED.Logic;
//using ED.Logic.Achieve;
using ED.DbRely.Model;
using System.Data;
using System.Configuration;
using System.Drawing;
using ED.BLL.Model;
//using ED.Logic.Model.Excel;

namespace ED.CLI
{
    class Program
    {
        //static T t1 = new T("");
        static void Main(string[] args)
        {
            //string xls = @"E:\Den_2020-09-14\Den\App\数据2020-8-31 174255\2020.08.30-3(E008-E009后4对，纯化，不纯化，1-10，无破碎）\2020.08.30-3结果分析.xls";
            //string xlsx = @"E:\Den_2020-09-14\Den\App\QPCR结果 - 1\2019.07.10\2019.7.10-上午-Quantification Cq Results.xlsx";
            string dic = null;
            dic = args.Length > 0 ? args[0] : null;

            //dic = @"E:\100-项目代码箱\110-进行中项目\QPCR数据\仪器Excel";
            dic = @"E:\Den_2020-09-14\Den\App\仪器Excel";
            //dic = @"E:\Den_2020-09-14\Den\App\数据2020-8-31 174255";
            //dic = @"E:\Den_2020-09-14\Den\App\数据2020-8-31 174255\2020.08.30-2(E008中间7对，纯化，不纯化，1-10，无破碎）";

            IEDInstance ins = new BLL.Export();

            IEDSett set = ins.GetEDSett();
            string t = Convert.ToString(true);
            set.Write("JoinDb", t);

            DateTime dtStart = DateTime.Now;
            IEDLoad load = ins.GetEDLoad("I");
            load.ProgressEvent += Load_ProgressEvent;
            load.Load(100, dic);
            DateTime dtEnd = DateTime.Now;
            Console.WriteLine((dtEnd - dtStart).TotalMilliseconds);

            SQLite.Export exp = new SQLite.Export();

            Console.ReadKey(); 
        }

        private static void Load_ProgressEvent(double pro,string tag)
        {
            Console.CursorLeft = 0;
            Console.CursorTop -= Console.CursorTop > 0 ? 1 : 0;
            Console.WriteLine(string.Format("{0} - {1}中", Math.Round(pro) + "%", tag));
        }
    }
}
