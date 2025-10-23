using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using InstrumentTest.Motion_IO_Card.Base;
using ToolFunction.Base;
using System.IO;
using System.Xml.Linq;

namespace LFSMEO.Logic
{
    public class Function_Motion_Card
    {
        #region parameter define
        private List<Base_Motion_IO_Card> DML = new List<Base_Motion_IO_Card>();
        private List<AXIS_INFO> DML_INFO = new List<AXIS_INFO>();
        private HOME_INFO[] GO_HOME_PARAM;
        private Base_Motion_IO_Card.HOME_PARAM hOME_PARAM = new Base_Motion_IO_Card.HOME_PARAM();
        private int[] DML2Axis;
        private bool[] DML_Home_Complete;

        public struct AXIS_INFO
        {
            //[Axis Configuration]
            public string AXIS_TYPE;    //軸卡名稱
            public int LINE_NO;         //軸卡線程
            public int DEV_NO;          //軸卡軸編號
            public int AXIS_USE;        //軸卡使用Y/N
            public int LIMIT_LOGIC;     //硬體極限觸發邏輯
            public int STOP_MODE;       //停止模式

            //[Software Configuration]
            public string AXIS_NANE;    //軸名稱
            public int SW_LIMIT;        //軟體極限Y/N
            public double PEL_POS;      //軟體正極限位置
            public double MEL_POS;      //軟體負極限位置
            public int REVERSE_MOVE;    //運動方向相反Y/N

            //[Home Configuration]
            public int MODE;            //歸Home模式
            public int DIRECTION;       //方向
            public int ACC;             //加速度
            public int MAX_VELOCITY;    //最大速度

            public int AXIS_NO; //UI定義軸編號

            public double ORIGIN_POS;   //原點設定位置
        }
        public struct HOME_INFO
        {
            public int MODE;            //歸Home模式
            public int DIRECTION;       //方向
            public int ACC;             //加速度
            public int MAX_VELOCITY;    //最大速度

        }
        public enum AXIS_NAME
        {
            AXIS_X,
            AXIS_Y,
            AXIS_Z,
            AXIS_A,
            AXIS_AX,
            AXIS_AY,
            AXIS_AZ,
            AXIS_AA,
            AXIS_EX,
            AXIS_EY,
            AXIS_EZ,
            AXIS_EA,
            AXIS_IX,
            AXIS_IY,
            AXIS_IZ,
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
                Tool.SaveLogToFile($"{name}開卡失敗");
                return false;
            }
        }
        private bool AchieveLimit(int axis)
        {
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            DML[DML2Axis[axis]].UpdateMotionStatus(lineNo: line, devNo: dev_no);

            bool PEL = DML[DML2Axis[axis]].GetMotionStatus(lineNo: line, devNo: dev_no, state: (int)MOTION_IO.PEL);
            bool MEL = DML[DML2Axis[axis]].GetMotionStatus(lineNo: line, devNo: dev_no, state: (int)MOTION_IO.MEL);

            return PEL || MEL;
        }
        private bool SetOrigin(int axis, double pos)
        {
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            int res = DML[DML2Axis[axis]].SetPosition(lineNo: line, devNo: dev_no, pos: pos);

            if (res == 0)
                return true;
            else
                return false;
        }
        private async Task<bool> WaitForMotionCompleteAsync(int axis, int timeoutMs = 60000 * 5)
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
        private async Task<bool> WaitAchieveLimitAsync(int axis, int timeoutMs = 60000 * 5)
        {
            int elapsed = 0;
            const int interval = 20;

            while (elapsed < timeoutMs)
            {
                if (AchieveLimit(axis))
                    return true;

                await Task.Delay(interval);
                elapsed += interval;
            }

            return false;
        }
        #endregion

