using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstrumentTest.IO_Card.Base.APS168_W64;
using InstrumentTest.IO_Card.Base.APS_Define_W32;

namespace InstrumentTest.IO_Card.Base
{
    class APS : Base_IO_Card
    {
        #region parameter define 
        APS_Parameter APS_Param = new APS_Parameter();
        
        struct APS_Parameter
        {
            public Int32 CardType;
            public Int32 MAX_DI_NUM;
            public Int32 MAX_DO_NUM;
            public bool[,,] Input_Status;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
            public int PRA_EL_LOGIC;
        }
        #endregion

        public APS()
        {

        }
        
        #region IO Function
        public override bool GetInputStatus(byte lineNo, byte DevNo, byte port)
        {
            if (APS_Param.CardType == (Int32)APS_Define.DEVICE_NAME_AMP_20408C)
            {
                if (port < 0 || port >= APS_Param.MAX_DI_NUM)
                    return false;
                if (DevNo < 0 || DevNo >= 5)
                    return false;
                if (lineNo < 0 || lineNo >= 5)
                    return false;
            }

            return APS_Param.Input_Status[lineNo, DevNo, port];
        }

        public override string GetName()
        {
            if (APS_Param.CardType == (Int32)APS_Define.DEVICE_NAME_AMP_20408C)
                return "AMP_204C";

            return "None";
        }

        public override bool Open()
        {
            Int32 boardID_InBits = 0;
            Int32 mode = 0;
            Int32 ret = 0;
            Int32 StartAxisID = 0;
            Int32 TotalAxisNum = 0;

            try
            {
                ret = APS168.APS_initial(ref boardID_InBits, mode);
            }
            catch
            {
                ret = -1;
            }
            
            
            if (ret != 0)
                return false;

            for (int i = 0; i < 16; i++)
            {
                int temp = (boardID_InBits >> i) & 1;

                if (temp != 1)
                    continue;

                ret = APS168.APS_get_card_name(i, ref APS_Param.CardType);

                if (APS_Param.CardType == (Int32)APS_Define.DEVICE_NAME_PCI_825458 ||
                    APS_Param.CardType == (Int32)APS_Define.DEVICE_NAME_AMP_20408C)
                {
                    ret = APS168.APS_get_first_axisId(i, ref StartAxisID, ref TotalAxisNum);

                    APS_Param.MAX_DI_NUM = 24;
                    APS_Param.MAX_DO_NUM = 24;
                    APS_Param.Input_Status = new bool[5, 5, APS_Param.MAX_DI_NUM];

                    break;
                }
            }

            return true;
        }

        public override void UpdateInput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            Int32 digital_input_value = 0;

            APS168.APS_read_d_input(lineNo, 0 , ref digital_input_value);

            //digital_input_value = digital_input_value >> 8; //??芭比主程式

            for (int i = 0; i < APS_Param.MAX_DI_NUM; i++)
            {
                int check = ((digital_input_value >> i) & 1);

                if (check == 1)
                    APS_Param.Input_Status[lineNo, devNo, i] = true;
                else
                    APS_Param.Input_Status[lineNo, devNo, i] = false;
            }
        }

        public override bool SetOutputStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0, bool truefalse = false)
        {
            Int32 digital_output_value = 0;

            Int32[] do_ch = new Int32[APS_Param.MAX_DO_NUM];

            //****** Read digital output channels *****************************
            APS168.APS_read_d_output(cardNo, 0 , ref digital_output_value);

            for (int i = 0; i < APS_Param.MAX_DO_NUM; i++)
                do_ch[i] = ((digital_output_value >> i) & 1);

            //************ Write digital output channels examples *************
            int LineNumber = port;  //??芭比主程式+8

            if (truefalse)
                digital_output_value |= (1 << LineNumber);
            else
                digital_output_value &= ~(1 << LineNumber);

            APS168.APS_write_d_output(cardNo
                , 0                     // I32 DO_Group
                , digital_output_value  // I32 DO_Data
            );
            //*******************************************************************

            return true;
        }
        #endregion

        #region Motion Function
        public override bool SetMotionConfig()
        {
            throw new NotImplementedException();
        }

        public override void UpdateOutput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            throw new NotImplementedException();
        }

        public override bool GetOutputStatus(byte lineNo, byte DevNo, byte port)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
