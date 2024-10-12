using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

using CommonFunction;

namespace Mapping
{
    public struct XlsxType
    {
        public bool IsBold; //字型粗體
        public bool IsCenter;   //自體置中
        public bool IsBackgroundColor;  //欄位底色
        public double Width;    //欄位寬度
        public double Heigh;    //欄位高度
        public double FontSize; //字體大小
    }

    class Xlsx
    {
        Tool tool = new Tool();
        

        public XLWorkbook Open(string path)
        {
            XLWorkbook workbook = null;

            try
            {
                workbook = new XLWorkbook(path);
            }
            catch
            {
                tool.SaveHistoryToFile("開啟xlsx失敗");
            }

            return workbook;
        }

        public string GetValue(XLWorkbook work_book, string sheet_name, string cell_name)
        {
            if (work_book == null)
                return "";

            // 取得工作表
            var worksheet = work_book.Worksheet(sheet_name);

            // 讀取單元格的值
            string value = worksheet.Cell(cell_name).GetString();

            return value;
        }
    
        public void WriteValue(XLWorkbook work_book, string sheet_name, string cell_name, string data)
        {
            var worksheet = work_book.Worksheet(sheet_name);

            // 寫入數據到單元格
            worksheet.Cell(cell_name).Value = data;
        }

        public void WriteValue(XLWorkbook work_book, string sheet_name, int col, int row, double data)
        {
            var worksheet = work_book.Worksheet(sheet_name);

            // 填充單元格
            worksheet.Cell(row, col).Value = data;
        }

        public void SetType(XLWorkbook work_book, string sheet_name, int col, int row, XlsxType type)
        {
            var worksheet = work_book.Worksheet(sheet_name);

            worksheet.Cell(row, col).Style.Font.FontSize = type.FontSize;
            worksheet.Cell(row, col).Style.Font.Bold = type.IsBold;
            worksheet.Column(col).Width = type.Width;
            worksheet.Row(row).Height = type.Heigh;

            if(type.IsCenter)
            {
                worksheet.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(row, col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }

            if(type.IsBackgroundColor)
                worksheet.Cell(row, col).Style.Fill.BackgroundColor = XLColor.YellowProcess;
        }

        public XlsxType DefaultType(XlsxType type)
        {
            type.IsBold = true;
            type.Width = 9.43;
            type.Heigh = 50;
            type.FontSize = 20;
            type.IsCenter = true;
            type.IsBackgroundColor = false;
            return type;
        }

        public void Save(XLWorkbook work_book)
        {
            work_book.Save();
        }

        public bool SaveAs(XLWorkbook work_book, string path)
        {
            try
            {
                work_book.SaveAs(path);
                return true;
            }
            catch
            {
                tool.SaveHistoryToFile("xlsx另存新檔失敗");
                return false;
            }
        }
    }
    
}