        #region public function
        // Initial Function
        public bool Initial_All_Motion()
        {
            bool Use_MN200, Use_APS;
            
            Base_Motion_IO_Card mN200 = new MN200();
            Base_Motion_IO_Card APS = new APS();

            Use_MN200 = OpenMotionCard(mN200, "MN200");
            Use_APS = OpenMotionCard(APS, "APS");

            if (!Use_MN200 && !Use_APS)    //沒有任何一張Motion卡
            {
                Tool.SaveLogToFile("Motion卡Initial失敗");
                return false;
            }

            Task task = Task.Run(() => Process());

            return true;
        }
        public void SetAxis(AXIS_INFO MF)
        {
            DML_INFO.Add(MF);
        }
        public void ClearAxis()
        {
            DML_INFO.Clear();
        }
        public void BindingAxis()
        {
            DML2Axis = new int[DML_INFO.Count];
            DML_Home_Complete = new bool[DML_INFO.Count];
            GO_HOME_PARAM = new HOME_INFO[DML_INFO.Count];

            Dictionary<string, int> nameToIndex = new Dictionary<string, int>();

            for (int j = 0; j < DML.Count; j++)
            {
                nameToIndex[DML[j].GetName()] = j;
            }

            for (int i = 0; i < DML_INFO.Count; i++)
            {
                if (nameToIndex.TryGetValue(DML_INFO[i].AXIS_TYPE, out int idx))
                {
                    DML2Axis[i] = idx;
                }
            }
        }


        // Set Parameter & Status Function
        public bool SetServo(int axis, bool on_off)
        {
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            bool res = DML[DML2Axis[axis]].Servo_ONOff(lineNo: line, devNo: dev_no, flag: on_off);

            return res;
        }
        public int SetSpeedConfig()
        {



            return 0;
        }
        public int SetHomeConfig(int axis, HOME_INFO info)
        {
            
            
            return 0;
        }


        // Position Function
        public double GetPosition(int axis)
        {
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            double res = DML[DML2Axis[axis]].GetPosition(lineNo: line, devNo: dev_no);

            return res;
        }


        // Home Function
        public async Task<bool> GoHome(int axis)
        {
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            DML_Home_Complete[axis] = false;
            DML[DML2Axis[axis]].GoHome(lineNo:line, devNo:dev_no);

            bool ok = await WaitAchieveLimitAsync(axis);

            if(!ok)
            {
                Tool.SaveLogToFile($"軸 = {axis} 初始化未完成");
            }

            //設定原點位置
            SetOrigin(axis, DML_INFO[axis].ORIGIN_POS);

            //到達原點後位移

            return true;
        }
        public bool Get_Home_Complete(int axis)
        {
            return DML_Home_Complete[axis];
        }


        // Move Function
        public bool Get_Motion_Complete(int axis)
        {
            byte line = (byte)DML_INFO[axis].LINE_NO;
            byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            bool res = DML[DML2Axis[axis]].GetMotionComplete(lineNo: line, devNo: dev_no);

            return res;
        }
        public bool PTP_Move(int axis , double pos, string mode = "Abs")
        {
            //byte line = (byte)DML_INFO[axis].LINE_NO;
            //byte dev_no = (byte)DML_INFO[axis].DEV_NO;

            //if(mode == "Abs")
            //    DML[DML2Axis[axis]].AbsoluteSMove

            //bool res = DML[DML2Axis[axis]].AbsoluteSMove();




            return true;
        }
        //[Read&Save Axis Information]
        public void SaveAxis(string filePath, string axisName, Dictionary<string, string> parameters)
        {
            XDocument doc;

            if (File.Exists(filePath))
                doc = XDocument.Load(filePath);
            else
                doc = new XDocument(new XElement("MachineConfig"));

            XElement root = doc.Element("MachineConfig");

            // 找出 Axis 節點
            var existingAxis = root.Elements("Axis")
                                   .FirstOrDefault(x => (string)x.Attribute("name") == axisName);

            if (existingAxis != null)
            {
                // 清空舊的參數
                existingAxis.RemoveNodes();
            }
            else
            {
                existingAxis = new XElement("Axis", new XAttribute("name", axisName));
                root.Add(existingAxis);
            }

            // 新增參數
            foreach (var kvp in parameters)
            {
                existingAxis.Add(new XElement("Parameter",
                    new XAttribute("key", kvp.Key),
                    new XAttribute("value", kvp.Value)
                ));
            }

            doc.Save(filePath);
        }
        #endregion
    }
}
