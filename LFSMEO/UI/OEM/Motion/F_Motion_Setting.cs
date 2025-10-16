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
    public partial class F_Motion_Setting : Form
    {
        #region parameter define
        F_AxisButton f_AxisButton = new F_AxisButton();
        F_AxisSetting f_AxisSetting = new F_AxisSetting();
        #endregion

        #region private function
        private void InitialForm()
        {
            ApplicationSetting.ReadAllRecipe<eDefaultSetting>();
            ApplicationSetting.UpdataRecipeToForm<eDefaultSetting>(this);

            ShowHint();

            if(ApplicationSetting.Get_Bool_Recipe<eDefaultSetting>((int)eDefaultSetting.None) == true)
                Tool.ShowFormName(this);    //可開選項設定是否顯示

            f_AxisButton = new F_AxisButton();
            Tool.SetForm(Pnl_AxisButton, f_AxisButton);
            f_AxisButton.Show();

            f_AxisSetting = new F_AxisSetting();
            Tool.SetForm(Pnl_AxisSetting, f_AxisSetting);
            f_AxisSetting.Show();

        }
        private void ShowHint()
        {

        }
        #endregion

        #region public function
        
        #endregion

        public F_Motion_Setting()
        {
            InitializeComponent();

            InitialForm();
        }

        private void F_Template_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                //儲存參數
                ApplicationSetting.SaveRecipeFromForm<eDefaultSetting>(this);
                //重新讀取變數值
                ApplicationSetting.ReadAllRecipe<eDefaultSetting>();

                //釋放記憶體資源
                Tool.ReleaseButtonImages(this);
                this.Close();
                this.Dispose();
            }
        }
    }
}
