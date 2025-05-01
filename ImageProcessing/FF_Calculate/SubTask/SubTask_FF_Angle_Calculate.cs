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
        public override UpdateTaskStateCallBack UpdateTaskState { get; set; }
        public override SetErrorMsgCallBack SetErrorMsg { get; set; }

        enum WORK
        {
            NONE,
            INITIAL,
            IDLE,


            LOAD_IMAGE,
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
            if (form.Name == "F_Wafer_Align_Angle")
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
                        Transition(WORK.LOAD_IMAGE);
                    }
                    break;

                case WORK.LOAD_IMAGE:
                    {
                        string path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
                        image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);

                        if (image.Empty())
                        {
                            tool.SaveHistoryToFile("影像不存在");
                            Transition(WORK.SUCCESS);
                            break;
                        }

                        using (Mat dst = new Mat())
                        {
                            Cv2.Normalize(image, dst, 0, 255, NormTypes.MinMax, MatType.CV_8U); //16位元轉8位元
                            Cv2.ImWrite(Save_Path, dst);  //儲存圖像
                            f_FF_Calculate.ShowImage(Save_Path);
                        }

                        if (IsServerMode)
                        {
                            Transition(WORK.THRESHOLD_IMAGE);
                        }
                        else
                        {
                            SetNextState(WORK.THRESHOLD_IMAGE);
                            SetStatus(TASK_STATUS.PAUSE);
                            Transition(WORK.PAUSE);
                        }
                    }
                    break;

                case WORK.THRESHOLD_IMAGE:
                    {
                        double LowThreshold = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_EdgeLowThreshold);
                        double Threshold = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_EdgeThreshold);

                        //閥值處理,消除雜訊,凸顯邊緣
                        Cv2.Canny(image, image, LowThreshold, Threshold);

                        // 創建一個彩色圖像用於顯示結果
                        Cv2.CvtColor(image, outputImage, ColorConversionCodes.GRAY2BGR);

                        Cv2.ImWrite(Save_Path, outputImage);

                        //顯示圖像
                        f_FF_Calculate.ShowImage(Save_Path);

                        if (IsServerMode)
                        {
                            Transition(WORK.FIND_RECTANGLE);
                        }
                        else
                        {
                            SetNextState(WORK.FIND_RECTANGLE);
                            SetStatus(TASK_STATUS.PAUSE);
                            Transition(WORK.PAUSE);
                        }
                    }
                    break;
                case WORK.FIND_RECTANGLE:
                    {
                        double Angle = 0.0; //Chip偏轉角度
                        double AngleCheck = 0.0;
                        double ChipWidth = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_ChipWidth);
                        double ChipHeigh = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_ChipHeigh);
                        double XPitch = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelPitchX);
                        double YPitch = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelPitchY);
                        int CorrectCount = 0;
                        double W_Pixel = 0.0;
                        double H_Pixel = 0.0;
                        try
                        {
                            if (Math.Abs(XPitch - 0) > 0.01 && Math.Abs(XPitch - 0) > 0.01)
                            {
                                W_Pixel = ChipWidth / XPitch;
                                H_Pixel = ChipHeigh / YPitch;
                            }
                        }
                        catch (Exception ex)
                        {
                            tool.SaveHistoryToFile($"{ex}");
                        }

                        // 找到輪廓
                        Point[][] contours;
                        HierarchyIndex[] hierarchy;
                        Cv2.FindContours(image, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                        foreach (var contour in contours)
                        {
                            // 擬合最小外接矩形
                            RotatedRect rotatedRect = Cv2.MinAreaRect(contour);

                            if (Math.Abs(rotatedRect.Size.Width - W_Pixel) <= 10 && Math.Abs(rotatedRect.Size.Height - H_Pixel) <= 10)
                            {
                                Angle = rotatedRect.Angle;

                                //// 修正角度範圍
                                //if (Angle < -45)
                                //{
                                //    Angle += 90; // 保證角度範圍在 [-45, 45] 之間
                                //}

                                if (CorrectCount == 0)
                                {
                                    AngleCheck = Angle;
                                    CorrectCount++;
                                }
                                else if (Math.Abs(AngleCheck - Angle) < 0.5)
                                {
                                    //計算找到的晶粒偏轉角度相同次數
                                    CorrectCount++;
                                }


                                // 獲取矩形的四個頂點
                                Point2f[] boxPoints = rotatedRect.Points();
                                Point[] intPoints = Array.ConvertAll(boxPoints, point => new Point((int)Math.Round(point.X), (int)Math.Round(point.Y)));
                                // 繪製方型
                                Cv2.Polylines(outputImage, new[] { intPoints }, true, Scalar.Red, 5);
                            }
                        }

                        Cv2.ImWrite(Save_Path, outputImage);
                        f_FF_Calculate.ShowImage(Save_Path);

                        if (CorrectCount <= 3)
                            ShowMessage("Find Angle Error");
                        else
                            ShowMessage($"Angle:{Angle.ToString("0.00")}");

                        Scope.WaferAngle = Angle;

                        Transition(WORK.SUCCESS);
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
