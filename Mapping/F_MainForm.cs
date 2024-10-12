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
        #region parameter define
        Tool tool = new Tool();
        #endregion

        #region private function
        private void CreateDynamicElement()
        {
            //
            // Panel 主要顯示頁面
            //
            GlobalVariable.MyStaticPanel = new Panel();
            GlobalVariable.MyStaticPanel.Location = new System.Drawing.Point(0, 0);
            GlobalVariable.MyStaticPanel.Size = new System.Drawing.Size(1022, 554);
            GlobalVariable.MyStaticPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.Pnl_Group.Controls.Add(GlobalVariable.MyStaticPanel);

            //
            // Panel 顯示上方選項頁面
            //
            GlobalVariable.MyStaticPanel_1 = new Panel();
            GlobalVariable.MyStaticPanel_1.Location = new System.Drawing.Point(69, 0);
            GlobalVariable.MyStaticPanel_1.Size = new System.Drawing.Size(883, 65);
            GlobalVariable.MyStaticPanel_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.Pnl_Function.Controls.Add(GlobalVariable.MyStaticPanel_1);
        }
        private void InitialApplication()
        {
            //f_Mapping.SetF_Mapping(Pnl_Group, f_Mapping);
            SetHint();  //設定提示
            
            CreateDynamicElement(); //創建動態元件
            
            CreateFolder(); //建立必要的資料夾

            F_Mapping_ButtonGroup f_Mapping_ButtonGroup = new F_Mapping_ButtonGroup();
            f_Mapping_ButtonGroup.SetF_Mapping_ButtonGroup(GlobalVariable.MyStaticPanel_1, f_Mapping_ButtonGroup);
            f_Mapping_ButtonGroup.Show();

            F_Mapping f_Mapping = new F_Mapping();
            f_Mapping.SetF_Mapping(GlobalVariable.MyStaticPanel, f_Mapping);
            f_Mapping.Show();

            f_Mapping_ButtonGroup.SaveImage += f_Mapping.SavePicture;
            f_Mapping_ButtonGroup.SaveXlsx += f_Mapping.SaveMappingXlsx;
        }
        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_CloseApp, "Close");
            toolTip1.SetToolTip(Btn_Home, "Home");
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
        private void CreateFolder()
        {
            tool.CreateFolder(Application.StartupPath + @"\Temp");
            tool.CreateFolder(Application.StartupPath + @"\History");
            tool.CreateFolder(Application.StartupPath + @"\Picture");
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

        #region public function

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
            CloseFormOnPanel(GlobalVariable.MyStaticPanel_1);

            F_Mapping f_Mapping = new F_Mapping();
            f_Mapping.SetF_Mapping(GlobalVariable.MyStaticPanel, f_Mapping);
            f_Mapping.Show();

            F_Mapping_ButtonGroup f_Mapping_ButtonGroup = new F_Mapping_ButtonGroup();
            f_Mapping_ButtonGroup.SetF_Mapping_ButtonGroup(GlobalVariable.MyStaticPanel_1, f_Mapping_ButtonGroup);
            f_Mapping_ButtonGroup.Show();

            f_Mapping_ButtonGroup.SaveImage += f_Mapping.SavePicture;
            f_Mapping_ButtonGroup.SaveXlsx += f_Mapping.SaveMappingXlsx;

            GC.Collect();
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
