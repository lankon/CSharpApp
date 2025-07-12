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
using CommonFunction;

namespace LFSMEO.UI
{    
    public partial class F_Motion_Setting : Form
    {
        #region parameter define
        #endregion

        #region private function
        private void InitialForm()
        {
            ApplicationSetting.ReadAllRecipe<eDefaultSetting>();
            ApplicationSetting.UpdataRecipeToForm<eDefaultSetting>(this);

            ShowHint();

            if(ApplicationSetting.Get_Bool_Recipe<eDefaultSetting>((int)eDefaultSetting.None) == true)
                Tool.ShowFormName(this);    //可開選項設定是否顯示
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
