using ED.BLL.CellPort;
using ED.BLL.CqPort;
using ED.BLL.Model;
using ED.Core;
using ED.DbRely;
using ED.DbRely.Model;
using ED.ExcRely;
using ED.ExcRely.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using MF = ED.BLL.Model.File;
using SIF = System.IO.File;
using ED.BLL.VerifyPort;
using ED.BLL.CollectionPort;
using ED.BLL.CalculatePort;
using ED.BLL.ExcelPort;
using ED.BLL.Sett;

namespace ED.BLL.Load
{
    class LoadI :IEDLoad
    {
        bool joinDb;
        ICellHelper cc = null;  //单元格接口
        ICqHelper cqGrouper = null; //Cq接口
        IExtractValidation evFile/*, evTarget, evSample*/; //验证接口
        IGroup clt; //分组集合接口
        ICalculation ccl = null; //计算接口
        IOutPut output = null; //输出接口
        IExcInstance excIns = null;
        IDbInstance dbIns = null;
        IEDSett set = null;

        public LoadI(Port port)
        {

            joinDb = Convert.ToBoolean(port.Set.Read("JoinDb"));

            cc = port.cc;
            evFile = port.evFile;
            IExtractValidation evTarget = port.evTarget;
            IExtractValidation evSample = port.evSample;
            cqGrouper = port.cqGrouper;
            clt = port.clt;
            ccl = port.ccl;
            output = port.output;
            excIns = port.excIns;
            dbIns = port.dbIns;
            set = port.Set;
        }

        public event ProgressOfDelegate ProgressEvent;

        public void Load(int maxPro = 100, params string[] paths)
        {
            int firstStage = (int)(maxPro * 0.8d);
            int secondStage = (int)(maxPro * 0.9d);

            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            List<FileInfo> files = new List<FileInfo>();
            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                    dirs.Add(new DirectoryInfo(path));
                else if(SIF.Exists(path))
                    files.Add(new FileInfo(path));
            }

            if(dirs.Any())
            {
                Each each = new Each();
                each.SetProgress(0, firstStage);
                each.FileHandleEvent += Each_FileHandleEvent;
                each.ProgressEvent += this.ProgressEvent;

                each.DirsEach(dirs.ToArray());

                each.FileHandleEvent -= Each_FileHandleEvent;
                each.ProgressEvent -= this.ProgressEvent;
            }

            if(files.Any())
            {
                FileEach(files);
            }

            if(joinDb)
            {
                using(IDbQuery query = dbIns.GetQuery("Cq"))
                {
                    List<Cq> cqs = query.SelectToList<Cq>(null);

                    foreach (Cq cq in cqs)
                    {
                        cqGrouper.InsertToCollection(cq, clt);
                    }
                }
            }

            //下面开始从集合中进行计算
            ResultCollection rc = null;
            if (ccl !=null)
            {
                ccl.SetProgress(firstStage, secondStage);
                ccl.ProgressEvent += this.ProgressEvent;
                rc = ccl.ToCalculate(clt);
                ccl.ProgressEvent -= this.ProgressEvent;
            }

            //下面开始打印到 Excel
            
            if(output!=null)
            {
                output.SetProgress(secondStage, maxPro);
                output.ProgressEvent += this.ProgressEvent;
                output.ToExcel(clt,excIns.GetWriter(), rc);
                output.ProgressEvent -= this.ProgressEvent;
            }

        }


        private void FileEach(List<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                Each_FileHandleEvent(file);
            }
        }

        private void Each_FileHandleEvent(FileInfo info)
        {
            //判断文件是否符合要求
            bool meet = true;
            meet = !info.Name.Contains("~$") && (evFile?.IsMeet(info.Name) ?? false);
            
            if (meet)
            {
                //生成MD5
                string md5 = ToGenerateMD5(info);
                MF file = new MF()
                {
                    FID = md5,
                    FileName = info.Name,
                    Date = Convert.ToDateTime(evFile.Extract(info.Name))
                };

                //查询数据库，判断是否已经读取过
                bool readed = false;
                if(joinDb)
                {
                    using(IDbQuery query = dbIns.GetQuery("File"))
                    {
                        if(query !=null)
                        {
                            query.Open();
                            object obj = query.SelectToValue(new QueryArgs()
                            {
                                Where = "FID=@FID",
                                Rows = "Count(1)",
                                Param = new DbParams[]
                                {
                                new DbParams("FID",md5)
                                }
                            });
                            int count = Convert.ToInt32(obj);
                            readed = count > 0;
                        }
                    }
                }

                //循环读取
                if (!readed)
                {
                    using (IExcReader reader = excIns.GetReader())
                    {
                        bool isopen = reader?.Open(info.FullName) ?? false;

                        if(cc != null && isopen)
                        {
                            cc.ToObtainColumn = new string[] { "Target", "Sample", "Cq" };

                            while (reader.Read())
                            {
                                Cq cq = null;
                                //如果列索引为空则查找索引
                                if (!cc.IsFindColumn)
                                    cc.FindColumnIndex(reader.Columns);
                                //不为空则将单元格转化为 Cq ;
                                else
                                {
                                    CellArgs args = cc.GetArgs(reader);
                                    args.File = file;

                                    cq = cqGrouper.ConvertToCq(args);
                                }

                                //将 Cq 插入到分类集合中
                                if(cq !=null)
                                    cqGrouper?.InsertToCollection(cq,clt);
                            }
                        }
                    }
                }
            }
        }

        private string ToGenerateMD5(FileInfo info)
        {
            string sMD5 = string.Empty;

            using(FileStream fs = info.OpenRead())
            {
                MD5 md5 = MD5.Create();
                byte[] buffer = md5.ComputeHash(fs);

                foreach (byte item in buffer)
                {
                    sMD5 += item.ToString("X2");
                }
            }

            return sMD5;
        }
    }
}
