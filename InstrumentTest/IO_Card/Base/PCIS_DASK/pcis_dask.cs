using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonFunction;

namespace InstrumentTest.IO_Card.Base
{
    class pcis_dask : Base_IO_Card
    {
        #region parameter define
        
        Tool tool = new Tool();
        PCI_DASK_Parameter pCI_Parm = new PCI_DASK_Parameter();

        struct PCI_DASK_Parameter
        {
            public ushort CardType;                    
            public bool[,,] Input_Status;   //紀錄[LineNo,DevNo,Port]對應的Input訊號
        }
        #endregion

        public pcis_dask(ushort card_type)
        {
            pCI_Parm.CardType = card_type;

            if(card_type == PCIS_DASK.Param.PCI_9111DG)
            {
                pCI_Parm.Input_Status = new bool[5, 2, 15];
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
                tool.SaveHistoryToFile($"PCI_DASK = ({pCI_Parm.CardType}) 初始化失敗");
                res = false;
            }

            return res;
        }

        public override void UpdateInput(byte lineNo, byte devNo)
        {
            //devNo:點位
            PCIS_DASK.Functions.DI_ReadLine(lineNo, PCIS_DASK.Param.P9111_CHANNEL_DI, devNo, out ushort state);

            if (state == 1)
                pCI_Parm.Input_Status[lineNo, 0, devNo] = true;
            else
                pCI_Parm.Input_Status[lineNo, 0, devNo] = false;
        }
        public override bool GetInputStatus(byte lineNo, byte DevNo, byte port)
        {
            if (pCI_Parm.CardType == PCIS_DASK.Param.PCI_9111DG)
            {
                if (port < 0 || port > 15)
                    return false;
            }

            return pCI_Parm.Input_Status[lineNo, 0, port];
        }
    }
}
