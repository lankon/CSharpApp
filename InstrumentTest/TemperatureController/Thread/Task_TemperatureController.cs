using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFunction;

namespace InstrumentTest
{
    class Task_TemperatureController
    {
        #region parameter define 
        enum WORK
        {
            INITIAL,

            CONNECT,
            CLOSE,

            IDLE,

            START,
            STOP,
            START_ALL,
            STOP_ALL,

            MONITOR,
            ASK_PV,
            ANS_PV,

            ERROR,
        }

        public UpdatePresentValueCallBack UpdatePresentValue { get; set; }
        public UpdateSetValueCallBack UpdateSetValue { get; set; }

        Tool tool = new Tool();
        private bool Terminate = true;  //Thread是否停止
        private bool IsConnect = false; //確認連線
        private int delay_count = 0;
        private WORK state = WORK.INITIAL;
        private string ErrorMsg = "";
        private string present_temp_value = "-99";
        ITemperatureController[] TC = new ITemperatureController[4];
        #endregion

        #region private function
        private ITemperatureController Create_TC(string Type)
        {
            switch (Type)
            {
                case "TPT8000":
                    return new TemperatureController_TPT8000();
            }
            return null;
        }
        private void ResetTimeCount(out int tick)
        {
            tick = Environment.TickCount;
        }
        private bool CheckTimeOverMilliSec(int tick, int time)
        {
            var time_count = Environment.TickCount - tick;
            bool res = time_count > time;

            return res;
        }
        private void Transition(WORK target)
        {
            if (target != state) //狀態有變化時紀錄
            {
                tool.SaveHistoryToFile("Task_TemperatureController:"+ target.ToString());
            }

            state = target;
        }
        #endregion

        #region public function
        public void Connect()
        {
            ResetError();
            Transition(WORK.CONNECT);
        }
        public void DisConnect()
        {
            ResetError();
            Transition(WORK.CLOSE);
        }
        public bool IsIdle()
        {
            if (state != WORK.IDLE)
                return false;
            else
                return true;
        }
        public void Start()
        {
            ResetError();
            Transition(WORK.START);
        }
        public void StartAll()
        {
            ResetError();
            Transition(WORK.START_ALL);
        }
        public void Stop()
        {
            ResetError();
            Transition(WORK.STOP);
        }
        public string GetError()
        {
            return ErrorMsg;
        }
        public void ResetError()
        {
            ErrorMsg = "";
        }
        #endregion

        public Task_TemperatureController()
        {
            Task task = Task.Run(() => MainTask());
        }

        private void MainTask()
        {
            while (Terminate)
            {
                switch (state)
                {                               
                    case WORK.INITIAL:
                        //Transition(WORK.CONNECT);
                        
                        break;

                    case WORK.IDLE:
                        if (IsConnect)
                        {
                            //Transition(WORK.MONITOR);
                            state = WORK.MONITOR;

                            #region 顯示PV SV
                            double value = tool.StringToDouble(present_temp_value);
                            UpdatePresentValue(value);
                            #endregion
                        }
                        break;

                    #region Monitor
                    case WORK.MONITOR:
                        //Transition(WORK.ASK_PV);
                        state = WORK.ASK_PV;
                        ResetTimeCount(out delay_count);
                        break;

                    case WORK.ASK_PV:
                        if(CheckTimeOverMilliSec(delay_count,50))
                        {
                            TC[0].AskPV();
                            ResetTimeCount(out delay_count);
                            //Transition(WORK.ANS_PV);
                            state = WORK.ANS_PV;
                        }
                        break;

                    case WORK.ANS_PV:
                        if(CheckTimeOverMilliSec(delay_count, 100))
                        {
                            present_temp_value =  TC[0].GetAns();
                            ResetTimeCount(out delay_count);
                            //Transition(WORK);
                            state = WORK.IDLE;
                        }
                        break;
                    #endregion

                    #region 連線/結束連線
                    case WORK.CONNECT:
                        for (int i = 0; i < 1; i++)
                        {
                            TC[i] = Create_TC("TPT8000");

                            if(TC[i].Open("COM7", "38400", "None"))
                            {
                                tool.SaveHistoryToFile("TC Connect");
                                IsConnect = true;
                            }
                            else
                            {
                                tool.SaveHistoryToFile("TC " + "COM? " + "Connect Fail");
                                ErrorMsg = "TC " + "COM? " + "Connect Fail";
                            }
                        }

                        Transition(WORK.IDLE);
                        break;

                    case WORK.CLOSE:
                        if (IsConnect == false)
                        {
                            tool.SaveHistoryToFile("TC未連線");
                            ErrorMsg = "TC " + "COM? " + "DisConnect Fail";
                            Transition(WORK.IDLE);                            
                            break;
                        }

                        for (int i = 0; i < 1; i++)
                        {
                            if(TC[i].Close())
                            {
                                tool.SaveHistoryToFile("TC DisConnect");
                                IsConnect = false;
                            }
                            else
                            {
                                tool.SaveHistoryToFile("TC " + "COM? " + "DisConnect Fail");
                                ErrorMsg = "TC " + "COM? " + "DisConnect Fail";
                            }
                        }

                        Transition(WORK.IDLE);
                        break;
                    #endregion

                    #region 各別輸出指令控溫
                    case WORK.START:
                        if (IsConnect == false)
                        {
                            tool.SaveHistoryToFile("TC未連線");
                            ErrorMsg = "TC Start Fail";
                            Transition(WORK.IDLE);
                            break;
                        }

                        if (TC[0].Start())
                        {
                            tool.SaveHistoryToFile("TC Start");
                        }
                        else
                        {
                            tool.SaveHistoryToFile("TC Start Fail");
                            ErrorMsg = "TC Start Fail";
                        }

                        Transition(WORK.IDLE);
                        break;
                    #endregion

                    #region 全部一起下指令控溫
                    case WORK.START_ALL:
                        if (IsConnect == false)
                        {
                            tool.SaveHistoryToFile("TC未連線");
                            ErrorMsg = "TC Start Fail";
                            Transition(WORK.IDLE);
                            break;
                        }

                        int board_count = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_BoardCount);

                        for(int i=0; i< board_count; i++)
                        {
                            int ctrl = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_BoxNo1 + i);
                            string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_BoxCh1 + i);
                            int temp = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_TargetT1 + i);
                            
                            if (TC[0].Start(ctrl,ch,temp))
                            {
                                tool.SaveHistoryToFile("TC Start All");
                            }
                            else
                            {
                                tool.SaveHistoryToFile($"TC T{ctrl}C{ch} Start Fail");
                                ErrorMsg = "TC Start Fail";
                            }
                        }

                        Transition(WORK.IDLE);
                        break;
                    #endregion

                    case WORK.STOP:
                        if (IsConnect == false)
                        {
                            tool.SaveHistoryToFile("TC未連線");
                            ErrorMsg = "TC Stop Fail";
                            Transition(WORK.IDLE);
                            break;
                        }

                        if (TC[0].Stop())
                        {
                            tool.SaveHistoryToFile("TC Stop");
                        }
                        else
                        {
                            tool.SaveHistoryToFile("TC Stop Fail");
                            ErrorMsg = "TC Stop Fail";
                        }

                        Transition(WORK.IDLE);
                        break;

                }
            }
        }
    }
}
