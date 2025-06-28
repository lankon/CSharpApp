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

        public F_StartForm()
        {
            InitializeComponent();

            InitialApplication();
        }
    }
}
