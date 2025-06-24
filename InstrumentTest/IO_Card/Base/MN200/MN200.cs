using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using CommonFunction;

namespace InstrumentTest.IO_Card.Base
{
    class MN200:Base_IO_Card
    {
        #region parameter define
        private Int16 nErrCode;
        private bool Initial_Success = false;
        private const Byte MaxNumDevicesPerLine = 64;
        private const Byte MaxNumLine = 5;
        private const Byte MaxNumStatus = 32;
        private MN200_Parameter MN200_Param = new MN200_Parameter();
        public List<byte> InputLineNo 
        {
            get { return MN200_Param.IO_LineNo; } 
        }
        public List<byte> InputDevNo
        {
            get { return MN200_Param.IO_DevNo; }
        }

        struct MN200_Parameter
        {
            public bool[] UseLine;          //紀錄使用的LineNo.
            public byte[,] DevNoType;       //紀錄[LineNo,DevNo]對應的Type
            public bool[,,] Input_Status;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
            public bool[,,] Output_Status;  //紀錄[LineNo,DevNo,Port]對應的Output訊號
            public List<byte> IO_LineNo;    //紀錄IO Type Line No.
            public List<byte> IO_DevNo;     //紀錄IO Type Device No.
        }
        #endregion

        public MN200()
        {
            MN200_Param.UseLine = new bool[MaxNumLine];
            MN200_Param.DevNoType = new byte[MaxNumLine, MaxNumDevicesPerLine];
            MN200_Param.Input_Status = new bool[MaxNumLine, MaxNumDevicesPerLine, MaxNumStatus];
            MN200_Param.Output_Status = new bool[MaxNumLine, MaxNumDevicesPerLine, MaxNumStatus];
            MN200_Param.IO_DevNo = new List<byte>();
            MN200_Param.IO_LineNo = new List<byte>();
        }

        #region abstract
        public override bool Open()
        {
            Byte m_NumLine = 0;
            Byte DefBaudRate = PISO_MN200.Param.COMMSPEED_10M;    //要開放設定(連線速度)
            Byte pNumDev = 0;
            int IO_DevNum = 0;

            try
            {
                if ((nErrCode = PISO_MN200.Functions.mn_open_all(ref m_NumLine)) != PISO_MN200.ErrCode.SUCCESS)
                {
                    Tool.SaveHistoryToFile("MN200開卡失敗");
                    return false;
                }
            }
            catch
            {
                Tool.SaveHistoryToFile("無MN200 dll");
                return false;
            }
            

            if(m_NumLine == 0)
            {
                Tool.SaveHistoryToFile("無MN200相關設備");
                return false;
            }

            for (byte lineNo = 0; lineNo < m_NumLine; lineNo++) // Loop through each line
            {
                // Set baud rate of current line
                if ((nErrCode = PISO_MN200.Functions.mn_set_comm_speed(lineNo, DefBaudRate)) != PISO_MN200.ErrCode.SUCCESS)
                    continue;

                MN200_Param.UseLine[lineNo] = true;

                if ((nErrCode = PISO_MN200.Functions.mn_start_line(lineNo, ref pNumDev)) != PISO_MN200.ErrCode.SUCCESS)
                    continue;

                for (byte bDevNo = 0; bDevNo < MaxNumDevicesPerLine; bDevNo++)
                {
                    Byte bDevType = 0;

                    PISO_MN200.Functions.mn_get_dev_info(lineNo, bDevNo, ref bDevType);

                    switch (bDevType)
                    {
                        case PISO_MN200.Param.DEV_INFO_NO_DEV:
                            {
                                MN200_Param.DevNoType[lineNo, bDevNo] = bDevType;
                            }
                            break;

                        case PISO_MN200.Param.DEV_INFO_IO_16IN_16OUT_DEV:
                        case PISO_MN200.Param.DEV_INFO_IO_32IN_DEV:
                            {
                                MN200_Param.DevNoType[lineNo, bDevNo] = bDevType;
                                MN200_Param.IO_LineNo.Add(lineNo);
                                MN200_Param.IO_DevNo.Add(bDevNo);
                                IO_DevNum++;
                            }
                            break;
                        case PISO_MN200.Param.DEV_INFO_IO_32OUT_DEV:
                            {
                                MN200_Param.DevNoType[lineNo, bDevNo] = bDevType;
                                IO_DevNum++;
                            }
                            break;
                    }
                }
            }

            if(IO_DevNum == 0)
            {
                Tool.SaveHistoryToFile("無MN200 IO卡");
                return false;
            }

            Initial_Success = true;
            return true;
        }

