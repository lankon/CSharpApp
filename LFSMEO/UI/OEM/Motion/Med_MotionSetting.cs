using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFSMEO.UI
{
    public interface IF_MotionSetting
    {
        void SetMediator(Med_MotionSetting med);
    }
    
    public class Med_MotionSetting
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
    }
}
