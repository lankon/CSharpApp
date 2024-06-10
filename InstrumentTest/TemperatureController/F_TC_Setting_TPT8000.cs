using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using CommonFunction;

namespace InstrumentTest
{
    public partial class F_TC_Setting_TPT8000 : Form
    {
        #region parameter define
        Tool tool = new Tool();
        private bool InitialApp = false;
        #endregion

        #region private function
        private void InitialApplication()
        {
            ShowHint();

            TemperatureController_TPT8000 TPT8000 = new TemperatureController_TPT8000();

            ApplicationSetting.ReadAllRecipe<eFormAppSet>();

            int ctrl_box = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CtrlBox);
            int ch = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_Board_CH);
            TPT8000.ReadTempOffsetFile(ctrl_box, ch);

            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);

            InitialApp = true;
        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Btn_Save, "Save Offset Value");
        }
        #endregion

        #region public function
        public void SetF_TC_Setting_TPT8000(Panel pnl, F_TC_Setting_TPT8000 form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion


        public F_TC_Setting_TPT8000()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void F_TC_Setting_TPT8000_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ApplicationSetting.SaveAllRecipe(this);
                ApplicationSetting.ReadAllRecipe<eFormAppSet>();
            }
        }

        private void F_TC_Setting_TPT8000_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);
            ApplicationSetting.ReadAllRecipe<eFormAppSet>();

            this.Dispose(); // 显式调用 Dispose 以释放资源

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void TxtBx_Board_CH_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允許只有 '0' 和 '2'
            if (e.KeyChar != '0' && e.KeyChar != '2' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // 阻止其他字符的輸入
                MessageBox.Show("Please Input 0 or 2", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            tool.SaveHistoryToFile("儲存校正參數開始");
            
            ApplicationSetting.SaveAllRecipe(this);
            ApplicationSetting.ReadAllRecipe<eFormAppSet>();

            int CtrlBox = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_CtrlBox);

            if (CtrlBox >= 1)   //TPT8000韌體端沒有編號B01控制箱指令
                CtrlBox++;

            string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Board_CH);

            String FileName = "T" + CtrlBox.ToString() + "C" + ch;

            StreamWriter File;
            File = tool.CreateFile($"\\TemperatureController\\{FileName}", ".txt", false);

            for (int i = 0; i < 5; i++)
            {
                string temp = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Temp1 + i);
                string comp = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Comp1 + i);
                string offset = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Offset1 + i);

                tool.WriteFile(File, $"{temp},{comp},{offset}");
            }

            //string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_Board_CH);
            //tool.WriteFile(File, $"#Channel,{ch}");

            tool.CloseFile(File);

            tool.SaveHistoryToFile("儲存校正參數結束");
        }

        private void Cmbx_CtrlBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InitialApp == false)
                return;

            ApplicationSetting.SetRecipe((int)eFormAppSet.Cmbx_CtrlBox, Cmbx_CtrlBox.SelectedIndex.ToString());

            TemperatureController_TPT8000 TPT8000 = new TemperatureController_TPT8000();

            TPT8000.ReadTempOffsetFile(Cmbx_CtrlBox.SelectedIndex, tool.StringToInt(TxtBx_Board_CH.Text));

            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);
        }

        private void TxtBx_Board_CH_TextChanged(object sender, EventArgs e)
        {
            if (InitialApp == false)
                return;

            ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Board_CH, TxtBx_Board_CH.Text);

            TemperatureController_TPT8000 TPT8000 = new TemperatureController_TPT8000();

            TPT8000.ReadTempOffsetFile(Cmbx_CtrlBox.SelectedIndex, tool.StringToInt(TxtBx_Board_CH.Text));

            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);
        }
    }
}
