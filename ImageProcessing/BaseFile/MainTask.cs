using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommonFunction;


namespace ImageProcessing
{
    public class MainTask
    {
        #region parameter
        private bool Terminate = false;
        public IBaseTask base_task;
        private TASK_STATUS status_commad = TASK_STATUS.NONE;
        private WORK state = WORK.INITIAL;
        private WORK pre_state = WORK.INITIAL;
        public UpdateTaskStateCallBack UpdateTaskState { get; set; }
        public SetPauseAbortContinueCallBack SetPauseAbortContinue { get; set; }
        public SetErrorMsgCallBack SetErrorMsg { get; set; }
        enum WORK
        {
            INITIAL,
            IDLE,

            TASK,
            WAIT_TASK,

            END,

            SUCCESS,
            FAIL,

            PAUSE,
            ABORT,
            CONTINUE
        }
        #endregion

        #region private function
        /// <summary>
        /// 變換狀態
        /// </summary>
        private void Transition(WORK target)
        {
            if (target != state) //狀態有變化時紀錄
            {
                //UpdateTaskState("[Process]" + target.ToString());
                Tool.SaveHistoryToFile("[Process](MainTask)" + target.ToString());
            }

            state = target;
        }
        /// <summary>
        /// 確認Task狀態
        /// </summary>
        /// <param name="check"></param>
        private void CheckResult(TASK_STATUS check)
        {
            switch(check)
            {
                case TASK_STATUS.SUCCESS:
                    {
                        SetPauseAbortContinue(TASK_STATUS.SUCCESS);
                        Transition(WORK.IDLE);
                    }
                    break;
                case TASK_STATUS.PAUSE:
                    {
                        pre_state = state;
                        Transition(WORK.PAUSE);
                    }
                    break;
                case TASK_STATUS.ABORT:
                    {
                        Transition(WORK.ABORT);
                    }
                    break;
                case TASK_STATUS.CONTINUE:
                    {
                        
                    }
                    break;
                case TASK_STATUS.FAIL:
                    {
                        Transition(WORK.FAIL);
                    }
                    break;
            }

        }
        /// <summary>
        /// 設定狀態命令
        /// </summary>
        /// <param name="task_status"></param>
        private void SetStatusCommand(TASK_STATUS task_status)
        {
            status_commad = task_status;
        }
        /// <summary>
        /// 取得狀態命令
        /// </summary>
        /// <returns></returns>
        private TASK_STATUS GetStatusCommand()
        {
            TASK_STATUS temp_status;
            temp_status = status_commad;
            status_commad = TASK_STATUS.NONE;

            return temp_status;
        }


        private void ResetTimeCount(out int tick)
        {
            tick = Environment.TickCount;
        }
        private bool CheckTimeOverSec(int tick, int time)
        {
            var time_count = Environment.TickCount - tick;
            bool res = time_count > time;

            return res;
        }
        #endregion

        #region public function
        /// <summary>
        /// 設定欲執行Task物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        public void SetTask<T>(string method = "Default") where T : IBaseTask
        {
            if (state != WORK.IDLE)
            {
                Tool.SaveHistoryToFile("Thread No Ready");
                return;
            }

            base_task = (IBaseTask)Activator.CreateInstance(typeof(T), method);
        }
        /// <summary>
        /// 執行Task
        /// </summary>
        public void Run()
        {
            Transition(WORK.TASK);
        }
        /// <summary>
        /// 暫停Task執行
        /// </summary>
        public void GoToPause()
        {
            pre_state = state;

            TASK_STATUS status = base_task.GoToPause();
            SetPauseAbortContinue(status);  //設定使用者可點選UI介面

            Transition(WORK.PAUSE);
        }
        /// <summary>
        /// 放棄Task執行
        /// </summary>
        public void GoToAbort()
        {
            SetStatusCommand(TASK_STATUS.ABORT);
            SetPauseAbortContinue(TASK_STATUS.NONE);
            Transition(pre_state);
        }
        /// <summary>
        /// 繼續Task執行
        /// </summary>
        public void GoToContinue()
        {
            SetPauseAbortContinue(base_task.SetUserCtrl());
            SetStatusCommand(TASK_STATUS.CONTINUE);
            Transition(pre_state);
        }
        public void ForceAction()
        {
            Transition(WORK.IDLE);
        }
        #endregion

        public MainTask()
        {
            Task task = Task.Run(() => Process());
        }

        private void Process()
        {
            while (!Terminate)
            {
                switch(state)
                {
                    case WORK.INITIAL:
                        {
                            state = WORK.IDLE;
                        }
                        break;
                    case WORK.IDLE: //待命
                        {
                            
                        }
                        break;
                    case WORK.TASK:
                        {
                            SetPauseAbortContinue(base_task.SetUserCtrl());
                            SetStatusCommand(TASK_STATUS.NONE);
                            SetErrorMsg("Error Msg");
                            Transition(WORK.WAIT_TASK);
                        }
                        break;
                    case WORK.WAIT_TASK:    //等待Task執行完畢確認結果
                        {
                            TASK_STATUS check = base_task.Run(GetStatusCommand());
                            CheckResult(check);
                        }
                        break;
                    case WORK.FAIL:
                        {
                            //SetErrorMsg(base_task.GetErrorMsg());
                            SetPauseAbortContinue(TASK_STATUS.FAIL);
                            Transition(WORK.IDLE);
                        }
                        break;
                    case WORK.PAUSE:
                        {
                            
                        }
                        break;
                    case WORK.ABORT:
                        {
                            Transition(WORK.IDLE);
                        }
                        break;
                }

                Thread.Sleep(1);
            }
        }
    } 
}
