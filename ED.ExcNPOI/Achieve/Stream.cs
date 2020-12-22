using ED.ExcRely;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using SSCell = NPOI.SS.UserModel.ICell;

namespace ED.NPOI.Achieve
{
    class Stream : IExcStream
    {
        protected static IWorkbook work = null;
        protected static ISheet sheet = null;

        public string FileName { get; private set; }

        public void Close()
        {
            work?.Close();
        }

        public void Dispose()
        {
            //this.Close();
            work = null;
            sheet = null;
            GC.Collect();
        }


        public bool Open(string path =null)
        {
            FileName = path;
            if(path == null)
            {
                work = new XSSFWorkbook();
                work.CreateSheet();
            }
            else if(path.Contains(".xlsx"))
                work = new XSSFWorkbook(path);
            else if(path.Contains(".xls"))
            {
                using(FileStream fs = new FileStream(path,FileMode.Open,FileAccess.ReadWrite))
                {
                    work = new HSSFWorkbook(fs);
                }
            }
            sheet = work?.GetSheetAt(0);

            if (sheet != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取单元格数据
        /// </summary>
        /// <returns></returns>
        protected dynamic GetValue(SSCell cell)
        {
            dynamic value = String.Empty;
            if (cell != null)
            {
                try
                {
                    if (cell.CellType == CellType.Formula || cell.CellType == CellType.Numeric) //判断是否是日期格式
                    {
                        bool isDate = false;
                        try { isDate = DateUtil.IsCellDateFormatted(cell); }
                        catch { }
                        if (isDate)
                            value = cell.DateCellValue;
                        else if (cell.CellType == CellType.Formula) //判断是否是公式和数字
                        {
                            IFormulaEvaluator fe = null;
                            if (work is XSSFWorkbook)
                                fe = new XSSFFormulaEvaluator(work);
                            else if (work is HSSFWorkbook)
                                fe = new HSSFFormulaEvaluator(work);
                            if (fe != null)
                            {
                                CellValue cv = fe.Evaluate(cell);
                                if (cv.CellType == CellType.Numeric)
                                    value = cv.NumberValue;
                                else if (cv.CellType == CellType.Boolean)
                                    value = cv.BooleanValue;
                                else if (cv.CellType == CellType.Boolean)
                                    value = cv.BooleanValue;
                                else
                                    value = cv.ToString();
                            }
                        }
                        else if (cell.CellType == CellType.Numeric)
                        {
                            value = cell.NumericCellValue;
                        }
                    }
                    else if (cell.CellType == CellType.Boolean)
                        value = cell.BooleanCellValue;
                    else if (cell.CellType == CellType.Error)
                        value = cell.ErrorCellValue;
                    else
                        value = cell.ToString();

                }
                catch { value = "erorr"; }
            }
            return value;
        }
    }
}
