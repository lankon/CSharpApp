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
using LFSMEO.Base_LFSMEO;

namespace LFSMEO.UI
{
    public partial class F_StartForm : Form
    {
        #region parameter define
        #endregion

        #region private function
        void InitialForm()
        {
            ReadAllEnumRecipe();
            ApplicationSetting.UpdataRecipeToForm<eDefaultSetting>(this);

            ShowHint();

            if (ApplicationSetting.Get_Int_Recipe<eOEMSetting>((int)eOEMSetting.Cmbx_ShowFormName) == 1)
                Tool.ShowFormName(this);
        }
        private void ReadAllEnumRecipe()
        {
            ApplicationSetting.ReadAllRecipe<eDefaultSetting>();
            ApplicationSetting.ReadAllRecipe<eOEMSetting>();
        }
        void ShowHint()
        {
        }
        #endregion

        #region public function
        #endregion

        public F_StartForm()
        {
            InitializeComponent();

            InitialForm();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}
