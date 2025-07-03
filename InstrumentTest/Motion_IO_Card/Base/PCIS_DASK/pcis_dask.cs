﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonFunction;

namespace InstrumentTest.Motion_IO_Card.Base
{
    public class Pcis_dask_param
    {
        //ADLink PCI Card Type
        public const ushort PCI_9111DG = PCIS_DASK.Param.PCI_9111DG;

        //Channel Count
        public const ushort P9111_CHANNEL_DI = PCIS_DASK.Param.P9111_CHANNEL_DI;
    }

    class Pcis_dask : Base_Motion_IO_Card
    {
        #region parameter define
        private int lineMaxCount = 5;
        private int devMaxCount = 2;
        private int portMaxCount = 16;
        PCI_DASK_Parameter pCI_Parm = new PCI_DASK_Parameter();

        struct PCI_DASK_Parameter
        {
            public ushort CardType;                    
            public bool[,,] Input_Status;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
        }
        #endregion

        public Pcis_dask(ushort card_type)
        {
            pCI_Parm.CardType = card_type;

            if(card_type == PCIS_DASK.Param.PCI_9111DG)
            {
                pCI_Parm.Input_Status = new bool[lineMaxCount, devMaxCount, portMaxCount];
            }
        }


        public override string GetName()
        {
            if (pCI_Parm.CardType == PCIS_DASK.Param.PCI_9111DG)
                return "PCI_9111";
            else
                return "None";
        }

        public override bool Open()
        {
            bool res = false;

            try
            {
                if (PCIS_DASK.Functions.Register_Card(pCI_Parm.CardType, 0) == 0)
                    res = true;
            }
            catch(Exception ex)
            {
                Tool.SaveHistoryToFile($"PCI_DASK = ({pCI_Parm.CardType}) 初始化失敗");
                res = false;
            }

            return res;
        }

        public override void UpdateInput(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0)
        {
            //port:點位
            PCIS_DASK.Functions.DI_ReadLine(lineNo, PCIS_DASK.Param.P9111_CHANNEL_DI, port, out ushort state);

            if (state == 1)
                pCI_Parm.Input_Status[lineNo, devNo, port] = true;
            else
                pCI_Parm.Input_Status[lineNo, devNo, port] = false;
        }
        public override bool GetInputStatus(byte lineNo, byte DevNo, byte port)
        {
            if (pCI_Parm.CardType == PCIS_DASK.Param.PCI_9111DG)
            {
                if (port < 0 || port >= portMaxCount)
                    return false;
                if (DevNo < 0 || DevNo >= devMaxCount)
                    return false;
                if (lineNo < 0 || lineNo >= lineMaxCount)
                    return false;
            }

            return pCI_Parm.Input_Status[lineNo, DevNo, port];
        }

        public override bool SetOutputStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, byte port = 0, bool truefalse = false)
        {
            throw new NotImplementedException();
        }

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

        public override short UpdateMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            throw new NotImplementedException();
        }

        public override bool Servo_ONOff(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, bool flag = false)
        {
            throw new NotImplementedException();
        }

        public override bool GoHome(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            throw new NotImplementedException();
        }

        public override double GetPosition(byte cardNo = 0, byte lineNo = 0, byte devNo = 0)
        {
            throw new NotImplementedException();
        }

        public override bool GetMotionStatus(byte cardNo = 0, byte lineNo = 0, byte devNo = 0, int state = 0)
        {
            throw new NotImplementedException();
        }
    }
}
