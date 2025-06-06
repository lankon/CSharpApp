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
using System.Runtime.InteropServices;

using CommonFunction;


namespace ImageProcessing
{
    public partial class F_MainForm : Form
    {
        #region parameter define
        bool IsServerMode = false;
        
        AppName which_app = AppName.FF_Calculate;  //設定使用程式類型
        enum AppName
        {
            RECURSION,
            COORDINATE_EXPANSION,
            NEAR_FIELD,
            WAFER_ALIGN_ANGLE,
            FF_Calculate,
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
            Tool.CreateFolder(Application.StartupPath + @"\Temp");
            Tool.CreateFolder(Application.StartupPath + @"\History");
            Tool.CreateFolder(Application.StartupPath + @"\Picture");
            Tool.CreateFolder(Application.StartupPath + @"\Result");
        }
        private void CreateDynamicElement()
        {
            // Panel 主要顯示頁面
            //
            Scope.MyStaticPanel = new Panel();
            Scope.MyStaticPanel.Location = new System.Drawing.Point(0, 0);
            Scope.MyStaticPanel.Size = new System.Drawing.Size(1022, 554);
            Scope.MyStaticPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.Pnl_Group.Controls.Add(Scope.MyStaticPanel);

            //
            // Panel 顯示上方選項頁面
            //
            Scope.MyStaticPanel_1 = new Panel();
            Scope.MyStaticPanel_1.Location = new System.Drawing.Point(69, 0);
            Scope.MyStaticPanel_1.Size = new System.Drawing.Size(883, 65);
            Scope.MyStaticPanel_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.Pnl_Function.Controls.Add(Scope.MyStaticPanel_1);

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
                //case AppName.COORDINATE_EXPANSION:
                //    F_CoordinateExpansion f_CoordinateExpansion = new F_CoordinateExpansion();
                //    f_CoordinateExpansion.SetF_CoordinateExpansion(Scope.MyStaticPanel, f_CoordinateExpansion);
                //    f_CoordinateExpansion.Show();

                //    F_CoordinateExpan_ButtonGroup f_CoordinateExpan_ButtonGroup = new F_CoordinateExpan_ButtonGroup();
                //    f_CoordinateExpan_ButtonGroup.SetF_CoordinateExpan_ButtonGroup(Scope.MyStaticPanel_1, f_CoordinateExpan_ButtonGroup);
                //    f_CoordinateExpan_ButtonGroup.Show();
                //    break;

                //case AppName.RECURSION:
                //    F_Recursion f_Recursion = new F_Recursion();
                //    f_Recursion.SetF_Recursion(Scope.MyStaticPanel, f_Recursion);
                //    f_Recursion.Show();

                //    F_Recursion_ButtonGroup f_Recursion_ButtonGroup = new F_Recursion_ButtonGroup();
                //    f_Recursion_ButtonGroup.SetF_Recursion_ButtonGroup(Scope.MyStaticPanel_1, f_Recursion_ButtonGroup);
                //    f_Recursion_ButtonGroup.Show();
                //    break;

                //case AppName.NEAR_FIELD:
                //    F_NearField f_NearField = new F_NearField();
                //    f_NearField.SetF_NearField(Scope.MyStaticPanel, f_NearField);
                //    f_NearField.Show();

                //    F_NearField_ButtonGroup f_NearField_ButtonGroup = new F_NearField_ButtonGroup();
                //    f_NearField_ButtonGroup.SetF_NearFiled_ButtonGroup(Scope.MyStaticPanel_1, f_NearField_ButtonGroup);
                //    f_NearField_ButtonGroup.Show();
                //    break;

                //case AppName.WAFER_ALIGN_ANGLE:
                //    F_Wafer_Align_Angle f_Wafer_Align_Angle = new F_Wafer_Align_Angle();
                //    f_Wafer_Align_Angle.SetF_Wafer_Align_Angle(Scope.MyStaticPanel, f_Wafer_Align_Angle);
                //    f_Wafer_Align_Angle.Show();
                //    if(IsServerMode)
                //        f_Wafer_Align_Angle.Btn_ServeTest_Click(null, EventArgs.Empty);

                //    F_Wafer_Align_Angle_ButtonGroup f_Wafer_Align_Angle_ButtonGroup = new F_Wafer_Align_Angle_ButtonGroup();
                //    f_Wafer_Align_Angle_ButtonGroup.SetF_Wafer_Align_Angle_ButtonGroup(Scope.MyStaticPanel_1, f_Wafer_Align_Angle_ButtonGroup);
                //    f_Wafer_Align_Angle_ButtonGroup.Show();
                //    break;
                default:
                    {
                        //None Application
                    }
                    break;
            }
        }

        #region 視窗拖曳功能
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        bool beginMove = false;//初始化滑鼠位置
        int currentXPosition;
        int currentYPosition;

        private void F_MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//滑鼠的x座標為當前窗體左上角x座標
                currentYPosition = MousePosition.Y;//滑鼠的y座標為當前窗體左上角y座標
            }
        }

        private void F_MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根據滑鼠x座標確定窗體的左邊座標x
                this.Top += MousePosition.Y - currentYPosition;//根據滑鼠的y座標窗體的頂部，即Y座標
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void F_MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //設定初始狀態
                currentYPosition = 0;
                beginMove = false;
            }
        }
        #endregion

        #endregion

        public F_MainForm(string msg)
        {
            if (msg == "ProgramStart")
            {
                Tool.SaveHistoryToFile("ProgramStart隱藏視窗");
                this.Visible = false;
                IsServerMode = true;
            }

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
                Tool.SaveHistoryToFile("關閉應用程式");
            }
            else
            {

            }
        }

        private void Btn_Home_Click(object sender, EventArgs e)
        {
            CloseFormOnPanel(Scope.MyStaticPanel);

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
