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
    public partial class F_Template : Form
    {
        #region parameter define
        #endregion

        #region private function
        void InitialForm()
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

        public F_Template()
        {
            InitializeComponent();

            InitialForm();
        }

        private void F_Template_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                //釋放記憶體資源
                Tool.ReleaseButtonImages(this);
                this.Close();
                this.Dispose();
            }
        }
    }
}
