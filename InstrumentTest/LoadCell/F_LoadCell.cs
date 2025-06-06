using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using CommonFunction;

namespace InstrumentTest
{
    public partial class F_LoadCell : Form
    {
        bool IsLoadCellOpen = false;
        bool TempStop = false;
        ILoadCell[] LoadCell = new ILoadCell[4];

        public F_LoadCell()
        {
            InitializeComponent();

            ApplicationSetting.ReadAllRecipe<eFormAppSet>();
            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);
        }

        public void SetF_LoadCell(Panel pnl, F_LoadCell form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }

        private void SetElementEnable(bool true_false)
        {
            Cmbx_Com1_Comport.Enabled = true_false;
            Cmbx_Type.Enabled = true_false;
            TxtBx_Target.Enabled = true_false;
            Cmbx_Com1_DeviceNum.Enabled = true_false;

            //待優化
            TxtBx_Com1_Station1.Enabled = true_false;
            TxtBx_Com1_Station2.Enabled = true_false;
            TxtBx_Com1_Station3.Enabled = true_false;
        }

        private ILoadCell CreateLoadCell(string Type)
        {
            switch (Type)
            {
                case "Delta":
                    return new LoadCell_Delta();
            }

            return null;
        }

        private void SetParameter(int CtrlBox)
        {
            LoadCell[CtrlBox].Set_Parameter(Tool.StringToInt(Cmbx_Com1_DeviceNum.Text), Tool.StringToDouble(TxtBx_Target.Text));
            LoadCell[CtrlBox].Set_Station(TxtBx_Com1_Station1.Text, TxtBx_Com1_Station2.Text,
                                               TxtBx_Com1_Station3.Text, "123");
        }

        private bool CheckOpenCondition()
        {
            if (!Cmbx_Com1_Comport.Text.Contains("COM"))
            {
                Tool.SaveHistoryToFile("全部Comport皆未選擇");
                MessageBox.Show("Please Select Comport");
                return false;
            }

            if (Cmbx_Com1_Comport.Text.Contains("COM") && Cmbx_Com1_DeviceNum.SelectedIndex == 0)
            {
                Tool.SaveHistoryToFile("Device Number數量未選擇");
                MessageBox.Show("Please Select DeviceNum Count");
                return false;
            }

            if (IsLoadCellOpen == true)
            {
                Tool.SaveHistoryToFile("LoadCell已連線");
                MessageBox.Show("LoadCell Already Connected");
                return false;
            }

            return true;
        }

        private void Btn_Open_Click(object sender, EventArgs e)
        {
            if (!CheckOpenCondition())
                return;

            for (int i = 1; i <= 1; i++)
            {
                var cmbx_name = $"Cmbx_Com{i}_Comport";
                var temp_cmbx = this.GetType().GetField(cmbx_name, BindingFlags.NonPublic | BindingFlags.Instance);

                if (temp_cmbx == null)
                {
                    continue;
                }

                var cmbx = temp_cmbx.GetValue(this) as ComboBox;

                if (cmbx != null)
                {
                    LoadCell[i - 1] = CreateLoadCell(Cmbx_Type.Text);

                    if (!cmbx.Text.Contains("COM") || LoadCell[i - 1].Open(cmbx.Text, 500000, 0, 8, 1))
                    {
                        IsLoadCellOpen = true;
                        SetParameter(i - 1);
                    }
                    else
                    {
                        IsLoadCellOpen = false;
                        Tool.SaveHistoryToFile(cmbx.Text + "連線失敗");
                        MessageBox.Show(cmbx.Text + "連線失敗");
                        break;
                    }
                }
            }

            if (IsLoadCellOpen)
            {
                Pnl_Open.BackColor = Color.Green;
                Tool.SaveHistoryToFile("LoadCell連線成功");
                SetElementEnable(!IsLoadCellOpen);
            }
            else
                Pnl_Open.BackColor = Color.Red;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsLoadCellOpen == true && TempStop == false)
            {
                for (int i = 0; i < 1; i++)
                {
                    LoadCell[i].Ask_AllGramStatus();
                    Thread.Sleep(2);
                    LoadCell[i].Ans_AllGramStatus();

                    double[] Gram = new double[4];
                    ushort[] status = new ushort[4];

                    Gram = LoadCell[i].Get_AllGram();
                    status = LoadCell[i].Get_AllStatus();

                    for (int k = 0; k < Tool.StringToInt(Cmbx_Com1_DeviceNum.Text); k++)
                    {
                        #region 克重顯示
                        var txt_bx_name = $"TxtBx_Com{i + 1}_Gram{k + 1}";
                        var temp_txt_bx = this.GetType().GetField(txt_bx_name, BindingFlags.NonPublic | BindingFlags.Instance);

                        if (temp_txt_bx == null)
                        {
                            Tool.SaveHistoryToFile("元件名稱轉換錯誤");
                            continue;
                        }

                        var txt_bx = temp_txt_bx.GetValue(this) as TextBox;

                        txt_bx.Text = Gram[k].ToString();
                        #endregion

                        #region 狀態顯示
                        var pnl_name = $"Pnl_Com{i + 1}_Touch{k + 1}";
                        var temp_pnl_bx = this.GetType().GetField(pnl_name, BindingFlags.NonPublic | BindingFlags.Instance);

                        if (temp_pnl_bx == null)
                        {
                            Tool.SaveHistoryToFile("元件名稱轉換錯誤");
                            continue;
                        }

                        var pnl_bx = temp_pnl_bx.GetValue(this) as Panel;

                        if (status[k] == 0)
                            pnl_bx.BackColor = Color.Red;
                        else
                            pnl_bx.BackColor = Color.Green;
                        #endregion
                    }

                }
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (LoadCell[i] == null)
                    continue;

                if (LoadCell[i].Close())
                    IsLoadCellOpen = false;
                else
                {
                    IsLoadCellOpen = true;
                    break;
                }
            }

            if (IsLoadCellOpen == false)
                Pnl_Open.BackColor = Color.Red;

            SetElementEnable(!IsLoadCellOpen);
        }

        private void Btn_ZeroCalibration_Click(object sender, EventArgs e)
        {
            TempStop = true;
            //待優化
            LoadCell[0].Set_ZeroCalibration();
            TempStop = false;
        }
    }
}
