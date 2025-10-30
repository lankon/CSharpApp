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

        public int GetAxisBtnNum()
        {
            return f_AxisButton.GetCurrentBtnNum();
        }

        public void SaveAxisParameter()
        {
            ApplicationSetting.SaveRecipeFromForm<eOEMSetting>(f_AxisSetting);
            ApplicationSetting.ReadAllRecipe<eOEMSetting>();

            int num = f_AxisButton.GetCurrentBtnNum();
            string set;

            Dictionary<string, string> param = new Dictionary<string, string>();

            eOEMSetting[] total_param = new eOEMSetting[]
            { 
                //[Axis Configuration]
                eOEMSetting.TxtBx_AxisType,
                eOEMSetting.TxtBx_LineNo,
                eOEMSetting.TxtBx_AxisStation,
                eOEMSetting.Cmbx_AxisUse,
                eOEMSetting.Cmbx_AxisLimitLogic,
                eOEMSetting.Cmbx_AxisLimitStopMode,
                //[Software Configuration]
                eOEMSetting.TxtBx_AxisName,
                eOEMSetting.Cmbx_SW_Limit,
                eOEMSetting.TxtBx_SW_PEL_Pos,
                eOEMSetting.TxtBx_SW_MEL_Pos,
                eOEMSetting.Cmbx_ReverseMode,
                //[Home Configuration]
                eOEMSetting.Cmbx_HomeMode,
                eOEMSetting.Cmbx_HomeDirection,
                eOEMSetting.TxtBx_ORGPosition,
                eOEMSetting.TxtBx_ORGShiftPosition,
                eOEMSetting.TxtBx_HomeVelocity,
                eOEMSetting.TxtBx_ORGVelocity,
                eOEMSetting.TxtBx_HomeAcc,
            };

            for(int i=0; i<total_param.Length; i++)
            {
                set = ApplicationSetting.Get_String_Recipe<eOEMSetting>((int)total_param[i]);
                param.Add(total_param[i].ToString(), set);
            }

            Scope.DML.SaveAxisConfig(Application.StartupPath + @"\Setting\AxisConfig.xml", $"Axis{num}", param);
        }
        public void UpdateAxisInfo2Form(int axis)
        {
            Scope.DML.LoadAxisConfig();
            var config = Scope.DML.GetAxisConfig();

            //[Axis Configuration]
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_AxisType, config[axis].AXIS_TYPE);
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_LineNo, config[axis].LINE_NO.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_AxisStation, config[axis].DEV_NO.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.Cmbx_AxisUse, config[axis].AXIS_USE.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.Cmbx_AxisLimitLogic, config[axis].LIMIT_LOGIC.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.Cmbx_AxisLimitStopMode, config[axis].STOP_MODE.ToString());
            //[Software Configuration]
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_AxisName, config[axis].AXIS_NANE);
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.Cmbx_SW_Limit, config[axis].SW_LIMIT.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_SW_PEL_Pos, config[axis].PEL_POS.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_SW_MEL_Pos, config[axis].MEL_POS.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.Cmbx_ReverseMode, config[axis].REVERSE_MOVE.ToString());
            //[Home Configuration]
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.Cmbx_HomeMode, config[axis].MODE.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.Cmbx_HomeDirection, config[axis].DIRECTION.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_ORGPosition, config[axis].HOME_POS.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_ORGShiftPosition, config[axis].HOME_SHIFT.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_HomeVelocity, config[axis].MAX_VELOCITY.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_ORGVelocity, config[axis].HOEM_FIND_ORG_VELOCITY.ToString());
            ApplicationSetting.SetRecipe<eOEMSetting>((int)eOEMSetting.TxtBx_HomeAcc, config[axis].ACC.ToString());

            ApplicationSetting.SaveAllRecipe<eOEMSetting>();
            ApplicationSetting.UpdataRecipeToForm<eOEMSetting>(f_AxisSetting);
        }
    }
}
