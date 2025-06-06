using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using CommonFunction;

namespace FileTransform
{
    public partial class F_Coordinate_MirrorXY : Form
    {
        public F_Coordinate_MirrorXY()
        {
            InitializeComponent();
        }

      
        public string Preview_Coordinate_MirrorXY(string FileName)
        {
            string newFileName = "error";
            bool IsMirrorX = CkBx_MirrorX.Checked;
            bool IsMirrorY = CkBx_MirrorY.Checked;

            // 使用正則表達式提取數字
            Match xMatch = Regex.Match(FileName, @"X(-?\d+(\.\d+)?)");
            Match yMatch = Regex.Match(FileName, @"Y(-?\d+(\.\d+)?)");

            if (xMatch.Success && yMatch.Success)
            {
                string xValue = xMatch.Groups[1].Value;
                string yValue = yMatch.Groups[1].Value;

                xValue = (Int32.Parse(xValue) * -1).ToString();
                yValue = (Int32.Parse(yValue) * -1).ToString();

                if (IsMirrorX && !IsMirrorY)
                {
                    newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
                }
                else if (!IsMirrorX && IsMirrorY)
                {
                    newFileName = Regex.Replace(FileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);
                }
                else if (IsMirrorX && IsMirrorY)
                {
                    newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
                    newFileName = Regex.Replace(newFileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);
                }
                else
                {
                    return FileName;
                }
            }
            else
            {
                Tool.SaveHistoryToFile("未找到匹配座標");
            }

            return newFileName;
        }
        public bool GetMirrorX()
        {
            if (CkBx_MirrorX.Checked == true)
                return true;
            else
                return false;
        }

        public bool GetMirrorY()
        {
            if (CkBx_MirrorY.Checked == true)
                return true;
            else
                return false;
        }
    }
}
