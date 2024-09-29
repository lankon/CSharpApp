using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommonFunction;

namespace InstrumentTest
{
    public class Task_TemperatureController
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
            MONITOR_ALL,
            ASK_PV_ALL,
            ANS_PV_ALL,

            ERROR,
        }

        public UpdatePresentValueCallBack UpdatePresentValue { get; set; }
        public UpdateSetValueCallBack UpdateSetValue { get; set; }
        public GetBoardRTDCallBack GetBoardRTD { get; set; }


        Tool tool = new Tool();
        private bool Terminate = true;  //Thread是否停止
        private bool IsConnect = false; //確認連線
        private bool IsMonitorAll = false;  //判斷是否啟動所有控制器
        private bool IsUseCycleTest = false;  //判斷是否使用循環測試
        private int delay_count = 0;
        private int board_num = 0;
        private WORK state = WORK.INITIAL;
        private string ErrorMsg = "";
        private string present_temp_value = "-99";
        private string temp_5_rtd = "-99.0,-99.0,-99.0,-99.0,-99.0";
        private string stc_type = "";
        private string sbaudrate = "";
        private string sparity = "";
        private string scomport = "";
        //ITemperatureController[] TC = new ITemperatureController[4];
        BaseTemperatureController[] TC = new BaseTemperatureController[4];
        #endregion

        #region private function
        /// <summary>
        /// 建立TC物件。
        /// </summary>
        /// <returns>BaseTemperatureController。</returns>
        private BaseTemperatureController Create_TC(string Type)
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
            int itc_type = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_TC_Type);
            int ibaudrate = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_BaudRate);
            int iparity = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_Parity);
            int icomport = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.Cmbx_Comport);

            if (itc_type == 0)
                stc_type = "TPT8000";

            if (ibaudrate == 0)
                sbaudrate = "1200";
            else if (ibaudrate == 1)
                sbaudrate = "2400";
            else if (ibaudrate == 2)
                sbaudrate = "4800";
            else if (ibaudrate == 3)
                sbaudrate = "9600";
            else if (ibaudrate == 4)
                sbaudrate = "14400";
            else if (ibaudrate == 5)
                sbaudrate = "19200";
            else if (ibaudrate == 6)
                sbaudrate = "38400";

            if (iparity == 0)
                sparity = "Odd";
            else if (iparity == 1)
                sparity = "Even";
            else if (iparity == 2)
                sparity = "None";

            scomport = $"COM{icomport + 1}";

            

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
        public void StopAll()
        {
            ResetError();
            Transition(WORK.STOP_ALL);
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
        public void MonitorAll(bool flag)
        {
            IsMonitorAll = flag;
        }
        public void UseCycleTest(bool flag)
        {
            IsUseCycleTest = flag;
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
                        

                        break;
                    #region Idle
                    case WORK.IDLE:
                        if (IsConnect)
                        {
                            if (IsMonitorAll)
                            {
                                state = WORK.MONITOR_ALL;

                                GetBoardRTD(temp_5_rtd, board_num);

                                if (board_num >= ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_BoardCount))
                                    board_num = 0;
                                else
                                    board_num++;
                            }
                            else
                            {
                                double value = -99;

                                if (present_temp_value == "error")
                                {
                                    
                                }
                                else
                                {
                                    value = tool.StringToDouble(present_temp_value);
                                }
                                
                                UpdatePresentValue(value);
                                state = WORK.MONITOR;
                            }
                        }
                        break;
                    #endregion

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

                    #region Monitor All
                    case WORK.MONITOR_ALL:
                        //Transition(WORK.ASK_PV);
                        state = WORK.ASK_PV_ALL;
                        ResetTimeCount(out delay_count);
                        break;

                    case WORK.ASK_PV_ALL:
                        if (CheckTimeOverMilliSec(delay_count, 50))
                        {
                            int ctrl = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_BoxNo1 + board_num);
                            string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_BoxCh1 + board_num);
                            int temp = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_TargetT1 + board_num);

                            TC[0].AskPV(ctrl, ch);
                            ResetTimeCount(out delay_count);
                            //Transition(WORK.ANS_PV);
                            state = WORK.ANS_PV_ALL;
                        }
                        break;

                    case WORK.ANS_PV_ALL:
                        if (CheckTimeOverMilliSec(delay_count, 100))
                        {
                            TC[0].GetAns();
                            temp_5_rtd = TC[0].GetFivePointValue();
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
                            TC[i] = Create_TC(stc_type);

                            if(TC[i].Open(scomport, sbaudrate, sparity))
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

                            if (IsUseCycleTest == true)
                            {
                                
                                temp = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_CT_T1);
                            }
                                
                            
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

                    #region 各別停止控溫
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
                    #endregion

                    #region 全部一起下指令停止控溫
                    case WORK.STOP_ALL:
                        if (IsConnect == false)
                        {
                            tool.SaveHistoryToFile("TC未連線");
                            ErrorMsg = "TC Start Fail";
                            Transition(WORK.IDLE);
                            break;
                        }

                        int count = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_BoardCount);

                        for (int i = 0; i < count; i++)
                        {
                            int ctrl = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_BoxNo1 + i);
                            string ch = ApplicationSetting.Get_String_Recipe((int)eFormAppSet.TxtBx_BoxCh1 + i);

                            if (TC[0].Stop(ctrl, ch))
                            {
                                tool.SaveHistoryToFile("TC Stop All");
                            }
                            else
                            {
                                tool.SaveHistoryToFile($"TC T{ctrl}C{ch} Stop Fail");
                                ErrorMsg = "TC Stop Fail";
                            }
                        }

                        Transition(WORK.IDLE);
                        break;
                    #endregion

                }
            }
        }
    }
}
