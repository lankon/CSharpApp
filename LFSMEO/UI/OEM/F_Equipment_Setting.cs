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

namespace LFSMEO.Base_LFSMEO
{
    public partial class F_Equipment_Setting : Form
    {
        #region parameter define
        #endregion

        #region private function
        void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<eFormItem>();
            ApplicationSetting.UpdataRecipeToForm<eFormItem>(this);

            ShowHint();


        }
        void ShowHint()
        {

        }
        #endregion

        #region public function
        
        #endregion

        public F_Equipment_Setting()
        {
            InitializeComponent();

            InitialApplication();
        }
    }
}
