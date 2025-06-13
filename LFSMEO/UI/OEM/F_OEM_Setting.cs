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
    public partial class F_OEM_Setting : Form
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

        public F_OEM_Setting()
        {
            InitializeComponent();
        }

        private void Btn_IO_Form_Click(object sender, EventArgs e)
        {
            Tool.HideElementOnPanel(Scope.MainPanel);

            F_IO_Setting f_IO_Setting = new F_IO_Setting();
            Tool.SetForm(Scope.MainPanel, f_IO_Setting);
            f_IO_Setting.Show();
        }
    }
}
