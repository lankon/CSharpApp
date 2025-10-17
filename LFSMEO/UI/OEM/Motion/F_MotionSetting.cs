using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LFSMEO.Base_LFSMEO;
using ToolFunction.Base;

namespace LFSMEO.UI
{    
    public partial class F_MotionSetting : Form
    {
        #region parameter define
        F_AxisButton f_AxisButton = new F_AxisButton();
        F_AxisSetting f_AxisSetting = new F_AxisSetting();
        F_MotionSettingManage Mediator = new F_MotionSettingManage();
        #endregion

        #region private function
        private void InitialForm()
        {
            ApplicationSetting.ReadAllRecipe<eOEMSetting>();
            ApplicationSetting.UpdataRecipeToForm<eOEMSetting>(this);

            ShowHint();

            if (ApplicationSetting.Get_Bool_Recipe<eOEMSetting>((int)eOEMSetting.Cmbx_ShowFormName) == true)
                Tool.ShowFormName(this, 1);    //可開選項設定是否顯示

            f_AxisButton = new F_AxisButton();
            Tool.SetForm(Pnl_AxisButton, f_AxisButton);
            f_AxisButton.Show();

            f_AxisSetting = new F_AxisSetting();
            Tool.SetForm(Pnl_AxisSetting, f_AxisSetting);
            f_AxisSetting.Show();

            Mediator.SetForm(f_AxisButton);
            Mediator.SetForm(f_AxisSetting);

        }
        private void ShowHint()
        {

        }
        #endregion

        #region public function
        
        #endregion

        public F_MotionSetting()
        {
            InitializeComponent();

            InitialForm();
        }

        private void F_Template_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                //儲存參數
                ApplicationSetting.SaveRecipeFromForm<eOEMSetting>(this);
                //重新讀取變數值
                ApplicationSetting.ReadAllRecipe<eOEMSetting>();

                //釋放記憶體資源
                Tool.ReleaseButtonImages(this);
                this.Close();
                this.Dispose();
            }
        }
    }
}
