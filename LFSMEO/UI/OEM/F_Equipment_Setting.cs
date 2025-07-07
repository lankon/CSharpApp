using System;
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
        void InitialForm()
        {
            ApplicationSetting.ReadAllRecipe<eOEMSetting>();
            ApplicationSetting.UpdataRecipeToForm<eOEMSetting>(this);

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

            InitialForm();
        }

        private void F_Equipment_Setting_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                //儲存參數
                ApplicationSetting.SaveAllRecipe<eOEMSetting>(this);

                //釋放記憶體資源
                Tool.ReleaseButtonImages(this);
                this.Close();
                this.Dispose();
            }
        }
    }
}
