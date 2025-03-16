using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


using CommonFunction;
using System.Windows.Forms;

namespace FileTransform.Wafer_Align_Angle
{
    public class Task_AngleCalculate:IBaseTask
    {
        #region parameter
        private string error_msg;
        private bool IsSubTaskProcessing = false;
        private bool TerminateClient = false;
        private int task_delay = 0;
        private int delay_time = 2;
        Tool tool = new Tool();
        TCPIP_Server tCPIP_Server = new TCPIP_Server();
        SubTask_AngleCalculate Calculate;
        private WORK state = WORK.INITIAL;
        private WORK next_state = WORK.NONE;
        private TASK_STATUS status_commad = TASK_STATUS.NONE;
        private TASK_STATUS pre_status_commad = TASK_STATUS.NONE;
        private TASK_STATUS status = TASK_STATUS.CONTINUE;
        private F_Wafer_Align_Angle f_Wafer_Align_Angle;
        public override UpdateTaskStateCallBack UpdateTaskState { get; set; }
        public override SetErrorMsgCallBack SetErrorMsg { get; set; }
        public override SetPauseAbortContinueCallBack SetPauseAbortContinue { get; set; }
        enum WORK
        {
            NONE,
            INITIAL,
            IDLE,

            WAFER_ALIGN,
            WAIT_WAFER_ALIGN,

            END,

            SUCCESS,
            FAIL,

            PAUSE,
            ABORT,
            CONTINUE,
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
                tool.SaveHistoryToFile("[Task](Task_AngleCalculate)" + target.ToString());
                UpdateTaskState("[Task](Task_AngleCalculate)" + target.ToString());
            }

            state = target;
        }
        private void SetNextState(WORK target)
        {
            next_state = target;
        }
        private WORK GetNextState()
        {
            return next_state;
        }
        private void GoToNextState()
        {
            Transition(next_state);
        }
        /// <summary>
        /// 確認Task狀態
        /// </summary>
        /// <param name="check"></param>
        protected override void CheckResult(TASK_STATUS check)
        {
            switch (check)
            {
                case TASK_STATUS.SUCCESS:
                    {
                        Transition(WORK.SUCCESS);
                    }
                    break;
                case TASK_STATUS.PAUSE:
                    {
                        SetStatus(TASK_STATUS.PAUSE);
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
        /// 設定Task狀態
        /// </summary>
        /// <param name="st"></param>
        protected override void SetStatus(TASK_STATUS st)
        {
            status = st;
        }
        /// <summary>
        /// 設定是否有SubTask執行
        /// </summary>
        /// <param name="is_processing"></param>
        protected override void SetSubTaskProcessing(bool is_processing)
        {
            IsSubTaskProcessing = is_processing;
        }
        /// <summary>
        /// 取得是否有執行SubTask
        /// </summary>
        /// <returns></returns>
        protected override bool GetSubTaskProcessing()
        {
            return IsSubTaskProcessing;
        }
        /// <summary>
        /// 設定狀態命令
        /// </summary>
        /// <param name="task_status"></param>
        protected override void SetStatusCommand(TASK_STATUS task_status)
        {
            status_commad = task_status;
        }
        /// <summary>
        /// 取得狀態命令
        /// </summary>
        /// <returns></returns>
        protected override TASK_STATUS GetStatusCommand()
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
            bool res = time_count > time * 1000;

            return res;
        }
        #endregion

        #region public function
        /// <summary>
        /// 執行Task並回傳Task狀態
        /// </summary>
        /// <param name="task_status"></param>
        /// <returns></returns>
        public override TASK_STATUS Run(TASK_STATUS task_status = TASK_STATUS.NONE)
        {
            if (status == TASK_STATUS.CONTINUE || task_status != TASK_STATUS.NONE)
            {
                RunLoop(task_status);
            }

            return status;
        }       
        /// <summary>
        /// 放棄Task執行
        /// </summary>
        public override void GoToAbort()
        {
            Transition(WORK.ABORT);
        }
        /// <summary>
        /// 暫停Task
        /// </summary>
        /// <returns></returns>
        public override TASK_STATUS GoToPause()
        {
            //執行暫停後可根據Task狀況選擇要回傳
            //ABORT_CONTINUE
            //ABORT
            //CONTINUE
            //讓使用者選擇
                       
            return TASK_STATUS.ABORT_CONTINUE;
        }
        /// <summary>
        /// 設定Task可讓人員操作的選項
        /// </summary>
        /// <returns></returns>
        public override TASK_STATUS SetUserCtrl()
        {
            return TASK_STATUS.PAUSE;
        }
        public override void SetForm(Form form)
        {
            if(form.Name == "F_Wafer_Align_Angle")
            {
                f_Wafer_Align_Angle = form as F_Wafer_Align_Angle; // 嘗試轉型
                if (f_Wafer_Align_Angle == null)
                {
                    tool.SaveHistoryToFile("F_Wafer_Align_Angle轉型失敗");
                }
            }
        }
        #endregion

        public Task_AngleCalculate(string set_state = "Default")
        {
            switch (set_state)
            {
                case "Default":
                    {
                        state = WORK.INITIAL;
                        ResetTimeCount(out task_delay);
                    }
                    break;
            }
        }

        private void RunLoop(TASK_STATUS task_status)
        {
            if (task_status == TASK_STATUS.ABORT)   //人員傳入ABORT命令
            {
                if (GetSubTaskProcessing()) //判斷是否有執行SubTask
                    SetStatusCommand(task_status);
                else
                    Transition(WORK.ABORT);
            }
            else if (task_status == TASK_STATUS.CONTINUE)  //人員傳入CONTINUE命令
            {
                //SetStatusCommand(task_status);
                Transition(WORK.CONTINUE);
            }

            switch (state)
            {
                case WORK.INITIAL:
                    {
                        //預設進入執行case
                        Transition(WORK.WAFER_ALIGN);
                    }
                    break;

                case WORK.IDLE:
                    {
                    }
                    break;

                case WORK.WAFER_ALIGN:
                    {
                        Calculate = new SubTask_AngleCalculate();
                        Calculate.SetForm(f_Wafer_Align_Angle);
                        SetSubTaskProcessing(true);
                        Transition(WORK.WAIT_WAFER_ALIGN);
                    }
                    break;
                case WORK.WAIT_WAFER_ALIGN:
                    {
                        TASK_STATUS check = Calculate.Run(GetStatusCommand());
                        CheckResult(check);

                        if (check == TASK_STATUS.PAUSE)
                            SetNextState(WORK.WAIT_WAFER_ALIGN);
                    }
                    break;
                
                case WORK.SUCCESS:
                    {
                        SetStatus(TASK_STATUS.SUCCESS);
                    }
                    break;
                case WORK.FAIL:
                    {
                        if (CheckTimeOverSec(task_delay, delay_time))
                        {
                            SetStatus(TASK_STATUS.FAIL);
                        }
                    }
                    break;

                case WORK.PAUSE:
                    {

                    }
                    break;

                case WORK.ABORT:
                    {
                        SetStatus(TASK_STATUS.ABORT);
                    }
                    break;

                case WORK.CONTINUE:
                    {
                        if (GetNextState() != WORK.NONE)
                            GoToNextState();

                        SetStatus(TASK_STATUS.CONTINUE);
                        SetStatusCommand(TASK_STATUS.CONTINUE);
                    }
                    break;

                case WORK.END:
                    {
                        if (CheckTimeOverSec(task_delay, delay_time))
                        {
                            SetStatus(TASK_STATUS.SUCCESS);
                        }
                    }
                    break;
            }
        }
    }
}
