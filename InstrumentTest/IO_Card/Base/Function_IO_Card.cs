using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using CommonFunction;

namespace InstrumentTest.IO_Card.Base
{
    public delegate void UpDate_IO_StatusCallBack();

    public enum EIOCardType
    {
        None = 0,
        PCI_9111,
        MN200,
        
    }

    class Function_IO_Card
    {
        private bool Initial_IO_Success = false;
        public UpDate_IO_StatusCallBack UpDate_IO_Status { get; set; }
        private List<Base_IO_Card> IO = new List<Base_IO_Card>();
        Tool tool = new Tool();

        #region private function
        private void Process()
        {
            while(true)
            {
                //Thread持續讀取Input訊號
                for (int k = 0; k < IO.Count; k++)
                {
                    if(IO[k].GetName() == "MN200")
                    {
                        List<byte> LineNo = IO[k].GetLineNo();
                        List<byte> DevNo = IO[k].GetDevNo();

                        for (byte i = 0; i < LineNo.Count; i++)
                        {
                            IO[k].UpdateInput(LineNo[i], DevNo[i]);
                        }
                    }
                    else if(IO[k].GetName() == "PCI_9111")
                    {
                        for(byte i=0; i<15; i++)
                            IO[k].UpdateInput(0, i);
                    }
                }

                Thread.Sleep(1);
            }
        }
        #endregion

        #region public function
        public bool Initial_All_IO()
        {
            bool UseMN200 = false, UseP32C32 = false, UsePcisDask = false;
            
            Base_IO_Card mN200 = new MN200();
            Base_IO_Card fT_IO = new pcis_dask(PCIS_DASK.Param.PCI_9111DG);

            IO.Add(mN200);
            IO.Add(fT_IO);

            //開啟所有IO卡
            for (int i = 0; i < IO.Count; i++)
            {
                if (IO[i].Open() == false)
                    continue;

                if (IO[i].GetName() == "MN200")
                    UseMN200 = true;
                if (IO[i].GetName() == "PCI_9111")
                    UsePcisDask = true;
            }

            if (!UseMN200 && !UseP32C32 && !UsePcisDask)    //沒有任何一張IO卡
            {
                tool.SaveHistoryToFile("IO卡Initial失敗");
                return false;
            }

            Initial_IO_Success = true;

            return true;
        }
        public void IO_ThreadStart()
        {
            if (Initial_IO_Success == false)
                return;
            
            Task task = Task.Run(() => Process());
        }
        public bool GetInputStatus(EIOCardType CardType,byte lineNo, byte devNo, byte port)
        {
            for(int i=0; i<IO.Count; i++)
            {
                if(IO[i].GetName() == CardType.ToString())
                    return IO[i].GetInputStatus(lineNo, devNo, port);
            }

            return false;
        }
        #endregion





    }
}
