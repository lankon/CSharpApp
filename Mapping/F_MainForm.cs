using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using CommonFunction;

namespace Mapping
{
    public partial class F_MainForm : Form
    {
        Tool tool = new Tool();
        F_Setting f_Setting = new F_Setting();
        F_Mapping f_Mapping = new F_Mapping();

        public F_MainForm()
        {
            InitializeComponent();

            InitialApplication();
        }

        #region 初始化應用程式
        private void InitialApplication()
        {            
            f_Mapping.SetF_Mapping(Pnl_Group, f_Mapping);
            f_Setting.SetF_Setting(Pnl_Group, f_Setting);

            SetHint();
        }

        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_CloseApp, "Close");
            toolTip1.SetToolTip(Btn_Setting, "Setting");
            toolTip1.SetToolTip(Btn_Home, "Home");
        }
        #endregion

        private void HideFormOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Hide();
                    break; 
                }
            }
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            HideFormOnPanel(Pnl_Group);

            f_Setting.Show();
        }

        private void Btn_CloseApp_Click(object sender, EventArgs e)
        {
            // 顯示確認對話框
            DialogResult dialogResult = MessageBox.Show("Close Application ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 根據用戶的選擇返回布爾值
            if (dialogResult == DialogResult.Yes)
            {
                ApplicationSetting.SaveAllRecipe(this);

                Application.Exit();
                tool.SaveHistoryToFile("關閉應用程式");
            }
            else
            {

            }
        }

        private void Btn_Home_Click(object sender, EventArgs e)
        {
            HideFormOnPanel(Pnl_Group);

            //f_Setting.Hide();

            f_Mapping.Show();
        }

        private void F_MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);

            this.Dispose(); // 显式调用 Dispose 以释放资源

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Pnl_Function_Paint(object sender, PaintEventArgs e)
        {
            Panel pnl = (Panel)sender;

            e.Graphics.Clear(pnl.BackColor);
            e.Graphics.DrawString(pnl.Text, pnl.Font, Brushes.Black, 10, 1);
            var vSize = e.Graphics.MeasureString(pnl.Text, pnl.Font);
            //e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 8, vSize.Height / 2);
            //e.Graphics.DrawLine(Pens.Black, vSize.Width + 8, vSize.Height / 2, pnl.Width - 2, vSize.Height / 2);
            //e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 1, pnl.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, pnl.Height - 2, pnl.Width - 2, pnl.Height - 2);
            //e.Graphics.DrawLine(Pens.Black, pnl.Width - 2, vSize.Height / 2, pnl.Width - 2, pnl.Height - 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serialNumber;
            try
            {
                // 创建一个管理对象搜索器，用于查询WMI信息
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");

                // 遍历查询结果
                foreach (ManagementObject obj in searcher.Get())
                {
                    // 获取主板序列号
                    serialNumber = obj["SerialNumber"].ToString();
                    button1.Text = serialNumber;
                    //Console.WriteLine($"Mainboard Serial Number: {serialNumber}");
                }
            }
            catch
            {
                //Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }
}
