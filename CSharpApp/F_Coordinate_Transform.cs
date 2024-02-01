using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpApp
{
    public partial class F_Coordinate_Transform : Form
    {
        public F_Coordinate_Transform()
        {
            InitializeComponent();
        }

        Tool tool = new Tool();

        public int GetShiftX()
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