        //Motion function
        public override bool SetMotionConfig()
        {
            throw new NotImplementedException();
        }

        //IO function
        public override string GetName()
        {
            if (Initial_Success)
            return "MN200";
            else
                return "None";
        }
        public override void UpdateInput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            UInt16 wData = 0;
            byte m_CurDevType = MN200_Param.DevNoType[lineNo, devNo];

            if (m_CurDevType == PISO_MN200.Param.DEV_INFO_IO_32IN_DEV)
            {
                //讀取 DI 資料
                PISO_MN200.Functions.mn_get_di_word(lineNo, devNo, 0, ref wData);

                for (byte status = 0; status < 16; status++)
                {
                    MN200_Param.Input_Status[lineNo, devNo, status] = (wData & (1 << status)) != 0;
                }

                PISO_MN200.Functions.mn_get_di_word(lineNo, devNo, 1, ref wData);

                for (byte status = 16; status < 32; status++)
                {
                    MN200_Param.Input_Status[lineNo, devNo, status] = (wData & (1 << (status - 16))) != 0;
                }
            }
        }
        public override bool GetInputStatus(byte lineNo, byte devNo, byte port)
        {
            return MN200_Param.Input_Status[lineNo, devNo, port];
        }
        public override void UpdateOutput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            byte dev = MN200_Param.DevNoType[lineNo, devNo];

            if (dev == PISO_MN200.Param.DEV_INFO_IO_32OUT_DEV)
            {
                ushort uData_0 = 0;
                ushort uData_1 = 0;

                PISO_MN200.Functions.mn_get_do_word(lineNo, devNo, 0, ref uData_0);
                Thread.Sleep(1);
                PISO_MN200.Functions.mn_get_do_word(lineNo, devNo, 1, ref uData_1);

                for(int i=0; i<16; i++)
                {
                    if (((uData_0 >> i) & 1) == 1)
                        MN200_Param.Output_Status[lineNo, devNo, i] = true;
                    else
                        MN200_Param.Output_Status[lineNo, devNo, i] = false;

                    if (((uData_1 >> i) & 1) == 1)
                        MN200_Param.Output_Status[lineNo, devNo, i+16] = true;
                    else
                        MN200_Param.Output_Status[lineNo, devNo, i+16] = false;
                }
            }
        }
        public override bool GetOutputStatus(byte lineNo, byte DevNo, byte port)
        {
            UpdateOutput(0, lineNo, DevNo, port);
            
            bool res = MN200_Param.Output_Status[lineNo, DevNo, port];

            return res;
        }
        public override bool SetOutputStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0, bool truefalse = false)
        {
            byte dev = MN200_Param.DevNoType[lineNo, devNo]; 

            if(dev == PISO_MN200.Param.DEV_INFO_IO_32OUT_DEV)
            {
                ushort uData = 0;
                short res = -1;
                byte word_no = 0;

                if (port < 16)
                    word_no = 0;
                else
                {
                    word_no = 1;
                    port -= 16;
                }

                //取得目前DO狀態
                PISO_MN200.Functions.mn_get_do_word(lineNo, devNo, word_no, ref uData);

                if (truefalse)
                    uData |= (ushort)(1 << port);    // 設為 1
                else
                    uData &= (ushort)~(1 << port);    // 設為 0

                res = PISO_MN200.Functions.mn_set_do_word(lineNo, devNo, word_no, uData);
            }

            return true;
        }
        #endregion

        #region virtual
        public override List<byte> GetLineNo()
        {
            return MN200_Param.IO_LineNo;
        }
        public override List<byte> GetDevNo()
        {
            return MN200_Param.IO_DevNo;
        }
        #endregion


    }
}
