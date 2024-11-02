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

using FileTransform.Recursion;

namespace FileTransform
{
    public partial class F_MainForm : Form
    {
        #region parameter define
        Tool tool = new Tool();
        AppName which_app = AppName.RECURSION;
        enum AppName
        {
            RECURSION,
            COORDINATE_EXPANSION,
            NEAR_FIELD,
        }
        #endregion

        #region private function
        private void InitialApplication()
        {
            SetHint();

            CreateDynamicElement();

            CreateFolder();

            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);

            CreateApp(which_app);
        }
        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_CloseApp, "Close");
            toolTip1.SetToolTip(Btn_Home, "Home");           
        }
        private void CreateFolder()
        {
            tool.CreateFolder(Application.StartupPath + @"\Temp");
            tool.CreateFolder(Application.StartupPath + @"\History");
            tool.CreateFolder(Application.StartupPath + @"\Picture");
            tool.CreateFolder(Application.StartupPath + @"\Result");
        }
        private void CreateDynamicElement()
        {
            // Panel 主要顯示頁面
            //
            GlobalVariable.MyStaticPanel = new Panel();
            GlobalVariable.MyStaticPanel.Location = new System.Drawing.Point(0, 0);
            GlobalVariable.MyStaticPanel.Size = new System.Drawing.Size(1022, 554);
            GlobalVariable.MyStaticPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.Pnl_Group.Controls.Add(GlobalVariable.MyStaticPanel);

            //
            // Panel 顯示上方選項頁面
            //
            GlobalVariable.MyStaticPanel_1 = new Panel();
            GlobalVariable.MyStaticPanel_1.Location = new System.Drawing.Point(69, 0);
            GlobalVariable.MyStaticPanel_1.Size = new System.Drawing.Size(883, 65);
            GlobalVariable.MyStaticPanel_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.Pnl_Function.Controls.Add(GlobalVariable.MyStaticPanel_1);

        }
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
        private void CloseFormOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Close();
                    ((Form)control).Dispose();
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
        private void CreateApp(AppName app)
        {
            switch (app)
            {
                case AppName.COORDINATE_EXPANSION:
                    F_CoordinateExpansion f_CoordinateExpansion = new F_CoordinateExpansion();
                    f_CoordinateExpansion.SetF_CoordinateExpansion(GlobalVariable.MyStaticPanel, f_CoordinateExpansion);
                    f_CoordinateExpansion.Show();

                    F_CoordinateExpan_ButtonGroup f_CoordinateExpan_ButtonGroup = new F_CoordinateExpan_ButtonGroup();
                    f_CoordinateExpan_ButtonGroup.SetF_CoordinateExpan_ButtonGroup(GlobalVariable.MyStaticPanel_1, f_CoordinateExpan_ButtonGroup);
                    f_CoordinateExpan_ButtonGroup.Show();
                    break;

                case AppName.RECURSION:
                    F_Recursion f_Recursion = new F_Recursion();
                    f_Recursion.SetF_Recursion(GlobalVariable.MyStaticPanel, f_Recursion);
                    f_Recursion.Show();

                    F_Recursion_ButtonGroup f_Recursion_ButtonGroup = new F_Recursion_ButtonGroup();
                    f_Recursion_ButtonGroup.SetF_Recursion_ButtonGroup(GlobalVariable.MyStaticPanel_1, f_Recursion_ButtonGroup);
                    f_Recursion_ButtonGroup.Show();
                    break;

                case AppName.NEAR_FIELD:
                    F_NearField f_NearField = new F_NearField();
                    f_NearField.SetF_NearField(GlobalVariable.MyStaticPanel, f_NearField);
                    f_NearField.Show();

                    F_NearField_ButtonGroup f_NearField_ButtonGroup = new F_NearField_ButtonGroup();
                    f_NearField_ButtonGroup.SetF_NearFiled_ButtonGroup(GlobalVariable.MyStaticPanel_1, f_NearField_ButtonGroup);
                    f_NearField_ButtonGroup.Show();
                    break;
            }
        }
        #endregion

        public F_MainForm()
        {
            InitializeComponent();

            InitialApplication();
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
            CloseFormOnPanel(GlobalVariable.MyStaticPanel);

            CreateApp(which_app);
        }

        private void F_MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);

            this.Dispose(); // 显式调用 Dispose 以释放资源

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
