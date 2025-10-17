using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ToolFunction.Base;
using LFSMEO.Base_LFSMEO;


namespace LFSMEO.UI
{
    public interface IF_MotionSetting
    {
        void SetMediator(F_MotionSettingManage med);
    }
    
    public class F_MotionSettingManage
    {
        public F_AxisSetting f_AxisSetting = null;
        public F_AxisButton f_AxisButton = null;

        public void SetForm(IF_MotionSetting form)
        {
            form.SetMediator(this);
            
            if (form is F_AxisSetting setting)
                f_AxisSetting = setting;
            else if (form is F_AxisButton button)
                f_AxisButton = button;
        }

        public void UpdateParameter()
        {
            f_AxisSetting.UpdateParmeter();
        }

        public void SaveAxisParameter()
        {
            int num = f_AxisButton.GetCurrentBtnNum();
            string set;

            Dictionary<string, string> param = new Dictionary<string, string>();

            eOEMSetting[] total_param = new eOEMSetting[]
            { 
                eOEMSetting.Cmbx_AxisType,
                eOEMSetting.TxtBx_AxisStation,
                eOEMSetting.Cmbx_AxisUse,
                eOEMSetting.Cmbx_AxisLimitLogic,
                eOEMSetting.Cmbx_AxisLimitStopMode,
            };

            for(int i=0; i<total_param.Length; i++)
            {
                set = ApplicationSetting.Get_String_Recipe<eOEMSetting>((int)total_param[i]);
                param.Add(total_param[i].ToString(), set);
            }

            Scope.DML.SaveAxis(Application.StartupPath + @"\Setting\AxisConfig.xml", $"Axis{num}", param);
        }
    }
}
