using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;


using CommonFunction;
using Tesseract;
using System.Diagnostics;

namespace FileTransform.Wafer_Align_Angle
{
    public class SubTask_AngleCalculate : IBaseTask
    {
        #region parameter
        private int task_delay = 0;
        private int delay_time = 2;
        private int batch_count = 0;    //Batch計算次數
        private bool IsSubTaskProcessing = false;
        private bool NonUserPause = false;
        private bool IsBatchCalculate = false;
        private string Save_Path = Application.StartupPath + @"\Picture\" + "Calculate.png";
        private string path;
        private double W_Pixel;
        private float W_Size = 0.0f;
        private WORK state = WORK.INITIAL;
        private WORK next_state = WORK.NONE;
        private WORK pre_state = WORK.NONE;
        private TASK_STATUS status_commad = TASK_STATUS.NONE;
        private TASK_STATUS pre_status_commad = TASK_STATUS.NONE;
        private TASK_STATUS status = TASK_STATUS.CONTINUE;
        private Mat image;
        private Mat outputImage = new Mat();
        private Mat rotationMatrix;
        private Mat translationMatrix;
        Tool tool = new Tool();
        OpenCvSharp.Rect boundingRect;
        RotatedRect rotatedRect;
        private List<Point[]> filteredContours = new List<Point[]>();
        private F_Wafer_Align_Angle f_Wafer_Align_Angle;
        public override UpdateTaskStateCallBack UpdateTaskState { get; set; }
        public override SetErrorMsgCallBack SetErrorMsg { get ; set; }

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

            ROTATE_IMAGE,
            CREATE_TECTANGLE,
            SHIFT_XY,

