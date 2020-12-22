using ED.BLL.CalculatePort;
using ED.BLL.CellPort;
using ED.BLL.CollectionPort;
using ED.BLL.CqPort;
using ED.BLL.ExcelPort;
using ED.BLL.Model;
using ED.BLL.VerifyPort;
using ED.Core;
using ED.DbRely;
using ED.DbRely.Model;
using ED.ExcRely;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using SIF = System.IO.File;
using MF = ED.BLL.Model.File;
using System.Security.Cryptography;

namespace ED.BLL.Load
{
    abstract class LoadBase :ProgressAbstract, IEDLoad
    {
        #region
        protected bool JoinDb { get; }
        protected ICellHelper CellHelper { get; }//单元格接口
        protected ICqHelper CqHelper { get; } //Cq接口
        protected IExtractValidation EvFile { get; } //验证接口
        protected IGroup Group { get; } //分组集合接口
        protected ICalculation Calculation { get; } //计算接口
        protected IOutPut Output { get; }//输出接口
        protected IExcInstance ExcIns { get; }
        protected IDbInstance DbIns { get; }
        public override string Tag { get; set; }

        //protected IEDSett Set { get; }
        #endregion


        public LoadBase(Port port)
        {
            JoinDb = port.JoinDb;

            CellHelper = port.CellHelper;
            EvFile = port.EvFile;
            IExtractValidation evTarget = port.EvTarget;
            IExtractValidation evSample = port.EvSample;
            CqHelper = port.CqHelper;
            Group = port.Group;
            Calculation = port.Calculation;
            Output = port.Output;
            ExcIns = port.ExcIns;
            DbIns = port.DbIns;
            //Set = port.Set;
        }

        public void Load(int maxPro = 100, params string[] paths)
        {
            int firstStage = (int)(maxPro * 0.8d);
            int secondStage = (int)(maxPro * 0.9d);
            //添加到集合
            InsertToCollection(0,firstStage,paths);

            //下面开始从集合中进行计算
            ResultCollection rc = GetResult(firstStage, secondStage);
            //下面开始打印到 Excel
            OutToExcel(secondStage, maxPro, rc);
        }

        protected virtual void OutToExcel(int minPro, int maxPro, ResultCollection rc)
        {
            if (Output != null)
                Output.SetProgress(minPro, maxPro);
            Output.ProgressEvent += this.OnProgress;
            Output.ToExcel(Group, ExcIns.GetWriter(), rc);
            Output.ProgressEvent -= this.OnProgress;
        }

        protected virtual ResultCollection GetResult(int minPro, int maxPro)
        {
            ResultCollection rc = null;
            if (Calculation != null)
            {
                Calculation.SetProgress(minPro, maxPro);
                Calculation.ProgressEvent += this.OnProgress;
                rc = Calculation.ToCalculate(Group);
                Calculation.ProgressEvent -= this.OnProgress;
            }
            return rc;
        }

        protected virtual void InsertToCollection(int minPro,int maxPro, string[] paths)
        {
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            List<FileInfo> files = new List<FileInfo>();

            foreach (string path in paths)
            {
                if (Directory.Exists(path))
                    dirs.Add(new DirectoryInfo(path));
                else if (SIF.Exists(path))
                    files.Add(new FileInfo(path));
            }

            if (dirs.Any())
            {
                Each each = new Each();
                each.SetProgress(minPro, maxPro);
                each.FileHandleEvent += Each_FileHandleEvent;
                each.ProgressEvent += this.OnProgress;

                each.DirsEach(dirs.ToArray());

                each.FileHandleEvent -= Each_FileHandleEvent;
                each.ProgressEvent -= this.OnProgress;
            }

            if (files.Any())
            {
                FileEach(files);
            }
        }

        private void FileEach(List<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                Each_FileHandleEvent(file);
            }
        }
        protected virtual void Each_FileHandleEvent(FileInfo info)
        {
            //判断文件是否符合要求
            bool meet = true;
            meet = !info.Name.Contains("~$") && (EvFile?.IsMeet(info.Name) ?? false);

            if (meet)
            {
                //生成MD5
                string md5 = ToGenerateMD5(info);
                MF file = new MF()
                {
                    FID = md5,
                    FileName = info.Name,
                    Date = Convert.ToDateTime(EvFile.Extract(info.Name))
                };

                //查询数据库，判断是否已经读取过
                bool readed = false;
                if (JoinDb)
                {
                    using (IDbQuery query = DbIns.GetQuery("Cq"))
                    {
                        if (query != null)
                        {
                            query.Open();
                            object obj = query.SelectToValue(new QueryArgs()
                            {
                                Where = "FID=@FID",
                                Rows = "Count(1)",
                                Param = new DbParams[]
                                {
                                new DbParams("FID",md5)
                                },
                                Group = "FID"
                            });
                            int count = Convert.ToInt32(obj);
                            readed = count > 0;
                        }
                    }
                }

                //循环读取
                if (!readed)
                {
                    using (IExcReader reader = ExcIns.GetReader())
                    {
                        bool isopen = reader?.Open(info.FullName) ?? false;

                        if (CellHelper != null && isopen)
                        {
                            CellHelper.ToObtainColumn = new string[] { "Target", "Sample", "Cq" };

                            using (IDbAlter alter = DbIns.GetAlter("Cq"))
                            {
                                alter.Open();

                                IDbTrans trans = DbIns.GetTrans();
                                trans.Begin();

                                while (reader.Read())
                                {
                                    Record cq = null;
                                    //如果列索引为空则查找索引
                                    if (!CellHelper.IsFindColumn)
                                        CellHelper.FindColumnIndex(reader.Columns);
                                    //不为空则将单元格转化为 Cq ;
                                    else
                                    {
                                        CellArgs args = CellHelper.GetArgs(reader);
                                        args.File = file;

                                        cq = CqHelper.ConvertToCq(args);
                                    }

                                    //将 Cq 插入到分类集合中
                                    if (cq != null)
                                    {
                                        if (JoinDb)
                                            try { alter.Insert(cq, "ID"); }
                                            catch { trans.Rollback(); }

                                        CqHelper?.InsertToCollection(cq, Group);
                                    }
                                }

                                trans.Commit();
                            }
                        }
                    }
                }
            }
        }
        protected string ToGenerateMD5(FileInfo info)
        {
            string sMD5 = string.Empty;

            using (FileStream fs = info.OpenRead())
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
