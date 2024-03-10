using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFunction;
using System.IO.Ports;
using System.Threading;

namespace InstrumentTest
{
    class DeltaLoadCell_Func
    {
        Rs485 rs485 = new Rs485();
        Tool tool = new Tool();
        public int DeviceNum = 2;
        public double TrigGramReference = 2.5; //目標克重
        private double[] Gram;
        private ushort[] TrigStatus;
        private byte[] Station = new byte[4];

        public bool Open(string port_name, int baud_rate, int parity, int data_bit, int stop_bit)
        {
            try
            {
                if (rs485.Open(port_name, baud_rate, parity, data_bit, stop_bit))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                rs485.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Ask_AllGramStatus()
        {
            byte[] TxBuf = new byte[2];
            TxBuf[0] = 0x55;
            TxBuf[1] = 0xAA;
            rs485.ClearBuffer();
            rs485.SenMsg(TxBuf, 0, 2);
        }

        public void Ans_AllGramStatus()
        {
            byte[] RxBuf = new byte[DeviceNum*6];
            byte[] tmpRxBuf = new byte[6];
            int Force;
            Gram = new double[DeviceNum];
            TrigStatus = new ushort[DeviceNum];

            rs485.ReadMsg(RxBuf, 0, DeviceNum*6);

            for (int i = 0; i < DeviceNum; i++)
            {
                Buffer.BlockCopy(RxBuf, i * 6, tmpRxBuf, 0, 6);
                if (GetAllForceCRC(tmpRxBuf, 5) == tmpRxBuf[5])
                {
                    int Temp = tmpRxBuf[3] << 8;
                    
                    Force = tmpRxBuf[2] | (tmpRxBuf[3] << 8) + (tmpRxBuf[4] << 16);
                    if ((Force >> 23) != 0)
                    {

                        Force = (int)(0xFF000000 | Force);
                    }

                    Gram[i] = (short)Force*0.1;

                    if (Gram[i] > TrigGramReference)
                        TrigStatus[i] = 0x01;
                    else
                        TrigStatus[i] = 0x00;
                }
                else
                {
                    Gram[i] = -888;
                    TrigStatus[i] = 0x01;
                }
            }
        }

        public double[] Get_AllGram()
        {
            return Gram;
        }

        public ushort[] Get_AllStatus()
        {
            return TrigStatus;
        }

        public void Set_ZeroCalibration()
        {
            //Station
            
            byte[] TxBuf = new byte[64];
            byte[] RxBuf = new byte[64];
            
            for (int i = 0; i < DeviceNum; i++)
            {
                TxBuf[0] = 0x71;
                TxBuf[1] = Station[i];
                TxBuf[2] = 0x4F;
                TxBuf[3] = 0x34;
                TxBuf[4] = 0x20;
                TxBuf[5] = GetBufCRC(TxBuf, 5);
                rs485.ClearBuffer();
                Thread.Sleep(5);
                rs485.SenMsg(TxBuf, 0, 6);
                Thread.Sleep(5);
            }
        }

        public void Set_Station(string s_station1, string s_station2, string s_station3, string s_station4)
        {
            int station1 = tool.StringToInt(s_station1);
            int station2 = tool.StringToInt(s_station2);
            int station3 = tool.StringToInt(s_station3);
            int station4 = tool.StringToInt(s_station4);

            Station[0] = (byte)station1;
            Station[1] = (byte)station2;
            Station[2] = (byte)station3;
            Station[3] = (byte)station4;
        }



        #region Function
        byte GetBufCRC(byte[] ptr, byte len)
        {
            byte crc = 0x00;
            byte[] temp_ptr = new byte[len];
            byte[] temp = new byte[len];

            for (int i = 0; i < len; i++)
            {
                temp_ptr[i] = ptr[i];
            }

            for (int i = 0; i < len; i++)
            {
                temp[i] = Reverse_Byte(temp_ptr[i]);
            }

            for (int i = 0; i < len; i++)
            {
                crc = crc_table[crc ^ temp[i]];
            }

            crc = Reverse_Byte(crc);

            return crc;
        }

        byte GetAllForceCRC(byte[] ptr, byte len)
        {
            byte crc = 0x00;
            byte[] temp_ptr = new byte[len + 1];
            byte[] temp = new byte[len + 1];

            temp_ptr[0] = 0x55;

            for (int i = 0; i < len; i++)
            {
                temp_ptr[i + 1] = ptr[i];
            }

            for (int i = 0; i < len + 1; i++)
            {
                temp[i] = Reverse_Byte(temp_ptr[i]);
            }

            for (int i = 0; i < len + 1; i++)
            {
                crc = crc_table[crc ^ temp[i]];
            }

            crc = Reverse_Byte(crc);

            return crc;
        }

        byte Reverse_Byte(byte b)
        {
            b = (byte)((b & 0xF0) >> 4 | (b & 0x0F) << 4);
            b = (byte)((b & 0xCC) >> 2 | (b & 0x33) << 2);
            b = (byte)((b & 0xAA) >> 1 | (b & 0x55) << 1);
            return b;
        }

        readonly byte[] crc_table = new byte[] {
        0x00, 0x1D, 0x3A, 0x27, 0x74, 0x69, 0x4E, 0x53, 0xE8, 0xF5, 0xD2, 0xCF, 0x9C, 0x81, 0xA6, 0xBB,
        0xCD, 0xD0, 0xF7, 0xEA, 0xB9, 0xA4, 0x83, 0x9E, 0x25, 0x38, 0x1F, 0x02, 0x51, 0x4C, 0x6B, 0x76,
        0x87, 0x9A, 0xBD, 0xA0, 0xF3, 0xEE, 0xC9, 0xD4, 0x6F, 0x72, 0x55, 0x48, 0x1B, 0x06, 0x21, 0x3C,
        0x4A, 0x57, 0x70, 0x6D, 0x3E, 0x23, 0x04, 0x19, 0xA2, 0xBF, 0x98, 0x85, 0xD6, 0xCB, 0xEC, 0xF1,
        0x13, 0x0E, 0x29, 0x34, 0x67, 0x7A, 0x5D, 0x40, 0xFB, 0xE6, 0xC1, 0xDC, 0x8F, 0x92, 0xB5, 0xA8,
        0xDE, 0xC3, 0xE4, 0xF9, 0xAA, 0xB7, 0x90, 0x8D, 0x36, 0x2B, 0x0C, 0x11, 0x42, 0x5F, 0x78, 0x65,
        0x94, 0x89, 0xAE, 0xB3, 0xE0, 0xFD, 0xDA, 0xC7, 0x7C, 0x61, 0x46, 0x5B, 0x08, 0x15, 0x32, 0x2F,
        0x59, 0x44, 0x63, 0x7E, 0x2D, 0x30, 0x17, 0x0A, 0xB1, 0xAC, 0x8B, 0x96, 0xC5, 0xD8, 0xFF, 0xE2,
        0x26, 0x3B, 0x1C, 0x01, 0x52, 0x4F, 0x68, 0x75, 0xCE, 0xD3, 0xF4, 0xE9, 0xBA, 0xA7, 0x80, 0x9D,
        0xEB, 0xF6, 0xD1, 0xCC, 0x9F, 0x82, 0xA5, 0xB8, 0x03, 0x1E, 0x39, 0x24, 0x77, 0x6A, 0x4D, 0x50,
        0xA1, 0xBC, 0x9B, 0x86, 0xD5, 0xC8, 0xEF, 0xF2, 0x49, 0x54, 0x73, 0x6E, 0x3D, 0x20, 0x07, 0x1A,
        0x6C, 0x71, 0x56, 0x4B, 0x18, 0x05, 0x22, 0x3F, 0x84, 0x99, 0xBE, 0xA3, 0xF0, 0xED, 0xCA, 0xD7,
        0x35, 0x28, 0x0F, 0x12, 0x41, 0x5C, 0x7B, 0x66, 0xDD, 0xC0, 0xE7, 0xFA, 0xA9, 0xB4, 0x93, 0x8E,
        0xF8, 0xE5, 0xC2, 0xDF, 0x8C, 0x91, 0xB6, 0xAB, 0x10, 0x0D, 0x2A, 0x37, 0x64, 0x79, 0x5E, 0x43,
        0xB2, 0xAF, 0x88, 0x95, 0xC6, 0xDB, 0xFC, 0xE1, 0x5A, 0x47, 0x60, 0x7D, 0x2E, 0x33, 0x14, 0x09,
        0x7F, 0x62, 0x45, 0x58, 0x0B, 0x16, 0x31, 0x2C, 0x97, 0x8A, 0xAD, 0xB0, 0xE3, 0xFE, 0xD9, 0xC4, };
        #endregion CRC Table


    }
}
