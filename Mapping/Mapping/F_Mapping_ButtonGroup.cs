﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonFunction;

namespace Mapping
{
    public delegate void SaveImageCallBack();
    public delegate void SaveXlsxCallBack();
    public delegate void SaveTxtCallBack();

    public partial class F_Mapping_ButtonGroup : Form
    {
        #region parameter define 
        public SaveImageCallBack SaveImage { get; set; }
        public SaveXlsxCallBack SaveXlsx { get; set; }
        public SaveTxtCallBack SaveTxt { get; set; }
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
        private void InitialApplication()
        {
            ShowHint();
        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Btn_Save, "Save Mapping");
            toolTip1.SetToolTip(Btn_Setting, "Setting");
            toolTip1.SetToolTip(Btn_OneToOne, "One To One");
            toolTip1.SetToolTip(Btn_BinMapping, "Bin Mapping");
            toolTip1.SetToolTip(Pnl_FormHint, "F_Mapping_ButtonGroup");
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
        #endregion

        #region public function
        public void SetF_Mapping_ButtonGroup(Panel pnl, F_Mapping_ButtonGroup form)
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
        #endregion

        public F_Mapping_ButtonGroup()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_OneToOne_Click(object sender, EventArgs e)
        {
            CloseFormOnPanel(GlobalVariable.MyStaticPanel);
            CloseFormOnPanel(GlobalVariable.MyStaticPanel_1);

            F_OneToOne f_OneToOne = new F_OneToOne();
            f_OneToOne.SetF_OneToOne(GlobalVariable.MyStaticPanel, f_OneToOne);
            f_OneToOne.Show();
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            CloseFormOnPanel(GlobalVariable.MyStaticPanel);
            CloseFormOnPanel(GlobalVariable.MyStaticPanel_1);

            F_Setting f_Setting = new F_Setting();
            f_Setting.SetF_Setting(GlobalVariable.MyStaticPanel, f_Setting);
            f_Setting.Show();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            SaveImage();

            if(ApplicationSetting.Get_Bool_Recipe((int)FormItem.Cmbx_OutputExcel))
            {
                SaveXlsx();
                SaveTxt();
            }
        }

        private void Btn_BinMapping_Click(object sender, EventArgs e)
        {
            CloseFormOnPanel(GlobalVariable.MyStaticPanel);
            CloseFormOnPanel(GlobalVariable.MyStaticPanel_1);

            F_BinMapping f_BinMapping = new F_BinMapping();
            f_BinMapping.SetF_BinMapping_ButtonGroup(GlobalVariable.MyStaticPanel, f_BinMapping);
            f_BinMapping.Show();
        }
    }
}
