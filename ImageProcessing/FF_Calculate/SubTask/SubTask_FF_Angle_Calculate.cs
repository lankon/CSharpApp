using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

using CommonFunction;

namespace ImageProcessing.FF_Calculate
{
    class SubTask_FF_Angle_Calculate:IBaseTask
    {
        #region parameter
        private int task_delay = 0;
        private int delay_time = 2;
        private int record_test_time = 0;
        private bool IsSubTaskProcessing = false;
        private bool IsServerMode = false;
        private string Save_Path = Application.StartupPath + @"\Picture\" + "Calculate.png";
        private WORK state = WORK.INITIAL;
        private WORK next_state = WORK.NONE;
        private WORK pre_state = WORK.NONE;
        private TASK_STATUS status_commad = TASK_STATUS.NONE;
        private TASK_STATUS pre_status_commad = TASK_STATUS.NONE;
        private TASK_STATUS status = TASK_STATUS.CONTINUE;
        private Mat image;
        Mat outputImage = new Mat();
        Tool tool = new Tool();
        private F_FF_Calculate f_FF_Calculate;
        private FF_Algorithm FarField = new FF_Algorithm();
        public override UpdateTaskStateCallBack UpdateTaskState { get; set; }
        public override SetErrorMsgCallBack SetErrorMsg { get; set; }

        enum WORK
        {
            NONE,
            INITIAL,
            IDLE,


            LOAD_IMAGE,
            PRE_PROCESS,
            CALCULATE_DIAMETER,
            CALCULATE_FARFIELD_RESULT,

            GRAB_IMAGE,
            GRAB_ORRGIN,
            THRESHOLD_IMAGE,
            FIND_RECTANGLE,
            FIND_Circule,
            CALCULATE_DIST,


            END,

            SUCCESS,
            FAIL,

            PAUSE,
            ABORT,
            CONTINUE,
        }
        #endregion

        #region private function
        private void Transition(WORK target)
        {
            if (target != state) //狀態有變化時紀錄
            {
                tool.SaveHistoryToFile("[SubTask](SubTask_FF_Angle_Calculate)" + target.ToString());
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
        /// 設定Task狀態
        /// </summary>
        /// <param name="st"></param>
        protected override void SetStatus(TASK_STATUS st)
        {
            status = st;
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
        ///
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
            return status_commad;
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



        private void ResetTimeCount(out int tick)
        {
            tick = Environment.TickCount;
        }
        private int GetTimeCount(int tick)
        {
            int time_count = Environment.TickCount - tick;

            return time_count;
        }
        private bool CheckTimeOverSec(int tick, int time)
        {
            var time_count = Environment.TickCount - tick;
            bool res = time_count > time * 1000;

            return res;
        }
        private void ShowMessage(string msg)
        {
            if (IsServerMode == false)
                MessageBox.Show(msg);
            else
                tool.SaveHistoryToFile(msg);
        }
        private void ShowImageToForm(Mat image)
        {
            //顯示圖片
            if (image.Depth() == MatType.CV_16U)
            {
                Cv2.Normalize(image, outputImage, 0, 255, NormTypes.MinMax, MatType.CV_8U); //16位元轉8位元
                Cv2.ImWrite(Save_Path, outputImage);
            }
            else
            {
                Cv2.ImWrite(Save_Path, image);
            }

            f_FF_Calculate.ShowImage(Save_Path);
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
            if (form.Name == "F_FF_Calculate")
            {
                f_FF_Calculate = form as F_FF_Calculate; // 嘗試轉型
                if (f_FF_Calculate == null)
                {
                    tool.SaveHistoryToFile("F_FF_Calculate轉型失敗");
                }
            }
        }
        #endregion

        public SubTask_FF_Angle_Calculate(string set_state = "Default")
        {
            switch (set_state)
            {
                case "ServerMode":
                    {
                        IsServerMode = true;
                        state = WORK.INITIAL;
                    }
                    break;
                case "Default":
                    {
                        state = WORK.INITIAL;
                    }
                    break;
            }
            ResetTimeCount(out task_delay);
        }

        private void RunLoop(TASK_STATUS task_status)
        {
            if (task_status == TASK_STATUS.ABORT)   //人員傳入ABORT命令
            {
                //SetStatusCommand(task_status);
                //Transition(WORK.ABORT_PROCESS1);
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
                        //預設進入看要幹嘛
                        ResetTimeCount(out task_delay);
                        ResetTimeCount(out record_test_time);
                        Transition(WORK.LOAD_IMAGE);
                    }
                    break;

                case WORK.LOAD_IMAGE:
                    {
                        GetTimeCount(task_delay);
                        
                        string path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
                        image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);

                        if (image.Empty())
                        {
                            tool.SaveHistoryToFile("影像不存在");
                            Transition(WORK.SUCCESS);
                            break;
                        }

                        ShowImageToForm(image);

                        if (IsServerMode)
                        {
                            Transition(WORK.PRE_PROCESS);
                        }
                        else
                        {
                            SetNextState(WORK.PRE_PROCESS);
                            SetStatus(TASK_STATUS.PAUSE);
                            Transition(WORK.PAUSE);
                        }
                    }
                    break;

                case WORK.PRE_PROCESS:
                    {
                        FarField.PixelSize = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelSize);
                        FarField.TestHeigh = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_TestHeight);
                        
                        FarField.PreProcess(image);

                        FarField.ImageFiltering(image);

                        ShowImageToForm(image);

                        Transition(WORK.CALCULATE_DIAMETER);
                    }
                    break;
                case WORK.CALCULATE_DIAMETER:
                    {
                        FarField.Calculate_Half_Diameter(image);
                        FarField.Calculate_Diameter(image);

                        ShowImageToForm(image);

                        Transition(WORK.CALCULATE_FARFIELD_RESULT);
                    }
                    break;
                case WORK.CALCULATE_FARFIELD_RESULT:
                    {
                        FarField.Calculate_FarField_Result(angle: true);

                        outputImage = new Mat();
                        //FarField.CalculateEyeSafe(image, out outputImage);

                        //ShowImageToForm(outputImage);

                        Transition(WORK.SUCCESS);
                    }
                    break;

                case WORK.SUCCESS:
                    {
                        SetStatus(TASK_STATUS.SUCCESS);
                        
                        //顯示結果至畫面上
                        f_FF_Calculate.ShowFarFieldResult(angle:FarField.Angle);

                        GC.Collect();   //強制回收程式記憶體
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
                        //SaveHistoryCurrentState(WORK.ABORT);
                    }
                    break;

                case WORK.CONTINUE:
                    {
                        if (GetNextState() != WORK.NONE)
                            GoToNextState();

                        SetStatus(TASK_STATUS.CONTINUE);
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
