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

namespace FileTransform
{
    public partial class F_MainForm : Form
    {
        #region parameter define
        Tool tool = new Tool();
        F_CoordinateExpansion f_CoordinateExpansion = new F_CoordinateExpansion();
        F_CoordinateExpanSetting f_CoordinateExpanSetting = new F_CoordinateExpanSetting();
        #endregion

        #region private function
        #region 初始化應用程式
        private void InitialApplication()
        {
            f_CoordinateExpansion.SetF_CoordinateExpansion(Pnl_Group, f_CoordinateExpansion);
            f_CoordinateExpanSetting.SetF_CoordinateExpanSetting(Pnl_Group, f_CoordinateExpanSetting);

            SetHint();
            CreateFolder();
            f_CoordinateExpansion.Show();
        }

        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_CloseApp, "Close");
            toolTip1.SetToolTip(Btn_Setting, "Setting");
            toolTip1.SetToolTip(Btn_Home, "Home");
            toolTip1.SetToolTip(Btn_Save, "Save Picture");

            /*if (f_Mapping.Visible == true)
            {
                toolTip1.SetToolTip(Btn_Save, "Save Picture");
            }*/
        }

        private void CreateFolder()
        {
            tool.CreateFolder(Application.StartupPath + @"\Temp");
            tool.CreateFolder(Application.StartupPath + @"\History");
            tool.CreateFolder(Application.StartupPath + @"\Picture");
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
        #endregion

        public F_MainForm()
        {
            InitializeComponent();

            InitialApplication();

            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);
        }       

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            if (f_CoordinateExpanSetting.Visible == true)   //設定頁面已開啟直接跳過
                return;
            
            bool CE_flag = false;

            if (f_CoordinateExpansion.Visible == true)
                CE_flag = true;
                      
            HideFormOnPanel(Pnl_Group);

            if(CE_flag == true)
            {
                f_CoordinateExpanSetting.Show();
            }
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
            f_CoordinateExpansion.Show();
            SetHint();
        }

        private void F_MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);

            this.Dispose(); // 显式调用 Dispose 以释放资源

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            //f_Mapping.SavePicture();
        }
    }
}
