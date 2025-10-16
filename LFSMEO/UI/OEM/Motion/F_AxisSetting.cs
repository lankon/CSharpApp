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
    public partial class F_AxisSetting : Form
    {
        #region parameter define
        #endregion

        #region private function
        private void SetHint()
        {
            //toolTip1.SetToolTip(Btn_OEM_Setting, "OEM Setting");
        }
        private void InitialForm()
        {
            SetHint();
            //if(ApplicationSetting.Get_Int_Recipe((int)eDefaultSetting.Cmbx_DebugShowFormName) == 1)
            //    Tool.ShowFormName(this);
        }
        #endregion

        public F_AxisSetting()
        {
            InitializeComponent();


            InitialForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
