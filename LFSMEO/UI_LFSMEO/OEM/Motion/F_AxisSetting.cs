using LFSMEO.Base_LFSMEO;
using System;
using System.Windows.Forms;


using InstrumentTest.Motion_IO_Card.Base;
using ToolFunction.Base;
using System.Threading.Tasks;

namespace LFSMEO.UI
{
    public partial class F_AxisSetting : Form, IF_MotionSetting
    {
        #region parameter define
        F_MotionSettingManage f_MotionSettingManage;
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
        public void SetMediator(F_MotionSettingManage med)
        {
            f_MotionSettingManage = med;
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

        private void Btn_Homing_Click(object sender, EventArgs e)
        {
            f_MotionSettingManage.SaveAxisParameter();
            Scope.DML.LoadAxisConfig();

            Task.Run(async () =>
            {
                await Scope.DML.GoHome(f_MotionSettingManage.GetAxisBtnNum());
            });
        }

        private void F_AxisSetting_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
