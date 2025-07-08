using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstrumentTest.Motion_IO_Card.Base.PISO_P32C32;

namespace InstrumentTest.Motion_IO_Card.Base
{
    class P32C32 : Base_Motion_IO_Card
    {
        #region parameter define 
        private bool Initial_Success = false;
        private const Byte MaxNumDevicesPerLine = 5;
        private const Byte MaxNumLine = 5;
        private const Byte MaxNumStatus = 8;
        P32C32_Parameter P32C32_Param = new P32C32_Parameter();

        struct P32C32_Parameter
        {
            public bool[,,] Input_Status;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
            public bool[,,] Output_Status;  //紀錄[LineNo,DevNo,Port]對應的Output訊號
        }
        #endregion

        public P32C32()
        {
            P32C32_Param.Input_Status = new bool[MaxNumLine, MaxNumDevicesPerLine, MaxNumStatus];
        }


        #region abstract
        public override bool Open()
        {
            try
            {
                PISO_P32C32.Functions.DriverInit(out ushort card_num);

                if (card_num == 0)
                    return false;
                else
                {
                    Initial_Success = true;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public override string GetName()
        {
            if (Initial_Success)
                return "P32C32";
            else
                return "None";
        }

        // Motion Function



        // IO Function
        public override void UpdateInput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            try
            {
                int res = PISO_P32C32.Functions.Ixud_ReadDI(lineNo, devNo, out uint value);

                for(int i=0; i<8; i++)
                {
                    int bit = ((int)value >> i) & 1;
                    
                    if(bit == 1)
                        P32C32_Param.Input_Status[lineNo, devNo, i] = true;
                    else
                        P32C32_Param.Input_Status[lineNo, devNo, i] = false;
                }

            }
            catch
            {

            }
        }
        public override bool GetInputStatus(byte lineNo, byte devNo, byte port)
        {
            return P32C32_Param.Input_Status[lineNo, devNo, port];
        }


        #endregion


        public override int AbsoluteSMove(int axis, double position, double velocity_max, double velocity_start, double Tacc, double Sacc, double Tdec, double Sdec)
        {
            throw new NotImplementedException();
        }

        public override bool GetMotionComplete(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            throw new NotImplementedException();
        }

        public override bool GetMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, int state = 0)
        {
            throw new NotImplementedException();
        }

        public override bool GetOutputStatus(byte lineNo, byte DevNo, byte port)
        {
            throw new NotImplementedException();
        }

        public override double GetPosition(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            throw new NotImplementedException();
        }

        public override bool GoHome(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            throw new NotImplementedException();
        }

        

        public override int RelativeSMove(int axis, double position, double velocity_max, double velocity_start, double Tacc, double Sacc, double Tdec, double Sdec)
        {
            throw new NotImplementedException();
        }

        public override bool Servo_ONOff(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, bool flag = false)
        {
            throw new NotImplementedException();
        }

        public override bool SetMotionConfig()
        {
            throw new NotImplementedException();
        }

        public override bool SetOutputStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0, bool truefalse = false)
        {
            throw new NotImplementedException();
        }

        public override int SetPosition(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, double pos = 0)
        {
            throw new NotImplementedException();
        }

        

        public override short UpdateMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            throw new NotImplementedException();
        }

        public override void UpdateOutput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            throw new NotImplementedException();
        }
    }
}
