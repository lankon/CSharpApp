using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTransform
{
    enum FormItem
    {
        TxtBx_X_Interval,
        TxtBx_Y_Interval,
        TxtBx_X_Expansion,
        TxtBx_Y_Expansion,
        TxtBx_SaveFilePath,
        Cmbx_ExpansionType,
        TxtBx_RadiusExpansion,

        Cmbx_DebugMode,

        #region Recursion
        TxtBx_PixelX,
        TxtBx_PixelY,
        TxtBx_TeachPath,
        TxtBx_BatchPath,
        TxtBx_ResultDelayTime,
        TxtBx_Threshold,

        //教學結果
        TxtBx_RegionStartX,
        TxtBx_RegionStartY,
        TxtBx_RegionWidth,
        TxtBx_RegionHeight,
        TxtBx_PinStartX,
        TxtBx_PinStartY,
        TxtBx_PinWidth,
        TxtBx_PinHeight,
        #endregion

        TxtBx_PixelPitchX,
        TxtBx_PixelPitchY,
        TxtBx_EdgeLowThreshold,
        TxtBx_EdgeThreshold,
        TxtBx_ChipWidth,
        TxtBx_ChipHeigh,

    }
}
