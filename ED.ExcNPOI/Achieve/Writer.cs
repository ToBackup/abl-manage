using ED.ExcRely;
using ED.ExcRely.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ED.NPOI.Achieve
{
    class Writer : Stream, IExcWriter
    {
        public bool Save(string newPath)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    work.Write(ms);
                    byte[] buffer = ms.ToArray();

                    if(string.IsNullOrEmpty(newPath))
                    {
                        if (work is XSSFWorkbook)
                            newPath = "Result_*.xlsx";
                        else if (work is HSSFWorkbook)
                            newPath = "Result_*.xls";
                        
                        //string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, newPath);
                        newPath = newPath.Replace("*", DateTime.Now.ToString("yyyyMMddHHmmss")/*files.Length.ToString()*/);
                    }

                    using (FileStream fs = new FileStream(newPath, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(buffer, 0, buffer.Length);
                        fs.Flush();
                    }
                }
                return true;
            }
            catch { return false; }
            
        }

        public bool Write(Cell cell)
        {
            if(sheet!=null)
            {
                IRow row = sheet.GetRow(cell.RowIndex);
                if (row == null)
                    row = sheet.CreateRow(cell.RowIndex);
                ICell c = row.GetCell(cell.ColumnIndex);
                if (c == null)
                    c = row.CreateCell(cell.ColumnIndex);

                try
                {
                    c.SetCellValue(cell.Value);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }
    }
}