            PROCESS_IMAGE,
            GRAB_KEYWORD,
            CHECK_OCR,
            ROTATE_IMAGE_INVERSE,


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
                tool.SaveHistoryToFile("[SubTask](SubTaskCalculate)" + target.ToString());
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
            if (NonUserPause == false)
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
                f_Wafer_Align_Angle = form as F_Wafer_Align_Angle; // 嘗試轉型
                if (f_Wafer_Align_Angle == null)
                {
                    tool.SaveHistoryToFile("F_Wafer_Align_Angle轉型失敗");
                }
            }
        }

        public void SetBatchCount(int count)
        {
            batch_count = count;
        }
        #endregion

        public SubTask_AngleCalculate(string set_state = "Default")
        {
            switch (set_state)
            {
                case "ServerMode":
                    {
                        NonUserPause = true;
                        state = WORK.INITIAL;
                    }
                    break;
                case "Batch_Calculate":
                    {
                        NonUserPause = true;
                        IsBatchCalculate = true;
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
            else if(task_status == TASK_STATUS.CONTINUE)  //人員傳入CONTINUE命令
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
                        if (NonUserPause)
                            path = Scope.batch_path[batch_count];
                        else
                            path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);

                        image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);

                        if (image.Empty())
                        {
                            tool.SaveHistoryToFile("影像不存在");
                            Transition(WORK.SUCCESS);
                            break;
                        }

                        using(Mat dst = new Mat())
                        {
                            Cv2.Normalize(image, dst, 0, 255, NormTypes.MinMax, MatType.CV_8U); //16位元轉8位元
                            Cv2.ImWrite(Save_Path, dst);  //儲存圖像
                            
                            if(!IsBatchCalculate)
                                f_Wafer_Align_Angle.ShowImage(Save_Path);

                            dst.Dispose();
                        }

                        if(NonUserPause)
                        {
                            Transition(WORK.GRAB_IMAGE);
                        }
                        else
                        {
                            SetNextState(WORK.GRAB_IMAGE);
                            SetStatus(TASK_STATUS.PAUSE);
                            Transition(WORK.PAUSE);

                            Scope.status = 1;
                            MessageBox.Show("Capture Image");
                        }
                    }
                    break;
                case WORK.GRAB_IMAGE:
                    {
                        //定義範圍(x, y, width, height)
                        OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)Scope.start_xy[0],
                                            (int)Scope.start_xy[1],
                                            (int)Scope.len[0],
                                            (int)Scope.len[1]);

                        // 擷取圖像範圍
                        Mat cropped = new Mat(image, roi);
                        image.Dispose();
                        image = cropped;

                        state = WORK.THRESHOLD_IMAGE;
                        goto case WORK.THRESHOLD_IMAGE;
                    }
                case WORK.THRESHOLD_IMAGE:
                    {
                        // 二值化閥值處理
                        double thresholdValue = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_Threshold);
                        //double maxValue = 65535; //16位元影像,最大值65536
                        double maxValue = 255; //8位元影像,最大值256
                        thresholdValue = 100;
                        Cv2.Threshold(image, image, thresholdValue, maxValue, ThresholdTypes.Binary);

                        //將圖像轉換成8位元深度
                        //image.ConvertTo(image, MatType.CV_8UC1, 255.0 / 65535.0);

                        // 創建一個彩色圖像用於顯示結果
                        Cv2.CvtColor(image, outputImage, ColorConversionCodes.GRAY2BGR);
                        Cv2.ImWrite(Save_Path, image);

                        //顯示圖像
                        if (!IsBatchCalculate)
                            f_Wafer_Align_Angle.ShowImage(Save_Path);

                        Transition(WORK.FIND_RECTANGLE);

                        //MessageBox.Show("Capture Needle Image");
                        //Scope.status = 2;
                    }
                    break;
                case WORK.FIND_RECTANGLE:
                    {
                        #region 隱藏
                        double Angle = 0.0; //Chip偏轉角度
                        double AngleCheck = 0.0;
                        double ChipWidth = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_ChipWidth);
                        double ChipHeigh = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_ChipHeigh);
                        double XPitch = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelPitchX);
                        double YPitch = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelPitchY);
                        int CorrectCount = 0;
                        W_Pixel = 0.0;
                        double H_Pixel = 0.0;
                        double Mini_Pixel = 0.0;
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

                        if (W_Pixel > H_Pixel)
                            Mini_Pixel = H_Pixel;
                        else
                            Mini_Pixel = W_Pixel;

                        // 找到輪廓
                        Point[][] contours;
                        HierarchyIndex[] hierarchy;
                        Cv2.FindContours(image, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                        Point2f[] boxPoints;
                        Point[] intPoints;

                        foreach (var contour in contours)
                        {
                            // 擬合最小外接矩形
                            rotatedRect = Cv2.MinAreaRect(contour);

                            if (Math.Abs(rotatedRect.Size.Width - Mini_Pixel) <= 30 || Math.Abs(rotatedRect.Size.Height - Mini_Pixel) <= 30)
                            {
                                Angle = rotatedRect.Angle;

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

                                tool.SaveHistoryToFile($"Width = {rotatedRect.Size.Width}");
                                tool.SaveHistoryToFile($"Height = {rotatedRect.Size.Height}");
                                W_Size = rotatedRect.Size.Width;

                                // 獲取矩形的四個頂點
                                boxPoints = rotatedRect.Points();
                                intPoints = Array.ConvertAll(boxPoints, point => new Point((int)Math.Round(point.X), (int)Math.Round(point.Y)));
                                // 繪製方型
                                Cv2.Polylines(outputImage, new[] { intPoints }, true, Scalar.Red, 5);

                                break;
                            }

                            // 獲取矩形的四個頂點
                            boxPoints = rotatedRect.Points();
                            intPoints = Array.ConvertAll(boxPoints, point => new Point((int)Math.Round(point.X), (int)Math.Round(point.Y)));
                            // 繪製方型
                            Cv2.Polylines(outputImage, new[] { intPoints }, true, Scalar.Red, 5);
                        }

                        Cv2.ImWrite(Save_Path, outputImage);
                        if (!IsBatchCalculate)
                            f_Wafer_Align_Angle.ShowImage(Save_Path);

                        Scope.WaferAngle = Angle;

                        if(NonUserPause)
                        {
                            Transition(WORK.ROTATE_IMAGE);
                        }
                        else
                        {
                            SetNextState(WORK.ROTATE_IMAGE);
                            SetStatus(TASK_STATUS.PAUSE);
                            Transition(WORK.PAUSE);
                        }
                        #endregion
                    }
                    break;

                case WORK.ROTATE_IMAGE:
                    {
                        //if (Math.Abs(rotatedRect.Size.Width - W_Pixel) <= 30)
                        if(rotatedRect.Angle < 45)
                            rotationMatrix = Cv2.GetRotationMatrix2D(rotatedRect.Center, rotatedRect.Angle, 1.0); //座標軸正
                        else
                            rotationMatrix = Cv2.GetRotationMatrix2D(rotatedRect.Center, rotatedRect.Angle -90, 1.0); //座標軸差90度

                        // 建立輸出圖像大小（建議使用原圖尺寸，或加大避免裁切）
                        Cv2.WarpAffine(image, image, rotationMatrix, image.Size(), InterpolationFlags.Linear, BorderTypes.Constant, Scalar.Black);

                        Cv2.ImWrite(Save_Path, image);
                        //f_Wafer_Align_Angle.ShowImage(Save_Path);

                        Transition(WORK.CREATE_TECTANGLE);

                        rotationMatrix.Dispose();

                    }
                    break;
                case WORK.CREATE_TECTANGLE:
                    {
                        // 檢測輪廓
                        OpenCvSharp.Point[][] contours;
                        HierarchyIndex[] hierarchy;
                        Cv2.FindContours(image, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

                        foreach (var contour in contours)
                        {
                            filteredContours.Add(contour);
                        }

                        Point2f[] points = filteredContours.SelectMany(c => c).Select(p => new Point2f(p.X, p.Y)).ToArray();

                        // 尋找最小外接矩形
                        boundingRect = Cv2.BoundingRect(points);

                        //// 創建一個彩色圖像用於顯示結果
                        Cv2.CvtColor(image, outputImage, ColorConversionCodes.GRAY2BGR);

                        // 繪製矩形
                        Cv2.Rectangle(outputImage, boundingRect, Scalar.Blue, 2);

                        Cv2.ImWrite(Save_Path, outputImage);

                        if (!IsBatchCalculate)
                            f_Wafer_Align_Angle.ShowImage(Save_Path);

                        //if (IsBatchCalculate)
                        //    Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"{batch_count}.png", result);

                        Transition(WORK.SHIFT_XY);

                    }
                    break;

                case WORK.SHIFT_XY:
                    {
                        Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"Orign.png", image);

                        #region 平移圖片
                        // 原圖
                        //Mat image = Cv2.ImRead(ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath));
                        int centerX = boundingRect.X + boundingRect.Width / 2;
                        int centerY = boundingRect.Y + boundingRect.Height / 2;

                        int dx = 280 - centerX; //中心點需要修改
                        int dy = 700 - centerY;

                        // 建立仿射平移矩陣 (2x3)
                        translationMatrix = new Mat(2, 3, MatType.CV_32F);
                        translationMatrix.Set<float>(0, 0, 1);
                        translationMatrix.Set<float>(0, 1, 0);
                        translationMatrix.Set<float>(0, 2, dx);

                        translationMatrix.Set<float>(1, 0, 0);
                        translationMatrix.Set<float>(1, 1, 1);
                        translationMatrix.Set<float>(1, 2, dy);

                        // 平移後圖像
                        Mat movedImage = new Mat();
                        Cv2.WarpAffine(image, movedImage, translationMatrix, image.Size(),
                                       InterpolationFlags.Linear, BorderTypes.Constant, Scalar.Black);

                        // 儲存或顯示
                        Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"Shift_{batch_count}.png", movedImage);

                        Cv2.ImWrite(Save_Path, movedImage);
                        if (!IsBatchCalculate)
                            f_Wafer_Align_Angle.ShowImage(Save_Path);

                        Transition(WORK.PROCESS_IMAGE);

                        #endregion
                    }
                    break;
                case WORK.PROCESS_IMAGE:
                    {
                        image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);

                        #region 擷取圖片
                        //定義範圍(x, y, width, height)
                        OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)Scope.start_xy[0],
                                            (int)Scope.start_xy[1],
                                            (int)Scope.len[0],
                                            (int)Scope.len[1]);

                        // 擷取圖像範圍
                        Mat cropped = new Mat(image, roi);
                        image.Dispose();
                        image = cropped;
                        #endregion

                        #region 導正圖片
                        if (rotatedRect.Angle < 45)
                            rotationMatrix = Cv2.GetRotationMatrix2D(rotatedRect.Center, rotatedRect.Angle, 1.0); //座標軸正
                        else
                            rotationMatrix = Cv2.GetRotationMatrix2D(rotatedRect.Center, rotatedRect.Angle - 90, 1.0); //座標軸差90度

                        // 建立輸出圖像大小（建議使用原圖尺寸，或加大避免裁切）
                        Cv2.WarpAffine(image, image, rotationMatrix, image.Size(), InterpolationFlags.Linear, BorderTypes.Constant, Scalar.Black);
                        #endregion

                        #region 圖片移至中心
                        Cv2.WarpAffine(image, image, translationMatrix, image.Size(),
                                       InterpolationFlags.Linear, BorderTypes.Constant, Scalar.Black);
                        #endregion

                        #region 圖片取閥值
                        Cv2.Threshold(image, image, 200, 255, ThresholdTypes.Binary);
                        #endregion

                        //圖片旋轉180度
                        Point2f center = new Point2f(280f, 700f);
                        rotationMatrix = Cv2.GetRotationMatrix2D(center, 180, 1.0);
                        Cv2.WarpAffine(image, image, rotationMatrix, image.Size(), InterpolationFlags.Linear, BorderTypes.Constant, Scalar.Black);

                        Cv2.ImWrite(Save_Path, image);
                        if (!IsBatchCalculate)
                            f_Wafer_Align_Angle.ShowImage(Save_Path);

                        if (NonUserPause)
                        {
                            Transition(WORK.GRAB_KEYWORD);
                        }
                        else
                        {
                            SetNextState(WORK.GRAB_KEYWORD);
                            SetStatus(TASK_STATUS.PAUSE);
                            Transition(WORK.PAUSE);

                            Scope.status = 2;
                            MessageBox.Show("Capture OCR region");
                        }
                    }
                    break;
                case WORK.GRAB_KEYWORD:
                    {
                        //定義範圍(x, y, width, height)
                        OpenCvSharp.Rect roi = new OpenCvSharp.Rect((int)Scope.orgin_xy[0],
                                            (int)Scope.orgin_xy[1],
                                            (int)Scope.orgin_len[0],
                                            (int)Scope.orgin_len[1]);

                        // 擷取圖像範圍
                        Mat cropped = new Mat(image, roi);
                        image.Dispose();
                        image = cropped;

                        state = WORK.CHECK_OCR;
                        goto case WORK.CHECK_OCR;
                    }
                case WORK.CHECK_OCR:
                    {
                        Cv2.ImWrite(Save_Path, image);
                        f_Wafer_Align_Angle.ShowImage(Save_Path);

                        using (var engine = new TesseractEngine(Application.StartupPath, "eng", EngineMode.Default))
                        {
                            engine.DefaultPageSegMode = PageSegMode.SingleChar;

                            using (var img = Pix.LoadFromFile(Save_Path))
                            {
                                using (var page = engine.Process(img))
                                {
                                    string text = page.GetText();
                                    Console.WriteLine("辨識結果：");
                                    Console.WriteLine(text);
                                    Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"OCR.png", image);
                                    f_Wafer_Align_Angle.ShowOCR_Result(text);
                                }
                            }
                        }

                        ResetTimeCount(out delay_time);

                        Transition(WORK.SUCCESS);
                    }
                    break;



                case WORK.SUCCESS:
                    {
                        if(CheckTimeOverSec(delay_time,1))
                        {
                            SetStatus(TASK_STATUS.SUCCESS);

                            image.Dispose();
                            outputImage.Dispose();
                            //GC.Collect();
                        }
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
