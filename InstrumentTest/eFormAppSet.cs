using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentTest
{
    enum eFormAppSet
    {
        Cmbx_Type,
        TxtBx_Target,
        Cmbx_Com1_Comport,
        Cmbx_Com1_DeviceNum,

        TxtBx_Com1_Station1,
        TxtBx_Com1_Station2,
        TxtBx_Com1_Station3,

        #region TC Base Setting
        Cmbx_TC_Type,
        Cmbx_BaudRate,
        Cmbx_Parity,
        #endregion

        #region TPT8000 Setting
        Cmbx_CtrlBox,
        TxtBx_CtrlBxCount,
        //Cmbx_Board,
        TxtBx_BoardCount,
        TxtBx_Board_CH,      

        // =============== 請勿在中間插入其他變數 =================
        TxtBx_Temp1, TxtBx_Temp2, TxtBx_Temp3, TxtBx_Temp4, TxtBx_Temp5,
        TxtBx_Comp1, TxtBx_Comp2, TxtBx_Comp3, TxtBx_Comp4, TxtBx_Comp5,
        TxtBx_Offset1, TxtBx_Offset2, TxtBx_Offset3, TxtBx_Offset4, TxtBx_Offset5,
        TxtBx_BoxNo1, TxtBx_BoxNo2, TxtBx_BoxNo3, TxtBx_BoxNo4, TxtBx_BoxNo5, TxtBx_BoxNo6, TxtBx_BoxNo7, TxtBx_BoxNo8, TxtBx_BoxNo9,
        TxtBx_BoxCh1, TxtBx_BoxCh2, TxtBx_BoxCh3, TxtBx_BoxCh4, TxtBx_BoxCh5, TxtBx_BoxCh6, TxtBx_BoxCh7, TxtBx_BoxCh8, TxtBx_BoxCh9,
        TxtBx_TargetT1, TxtBx_TargetT2, TxtBx_TargetT3, TxtBx_TargetT4, TxtBx_TargetT5, TxtBx_TargetT6, TxtBx_TargetT7, TxtBx_TargetT8, TxtBx_TargetT9,
        //=========================================================


        #endregion


    }


}
