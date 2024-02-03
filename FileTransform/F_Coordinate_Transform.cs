using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonFunction;
using System.IO;
using System.Text.RegularExpressions;

namespace FileTransform
{
    public partial class F_Coordinate_Transform : Form
    {
        public F_Coordinate_Transform()
        {
            InitializeComponent();
        }

        Tool tool = new Tool();

        public string Preview_Coordinate_Transform(string FileName)
        {
            int ShiftX = GetShiftX();
            int ShiftY = GetShiftY();
            string newFileName = "error";

            // 使用正則表達式提取數字
            Match xMatch = Regex.Match(FileName, @"X(-?\d+(\.\d+)?)");
            Match yMatch = Regex.Match(FileName, @"Y(-?\d+(\.\d+)?)");

            if (xMatch.Success && yMatch.Success)
            {
                string xValue = xMatch.Groups[1].Value;
                string yValue = yMatch.Groups[1].Value;

                xValue = (Int32.Parse(xValue) + ShiftX).ToString();
                yValue = (Int32.Parse(yValue) + ShiftY).ToString();

                newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
                newFileName = Regex.Replace(newFileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);
            }
            else
            {
                tool.SaveHistoryToFile("未找到匹配座標,錯誤檔案名稱:" + newFileName);
            }

            return newFileName;
        }
        public void Coordinate_Transform(string folderPath)
        {
            int ShiftX = GetShiftX();
            int ShiftY = GetShiftY();
            
            // 檢查資料夾是否存在
            if (Directory.Exists(folderPath))
            {
                // 取得資料夾內的所有檔案
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    // 建立新的檔案名稱
                    string newFileName = "";
                    string FileName = Path.GetFileName(filePath);

                    // 使用正則表達式提取數字
                    Match xMatch = Regex.Match(FileName, @"X(-?\d+(\.\d+)?)");
                    Match yMatch = Regex.Match(FileName, @"Y(-?\d+(\.\d+)?)");

                    if (xMatch.Success && yMatch.Success)
                    {
                        string xValue = xMatch.Groups[1].Value;
                        string yValue = yMatch.Groups[1].Value;

                        xValue = (Int32.Parse(xValue) + ShiftX).ToString();
                        yValue = (Int32.Parse(yValue) + ShiftY).ToString();

                        newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
                        newFileName = Regex.Replace(newFileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);
                    }
                    else
                    {
                        tool.SaveHistoryToFile("未找到匹配座標,錯誤檔案名稱:" + newFileName);
                        return;
                    }

                    // 建立新的檔案完整路徑
                    string newFilePath = Path.Combine(folderPath, newFileName);

                    // 修改檔案名稱
                    File.Move(filePath, newFilePath);
                }
            }
            else
            {
                tool.SaveHistoryToFile("指定的資料夾不存在");
            }
        }

        private int GetShiftX()
        {
            int number;
            if (!int.TryParse(TxtBx_ShiftX.Text, out number))
            {
                number = 0;
                TxtBx_ShiftX.Text = "Error";
                tool.SaveHistoryToFile("型態轉換錯誤");
            }

            return number;
        }

        public int GetShiftY()
        {
            int number;
            if (!int.TryParse(TxtBx_ShiftY.Text, out number))
            {
                number = 0;
                TxtBx_ShiftY.Text = "Error";
                tool.SaveHistoryToFile("型態轉換錯誤");
            }

            return number;
        }
    }
}
