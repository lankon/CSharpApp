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
using LFSMEO.Logic;
using ToolFunction.Base;


namespace LFSMEO.UI
{
    public partial class F_AxisSetting : Form, IF_MotionSetting
    {
        #region parameter define
        Med_MotionSetting MotionMediator;
        #endregion

        #region private function
        private void SetHint()
        {
            //toolTip1.SetToolTip(Btn_OEM_Setting, "OEM Setting");
        }
        private void InitialForm()
        {
            SetHint();

            if (ApplicationSetting.Get_Int_Recipe<eOEMSetting>((int)eOEMSetting.Cmbx_ShowFormName) == 1)
                Tool.ShowFormName(this);
        }
        #endregion

        #region public function
        public void SetMediator(Med_MotionSetting med)
        {
            MotionMediator = med;
        }
        public void UpdateParmeter()
        {
            ApplicationSetting.ReadAllRecipe<eOEMSetting>();
            ApplicationSetting.UpdataRecipeToForm<eOEMSetting>(this);
        }
        #endregion

        public F_AxisSetting()
        {
            InitializeComponent();

            ApplicationSetting.ReadAllRecipe<eOEMSetting>();
            ApplicationSetting.UpdataRecipeToForm<eOEMSetting>(this);

            InitialForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Btn_AllSetting_Click(object sender, EventArgs e)
        {
            //Scope.DML.SaveAxis();
        }
    }
}
