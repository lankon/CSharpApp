using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using CommonFunction;

namespace FileTransform.Wafer_Align_Angle
{
    public class Task_Wafer_Align_Angle
    {
        #region parameter define 
        enum WORK
        {
            INITIAL,
            IDLE,

            CALCULATE,
            WAIT_CALCULATE,

            ERROR,

            PAUSE,
            CONTINUE,
        }

        SubTask_Angle_Calculate Calculate;

        Tool tool = new Tool();
        private WORK state = WORK.INITIAL;
        private WORK temp_state;
        private bool Terminate = true;  //Thread是否停止
        private bool IsPause = false;   //判斷是否需要暫停
        private int cal_count = 0;          //計算Batch執行次數
        private string ErrorMsg = "";
        public ShowImageCallBack ShowImage { get; set; }
        //public ShowNameCallBack ShowName { get; set; }
        #endregion

        #region private function
        private void showimage(string path)
        {
            ShowImage("");
        }
        //private void showname(string name)
        //{
        //    ShowName(name);
        //}
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
                tool.SaveHistoryToFile("[Task](Task_NearField):" + target.ToString());
            }

            state = target;
        }
        private void CheckResult(bool res, WORK target)
        {
            if (res == true)
            {
                if (target != state) //狀態有變化時紀錄
                {
                    tool.SaveHistoryToFile("(Task_Recursion):" + target.ToString());
                }

                state = target;
            }
        }
        private void GoToPause(WORK current_state)
        {
            temp_state = current_state;
            state = WORK.PAUSE;
            IsPause = false;
        }
        #endregion

        #region public function
        public void Process_Calculate()
        {
            Transition(WORK.CALCULATE);
        }
        public void Process_Continue()
        {
            Transition(WORK.CONTINUE);
        }
        public void Process_Pause()
        {
            IsPause = true;
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

        public Task_Wafer_Align_Angle()
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
                        Calculate = null;
                        cal_count = 0;
                        break;

                    #region 計算角度
                    case WORK.CALCULATE:
                        Calculate = new SubTask_Angle_Calculate();
                        Calculate.ShowImage += showimage;
                        Transition(WORK.WAIT_CALCULATE);
                        break;

                    case WORK.WAIT_CALCULATE:
                        bool check = Calculate.Run();

                        if(ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_ServerMode) == 0)
                        {
                            GoToPause(WORK.WAIT_CALCULATE);
                        }
                      
                        CheckResult(check, WORK.IDLE);
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

                Thread.Sleep(1);
                
            }
        }

    }
}
