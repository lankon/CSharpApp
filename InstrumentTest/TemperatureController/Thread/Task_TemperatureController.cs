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

            ERROR,
        }

        public UpdatePresentValueCallBack UpdatePresentValue { get; set; }
        public UpdateSetValueCallBack UpdateSetValue { get; set; }

        Tool tool = new Tool();
        private bool Terminate = true;
        private bool IsConnect = false;
        private WORK state = WORK.INITIAL;
        private string ErrorMsg = "";
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
                        #region 顯示PV SV
                        //UpdatePresentValue(12.0);
                        #endregion




                        break;

                    case WORK.CONNECT:
                        for (int i = 0; i < 1; i++)
                        {
                            TC[i] = Create_TC("TPT8000");

                            if(TC[i].Open("COM1", "19200", "1"))
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

                    case WORK.START_ALL:
                        if (IsConnect == false)
                        {
                            tool.SaveHistoryToFile("TC未連線");
                            ErrorMsg = "TC Start Fail";
                            Transition(WORK.IDLE);
                            break;
                        }

                        int ctrl_box_count = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_CtrlBxCount);
                        int board_count = ApplicationSetting.Get_Int_Recipe((int)eFormAppSet.TxtBx_BoardCount);

                        for(int i=0; i< ctrl_box_count; i++)
                        {
                            for(int j=0; j< 2; j++)
                            {
                                ApplicationSetting.SetRecipe((int)eFormAppSet.Cmbx_CtrlBox, i.ToString());
         
                                if(j == 0)
                                {
                                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Board_CH, j.ToString());
                                }
                                else
                                {
                                    ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Board_CH, (j+1).ToString());
                                }
                                
                                if (TC[0].Start())
                                {
                                    tool.SaveHistoryToFile("TC Start All");
                                }
                                else
                                {
                                    tool.SaveHistoryToFile("TC Start Fail");
                                    ErrorMsg = "TC Start Fail";
                                    //Transition(WORK.IDLE);
                                    //break;
                                }
                            }
                        }

                        Transition(WORK.IDLE);
                        break;

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
