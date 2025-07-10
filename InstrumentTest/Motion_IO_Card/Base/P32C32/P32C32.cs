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
        UniDAQ_Parameter UniDAQ_Param = new UniDAQ_Parameter();
        PISO_P32C32.IXUD_CARD_INFO CardInfo = new IXUD_CARD_INFO();
        PISO_P32C32.IXUD_DEVICE_INFO DevInfo = new IXUD_DEVICE_INFO();

        struct UniDAQ_Parameter
        {
            public string Model_Name;
            public List<byte> IO_DevNo;      //紀錄IO Type Device No.
            public bool[,,] Input_Status;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
            public bool[,,] Output_Status;  //紀錄[LineNo,DevNo,Port]對應的Output訊號
        }
        #endregion

        public P32C32()
        {
            UniDAQ_Param.Input_Status = new bool[MaxNumLine, MaxNumDevicesPerLine, MaxNumStatus];
            UniDAQ_Param.IO_DevNo = new List<byte>();
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
                    StringBuilder sb = new StringBuilder(20);
                    PISO_P32C32.Functions.Ixud_GetCardInfo(0, ref DevInfo, ref CardInfo, sb);
                    UniDAQ_Param.Model_Name = sb.ToString().Trim();
                    
                    if(UniDAQ_Param.Model_Name == "PISO-P32C32")
                    {
                        byte port_num = (byte)CardInfo.wDIPorts;

                        for(byte i =0; i< port_num; i++)
                        {
                            UniDAQ_Param.IO_DevNo.Add(i);
                        }
                    }

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
            if (!Initial_Success)
                return "None";

            if (UniDAQ_Param.Model_Name == "PISO-P32C32")
                return "P32C32";


            return "None";
        }

        // Motion Function



        // IO Function
        public override void UpdateInput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            if (Initial_Success == false)
                return;

            if (lineNo >= MaxNumLine || devNo >= MaxNumDevicesPerLine || port >= MaxNumStatus)
                return;

            try
            {
                int res = PISO_P32C32.Functions.Ixud_ReadDI(lineNo, devNo, out uint value);

                for(int i=0; i<8; i++)
                {
                    int bit = ((int)value >> i) & 1;
                    
                    if(bit == 1)
                        UniDAQ_Param.Input_Status[lineNo, devNo, i] = true;
                    else
                        UniDAQ_Param.Input_Status[lineNo, devNo, i] = false;
                }

            }
            catch
            {

            }
        }
        public override bool GetInputStatus(byte lineNo, byte devNo, byte port)
        {
            if(UniDAQ_Param.Model_Name == "PISO-P32C32")
            {
                int dev = port / 8;
                int num = port % 8;

                if (lineNo >= MaxNumLine || dev >= MaxNumDevicesPerLine || num >= MaxNumStatus)
                    return false;

                return UniDAQ_Param.Input_Status[lineNo, dev, num];
            }
            else
            {
                if (lineNo >= MaxNumLine || devNo >= MaxNumDevicesPerLine || port >= MaxNumStatus)
                    return false;

                return UniDAQ_Param.Input_Status[lineNo, devNo, port];
            }
        }
        public override bool SetOutputStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0, bool truefalse = false)
        {
            if (Initial_Success == false)
                return false;

            try
            {
                if (truefalse)
                    PISO_P32C32.Functions.Ixud_WriteDOBit(lineNo, devNo, port, 1);
                else
                    PISO_P32C32.Functions.Ixud_WriteDOBit(lineNo, devNo, port, 0);

                return true;
            }
            catch
            {

            }

            return false;
        }

        #endregion

        #region virtual
        
        // IO Function
        public override List<byte> Get_IO_DevNo()
        {
            return UniDAQ_Param.IO_DevNo;
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
            return false;
            
            //throw new NotImplementedException();
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
