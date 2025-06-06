using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;

using CommonFunction;


namespace FileTransform.Recursion
{
    public class Task_Recursion
    {
        #region parameter define 
        enum WORK
        {
            INITIAL,
            IDLE,

            TEACH,
            WAIT_TEACH,

            BATCH_CALCULATE,
            WAIT_BATCH_CALCULATE,

            ERROR,

            PAUSE,
            CONTINUE,
        }

        SubTask_Recursion_Teach Teach;
        SubTask_Recursion_Batch Batch;

        private WORK state = WORK.INITIAL;
        private WORK temp_state;
        private bool Terminate = true;  //Thread是否停止
        private bool IsPause = false;   //判斷是否需要暫停
        private int cal_count = 0;          //計算Batch執行次數
        private string ErrorMsg = "";
        public ShowImageCallBack ShowImage { get; set; }
        public ShowNameCallBack ShowName { get; set; }
        #endregion

        #region private function
        private void showimage(string path)
        {
            ShowImage("");
        }
        private void showname(string name)
        {
            ShowName(name);
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
                Tool.SaveHistoryToFile("(Task_NearField):"+ target.ToString());
            }

            state = target;
        }
        private void CheckResult(bool res, WORK target)
        {
            if(res == true)
            {
                if (target != state) //狀態有變化時紀錄
                {
                    Tool.SaveHistoryToFile("(Task_Recursion):" + target.ToString());
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
        public void Process_Teach()
        {
            Transition(WORK.TEACH);
        }
        public void Process_Continue()
        {
            Transition(WORK.CONTINUE);
        }
        public void Process_Bath()
        {
            Transition(WORK.BATCH_CALCULATE);
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

        public Task_Recursion()
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
                        Teach = null;
                        cal_count = 0;
                        break;

                    #region 影像教學
                    case WORK.TEACH:
                        {
                            Teach = new SubTask_Recursion_Teach();
                            Teach.ShowImage += showimage;
                            Transition(WORK.WAIT_TEACH);
                        }
                        break;
                    case WORK.WAIT_TEACH:
                        {
                            bool check = Teach.Run();

                            GoToPause(WORK.WAIT_TEACH);
                            CheckResult(check, WORK.IDLE);
                        }
                        break;
                    #endregion

                    #region 批次計算
                    case WORK.BATCH_CALCULATE:
                        {
                            Batch = new SubTask_Recursion_Batch();
                            Batch.SetCount(cal_count);
                            Batch.ShowImage += showimage;
                            Batch.ShowName += showname;

                            Transition(WORK.WAIT_BATCH_CALCULATE);
                        }
                        break;
                    case WORK.WAIT_BATCH_CALCULATE:
                        {
                            bool check = Batch.Run();

                            //GoToPause(WORK.WAIT_TEACH);
                            if(cal_count == Scope.batch_path.Count-1)
                                CheckResult(check, WORK.IDLE);
                            else
                            {
                                CheckResult(check, WORK.BATCH_CALCULATE);
                                
                                if(check == true)
                                {
                                    if (IsPause)
                                        GoToPause(WORK.BATCH_CALCULATE);

                                    cal_count++;
                                }
                                    
                            }

                            
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
