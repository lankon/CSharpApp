using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommonFunction;

namespace FileTransform
{
    public class Task_NearField
    {
        #region parameter define 
        enum WORK
        {
            INITIAL,
            IDLE,

            TEACH,
            WAIT_TEACH,

            ERROR,

            PAUSE,
            CONTINUE,
        }

        //public UpdatePresentValueCallBack UpdatePresentValue { get; set; }
        //public UpdateSetValueCallBack UpdateSetValue { get; set; }
        //public GetBoardRTDCallBack GetBoardRTD { get; set; }

        SubTask_Teach Teach; 

        Tool tool = new Tool();
        private WORK state = WORK.INITIAL;
        private WORK temp_state;
        private bool Terminate = true;  //Thread是否停止
        //private bool IsConnect = false; //確認連線
        //private bool IsMonitorAll = false;  //判斷是否啟動所有控制器
        //private int delay_count = 0;
        //private int board_num = 0;
        private string ErrorMsg = "";
        //private string present_temp_value = "-99";
        //private string temp_5_rtd = "-99.0,-99.0,-99.0,-99.0,-99.0";
        //private string stc_type = "";
        //private string sbaudrate = "";
        //private string sparity = "";
        //private string scomport = "";
        //ITemperatureController[] TC = new ITemperatureController[4];
        public ShowImageCallBack ShowImage { get; set; }
        #endregion

        #region private function
        private void showimage(string path)
        {
            ShowImage("");
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
                tool.SaveHistoryToFile("(Task_NearField):"+ target.ToString());
            }

            state = target;
        }
        private void CheckResult(bool res, WORK target)
        {
            if(res == true)
            {
                if (target != state) //狀態有變化時紀錄
                {
                    tool.SaveHistoryToFile("(Task_NearField):" + target.ToString());
                }

                state = target;
            }
        }
        private void GoToPause(WORK current_state)
        {
            temp_state = current_state;
            state = WORK.PAUSE;
        }
        #endregion

        #region public function
        public void Process_Teach()
        {
            Transition(WORK.TEACH);
        }
        public void Process_Continue()
        {
            Transition(WORK.CONTINUE);
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

        public Task_NearField()
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

                    case WORK.IDLE:

                        break;

                    #region 近場教學
                    case WORK.TEACH:
                        {
                            Teach = new SubTask_Teach();
                            Teach.ShowImage += showimage;
                            Transition(WORK.WAIT_TEACH);
                        }
                        break;
                    case WORK.WAIT_TEACH:
                        {
                            bool check = Teach.Run();

                            //GoToPause(WORK.WAIT_TEACH);
                            CheckResult(check, WORK.IDLE);
                        }
                        break;
                    #endregion

                    #region 暫停/繼續
                    case WORK.PAUSE:
                        {

                        }
                        break;

                    case WORK.CONTINUE:
                        {
                            Transition(temp_state);
                        }
                        break;
                    #endregion



                }
            }
        }
    }
}
