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
        private List<MOTION_INFO> DML_INFO = new List<MOTION_INFO>();
        private int[] DML2Axis;
        private bool[] DML_Home_Complete;

        public struct MOTION_INFO
        {
            public string NAME; //軸卡名稱
            public int LINE_NO; //軸卡線程
            public int DEV_NO;  //軸卡軸編號
            public int AXIS_NO; //UI定義軸編號
        }
        #endregion

        #region Threading
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

        private void CheckHomeComplete()
        {

        }
        #endregion

        #region private function
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

            if (!Use_MN200 && !Use_APS)    //沒有任何一張Motion卡
            {
                Tool.SaveHistoryToFile("Motion卡Initial失敗");
                return false;
            }

            Task task = Task.Run(() => Process());

            return true;
        }
        public void SetAxis(MOTION_INFO MF)
        {
            DML_INFO.Add(MF);
        }
        public void ClearAxis()
        {
            DML_INFO.Clear();
        }
        public void CheckDML2Axis()
        {
            DML2Axis = new int[DML_INFO.Count];
            DML_Home_Complete = new bool[DML_INFO.Count];

            Dictionary<string, int> nameToIndex = new Dictionary<string, int>();

            for (int j = 0; j < DML.Count; j++)
            {
                nameToIndex[DML[j].GetName()] = j;
            }

            for (int i = 0; i < DML_INFO.Count; i++)
            {
                if (nameToIndex.TryGetValue(DML_INFO[i].NAME, out int idx))
                {
                    DML2Axis[i] = idx;
                }
            }
        }
        public async Task<bool> GoHome(int axis)
        {
            
            
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            DML_Home_Complete[axis] = false;
            DML[DML2Axis[axis]].GoHome(lineNo:line, devNo:dev_no);

            bool ok = await WaitForMotionCompleteAsync(axis);

            if(!ok)
            {
                Tool.SaveHistoryToFile($"軸 = {axis} 初始化未完成");
            }

            //到達原點後位移
            

            return true;
            
        }
        public bool Get_Home_Complete(int axis)
        {
            return false;
        }
        public bool Get_Motion_Complete(int axis)
        {
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            DML[DML2Axis[axis]].UpdateMotionStatus(lineNo: line, devNo: dev_no);

            bool res = DML[DML2Axis[axis]].GetMotionStatus(lineNo: line, devNo: dev_no, state: (int)MOTION_IO.INP);

            return res;
        }
        
        public void Test()
        {
            Initial_All_Motion();

            Thread.Sleep(500);

            DML[1].Servo_ONOff(flag: true);
        }
        #endregion


        #region Test
        public async Task<bool> WaitForMotionCompleteAsync(int axis, int timeoutMs = 60000*5)
        {
            int elapsed = 0;
            const int interval = 20;

            while (elapsed < timeoutMs)
            {
                if (Get_Motion_Complete(axis))
                    return true;

                await Task.Delay(interval);
                elapsed += interval;
            }

            return false;
        }

        #endregion

    }
}
