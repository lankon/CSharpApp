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
using LFSMEO.Base_LFSMEO;

namespace LFSMEO.UI
{
    public partial class F_IO_Setting : Form
    {
        #region parameter define
        #endregion

        #region private function
        void InitialForm()
        {
            ApplicationSetting.ReadAllRecipe<eDefaultSetting>();
            ApplicationSetting.UpdataRecipeToForm<eDefaultSetting>(this);

            ShowHint();

            if (ApplicationSetting.Get_Int_Recipe<eOEMSetting>((int)eOEMSetting.Cmbx_ShowFormName) == 1)
                Tool.ShowFormName(this);
        }
        void ShowHint()
        {

        }
        #endregion

        #region public function
        
        #endregion

        public F_IO_Setting()
        {
            InitializeComponent();

            InitialForm();
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            string[] context = new string[] { "None", "None", "None", "None", "-1", "OFF", "False", "-1", "-1", "-1" };

            Tool.DataGrid_AddRow(DGV_IO, context);
        }

        private void F_IO_Setting_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ApplicationSetting.SaveRecipeFromForm<eDefaultSetting>(this);
                ApplicationSetting.ReadAllRecipe<eDefaultSetting>();

                this.Close();
                this.Dispose();
            }
        }
    }
}
