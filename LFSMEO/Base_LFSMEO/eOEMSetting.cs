using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFSMEO.Base_LFSMEO
{
    enum eOEMSetting
    {
        Cmbx_MachineType,
        Cmbx_ShowFormName,

        #region Axis Configuration
        TxtBx_AxisType,
        TxtBx_LineNo,
        TxtBx_AxisStation,
        Cmbx_AxisUse,
        Cmbx_AxisLimitLogic,
        Cmbx_AxisLimitStopMode,
        #endregion
        #region Software Configuration
        TxtBx_AxisName,
        Cmbx_SW_Limit,
        TxtBx_SW_PEL_Pos,
        TxtBx_SW_MEL_Pos,
        Cmbx_ReverseMode,
        #endregion
        #region Home Configuration
        Cmbx_HomeMode,
        Cmbx_HomeDirection,
        TxtBx_ORGPosition,
        TxtBx_ORGShiftPosition,
        TxtBx_HomeVelocity,
        TxtBx_ORGVelocity,
        TxtBx_HomeAcc,
        #endregion
    }
}
