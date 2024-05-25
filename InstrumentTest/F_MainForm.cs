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


namespace InstrumentTest
{
    public partial class F_MainForm : Form
    {
        #region parameter define 
        Tool tool = new Tool();
        F_LoadCell f_LoadCell = new F_LoadCell();
        F_SelectApp f_SelectApp = new F_SelectApp();
        #endregion

        #region public function
        #endregion

        #region private function
        private void HideElementOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Hide();
                    //break; 
                }
                else if (control is Button && control.Visible == true)
                {
                    ((Button)control).Visible = false;
                }
                else if (control is Label && control.Visible == true)
                {
                    ((Label)control).Visible = false;
                }
            }
        }
        private void ShowElementOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Button && control.Visible == false)
                {
                    ((Button)control).Visible = true;
                }
                else if (control is Label && control.Visible == false)
                {
                    ((Label)control).Visible = true;
                }
            }
        }
        #endregion

        public F_MainForm()
        {
            InitializeComponent();

            InitialApplication();
        }

        #region 初始化應用程式
        private void InitialApplication()
        {
            f_LoadCell.SetF_LoadCell(Pnl_Group, f_LoadCell);
            f_SelectApp.SetF_SelectApp(Pnl_Group,f_SelectApp);

            SetHint();

            // 使用Task创建并启动线程
            //Task task = Task.Run(() =>MainTask());
        }

        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_CloseApp, "Close");
            toolTip1.SetToolTip(Btn_Home, "Home");
        }     
        #endregion
             
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
            HideElementOnPanel(Pnl_Group);

            f_SelectApp.Show();
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

        private void Btn_DeltaLoadCell_Click(object sender, EventArgs e)
        {
            HideElementOnPanel(Pnl_Group);

            
        }

        private void Btn_PISODIO_Click(object sender, EventArgs e)
        {
            PISODIO pISODIO = new PISODIO();

            pISODIO.Initialize();
        }
    }
}
