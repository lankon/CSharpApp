using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ToolFunction.Base;

namespace LFSMEO.Base_LFSMEO
{
    /// <summary>
    /// Form模板使用方式
    /// </summary>
    /// -> 修改F_Template名稱(.cs , .Designer.cs , class , function)
    /// -> 確認Recipe的Enum定義
    public partial class F_Template : Form
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

        public F_Template()
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
