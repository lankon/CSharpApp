using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstrumentTest.Motion_IO_Card.Base.APS168_W64;
using InstrumentTest.Motion_IO_Card.Base.APS_Define_W32;

namespace InstrumentTest.Motion_IO_Card.Base
{
    class APS : Base_Motion_IO_Card
    {
        #region parameter define 
        APS_Parameter APS_Param = new APS_Parameter();
        private bool Initial_Success = false;

        struct APS_Parameter
        {
            public Int32 CardType;
            public Int32 MAX_DI_NUM;
            public Int32 MAX_DO_NUM;
            public bool[,,] Input_Status;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
            public bool[,,] Motion_Status;  //紀錄[LineNo,DevNo,State]對應的Motion訊號
        }
        enum APS_Motion_IO
        {
            ALM = (int)APS_Define.MIO_ALM,
            PEL = (int)APS_Define.MIO_PEL,
            MEL = (int)APS_Define.MIO_MEL,
            ORG = (int)APS_Define.MIO_ORG,
            SVON = (int)APS_Define.MIO_SVON,
            INP = (int)APS_Define.MIO_INP,
            RDY = (int)APS_Define.MIO_RDY,
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
            if (!Initial_Success)
                return "None";

            if (APS_Param.CardType == (Int32)APS_Define.DEVICE_NAME_AMP_20408C)
                return "AMP_204C";

            return "None";
        }

        public override bool Open()
        {
            if (Initial_Success == true)
                return true;

            Int32 boardID_InBits = 0;
            Int32 mode = 0;
            Int32 ret = 0;
            Int32 StartAxisID = 0;
            Int32 TotalAxisNum = 0;

            try
            {
                ret = APS168.APS_initial(ref boardID_InBits, mode);
            }
            catch(Exception ex)
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
                    APS_Param.Motion_Status = new bool[5, 5, APS_Param.MAX_DI_NUM];

                    break;
                }
            }

            Initial_Success = true;

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

        public override void UpdateOutput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            throw new NotImplementedException();
        }

        public override bool GetOutputStatus(byte lineNo, byte DevNo, byte port)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Motion Function
        public override bool SetMotionConfig()
        {
            throw new NotImplementedException();
        }

        public override short UpdateMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            Int32 st = APS168.APS_motion_io_status(devNo);

            int index = (int)MOTION_IO.ALM;

            foreach(APS_Motion_IO signal in Enum.GetValues(typeof(APS_Motion_IO)))
            {
                int bitIndex = (int)signal;
                bool isOn = ((st >> bitIndex & 1) == 1) ? true : false;

                APS_Param.Motion_Status[lineNo, devNo, index] = isOn;

                index++;
            }

            return 0;
        }

        public override bool GetMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, int state = 0)
        {
            bool res = APS_Param.Motion_Status[lineNo, devNo, state];

            return res;
        }

        public override bool Servo_ONOff(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, bool flag = false)
        {
            if (Initial_Success == false)
                return false;
            
            int res = APS168.APS_set_servo_on(devNo, flag?1:0);

            if (res == 0)
                return true;
            else
                return false;
        }

        public override bool GoHome(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            if (Initial_Success == false)
                return false;

            byte axis_id = devNo;

            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_MODE, 8);       //Set home mode         
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_DIR, 1);        //Set home direction
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_CURVE, 0);      // Set acceleration pattern (T-curve)
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_ACC, 1000000);  // Set homing acceleration rate
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_VM, 20000);     // Set homing maximum velocity.
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_VO, 50000);     // Set homing
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_EZA, 0);        // Set homing
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_SHIFT, 0);      // Set homing
            APS168.APS_set_axis_param(axis_id, (int)APS_Define.PRA_HOME_POS, 0);        // Set homing

            

            int res = APS168.APS_home_move(axis_id);

            if (res == 0)
                return true;
            else
                return false;
        }

        public override double GetPosition(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            if (Initial_Success == false)
                return -1;

            int axis = devNo;
            double pos = -1;
            
            APS168.APS_get_position_f(axis, ref pos);

            return pos;
        }
        #endregion
    }
}
