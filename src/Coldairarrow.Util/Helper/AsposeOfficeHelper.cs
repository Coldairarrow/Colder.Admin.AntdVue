using Aspose.Cells;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace Coldairarrow.Util
{
    /// <summary>
    /// 使用Aspose组件的Office文件操作帮助类
    /// </summary>
    public class AsposeOfficeHelper
    {
        static AsposeOfficeHelper()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 将DataTable输出为字节数组
        /// </summary>
        /// <param name="dt">表格数据</param>
        /// <returns>Byte数组</returns>
        public static byte[] DataTableToExcelBytes(DataTable dt)
        {
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;
            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数
            //生成行 列名行 
            for (int i = 0; i < Colnum; i++)
            {
                cells[0, i].PutValue(dt.Columns[i].ColumnName);
            }
            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    cells[1 + i, k].PutValue(dt.Rows[i][k].ToString());
                }
            }

            //自动行高，列宽
            sheet.AutoFitColumns();
            sheet.AutoFitRows();

            //将DataTable写入内存流
            var ms = new MemoryStream();
            book.Save(ms, SaveFormat.Excel97To2003);
            return ms.ToArray();
        }

        /// <summary>
        /// 通过模板导出Excel
        /// </summary>
        /// <param name="templateFile">模板</param>
        /// <param name="dataSource">数据源</param>
        /// <returns>文件Byte[]</returns>
        public static byte[] ExportExcelByTemplate(string templateFile, params (string SourceName, object Data)[] dataSource)
        {
            if (templateFile.IsNullOrEmpty())
                throw new Exception("模板不能为空");
            if (dataSource.Length == 0)
                throw new Exception("数据源不能为空");

            WorkbookDesigner designer = new WorkbookDesigner
            {
                Workbook = new Workbook(templateFile)
            };
            var workBook = designer.Workbook;

            dataSource.ForEach(aDataSource =>
            {
                designer.SetDataSource(aDataSource.SourceName, aDataSource.Data);
            });
            designer.Process();

            using (MemoryStream stream = new MemoryStream())
            {
                workBook.Save(stream, SaveFormat.Excel97To2003);
                var fileBytes = stream.ToArray();

                return fileBytes;
            }
        }

        /// <summary>
        /// 从excel文件导入数据
        /// 注：默认将第一行当作标题行，即不当作数据
        /// </summary>
        /// <param name="fileNmae">文件名</param>
        /// <returns></returns>
        public static DataTable ReadExcel(string fileNmae)
        {
            Workbook book = new Workbook(fileNmae);
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;

            return cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
        }

        /// <summary>
        /// 从excel文件导入数据
        /// </summary>
        /// <param name="fileNmae">文件名</param>
        /// <param name="exportColumnName">是否将第一行当作标题行</param>
        /// <returns></returns>
        public static DataTable ReadExcel(string fileNmae,bool exportColumnName)
        {
            Workbook book = new Workbook(fileNmae);
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;

            return cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, exportColumnName);
        }

        /// <summary>
        /// 从excel文件字节源导入
        /// 注：默认将第一行当作标题行，即不当作数据
        /// </summary>
        /// <param name="fileBytes">文件字节源</param>
        /// <returns></returns>
        public static DataTable ReadExcel(byte[] fileBytes)
        {
            return ReadExcel(fileBytes, true);
        }

        /// <summary>
        /// 从excel文件字节源导入
        /// </summary>
        /// <param name="fileBytes">文件字节源</param>
        /// <param name="exportColumnName">是否将第一行当作标题行</param>
        /// <returns></returns>
        public static DataTable ReadExcel(byte[] fileBytes,bool exportColumnName)
        {
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                Workbook book = new Workbook(ms);
                Worksheet sheet = book.Worksheets[0];
                Cells cells = sheet.Cells;

                return cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, exportColumnName);
            }
        }
    }
}
