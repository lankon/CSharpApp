using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using InstrumentTest.Motion_IO_Card.Base;
using CommonFunction;

namespace InstrumentTest.Motion_IO_Card
{
    class Function_Motion_Card
    {
        #region parameter define
        private List<Base_Motion_IO_Card> DML = new List<Base_Motion_IO_Card>();
        #endregion

        #region private function
        private void Process()
        {
            while (true)
            {
                //Thread持續讀取Input訊號
                for (int k = 0; k < DML.Count; k++)
                {
                    if (DML[k].GetName() == "MN200")
                    {
                        List<byte> LineNo = DML[k].Get_Motion_LineNo();
                        List<byte> DevNo = DML[k].Get_Motion_DevNo();

                        for (byte i = 0; i < LineNo.Count; i++)
                        {
                            DML[k].UpdateMotionStatus(lineNo: LineNo[i], devNo: DevNo[i]);
                        }
                    }
                }

                Thread.Sleep(200);
            }
        }
        private bool OpenMotionCard(Base_Motion_IO_Card motion, string name)
        {
            if (motion.Open() == true)
            {
                DML.Add(motion);
                return true;
            }
            else
            {
                Tool.SaveHistoryToFile($"{name}開卡失敗");
                return false;
            }
        }
        #endregion


        #region public function
        public bool Initial_All_Motion()
        {
            bool Use_MN200, Use_APS;
            
            Base_Motion_IO_Card mN200 = new MN200();
            Base_Motion_IO_Card APS = new APS();

            Use_MN200 = OpenMotionCard(mN200, "MN200");
            Use_APS = OpenMotionCard(APS, "APS");

            if (!Use_MN200 && !Use_APS)    //沒有任何一張IO卡
            {
                Tool.SaveHistoryToFile("Motion卡Initial失敗");
                return false;
            }

            Task task = Task.Run(() => Process());

            return true;
        }
        #endregion
    }
}
