using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

using CommonFunction;


namespace FileTransform.Wafer_Align_Angle
{
    public class SubTask_Angle_Calculate
    {
        #region parameter define 
        enum WORK
        {
            INITIAL,

            LOAD_IMAGE,
            GRAB_IMAGE,
            GRAB_ORRGIN,
            THRESHOLD_IMAGE,
            FIND_RECTANGLE,
            FIND_Circule,
            CALCULATE_DIST,

            ERROR,

            SUCCESS,
            END,
        }

        Tool tool = new Tool();
        private WORK state;// = WORK.INITIAL;
        private bool IsFinish = false;
        private int test_time = 0;
        private string ErrorMsg = "";
        private string Save_Path = Application.StartupPath + @"\Picture\" + "Calculate.png";
        Rect boundingRect;
        Point2f center_final;
        private List<Point[]> filteredContours = new List<Point[]>();
        private Mat image;
        Mat outputImage = new Mat();
        public ShowImageCallBack ShowImage { get; set; }
        #endregion

        #region private function
        private void ResetTimeCount(out int tick)
        {
            tick = Environment.TickCount;
        }
        private int GetTimeCount(int tick)
        {
            var time_count = Environment.TickCount - tick;

            return time_count;
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
                tool.SaveHistoryToFile("[Task](SubTask_Recursion_Teach):"+ target.ToString());
            }

            state = target;
        }
        #endregion

        #region public function
        public bool Run()
        {
            if (IsFinish == true)
                return true;
            else
            {
                RunLoop();
                return false;
            }
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

        public SubTask_Angle_Calculate()
        {
            Transition(WORK.INITIAL);
        }

        private void RunLoop()
        {
            switch (state)
            {                               
                case WORK.INITIAL:
                    {
                        state = WORK.LOAD_IMAGE;
                        goto case WORK.LOAD_IMAGE;
                    }
                case WORK.LOAD_IMAGE:
                    {
                        tool.SaveHistoryToFile("[Task](SubTask_Angle_Calculate):" + state.ToString());
                        ResetTimeCount(out test_time);

                        string path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
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
                            ShowImage(Save_Path);   // 顯示影像於主畫面
                        }

                        Transition(WORK.THRESHOLD_IMAGE);
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
                        ShowImage(Save_Path);

                        Transition(WORK.FIND_RECTANGLE);
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
                            if(Math.Abs(XPitch - 0) > 0.01 && Math.Abs(XPitch - 0) > 0.01)
                            {
                                W_Pixel = ChipWidth / XPitch;
                                H_Pixel = ChipHeigh / YPitch;
                            }
                        }
                        catch(Exception ex)
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

                            if(Math.Abs(rotatedRect.Size.Width - W_Pixel) <= 5 && Math.Abs(rotatedRect.Size.Height - H_Pixel) <= 5)
                            {
                                Angle = rotatedRect.Angle;

                                //// 修正角度範圍
                                //if (Angle < -45)
                                //{
                                //    Angle += 90; // 保證角度範圍在 [-45, 45] 之間
                                //}

                                if(CorrectCount == 0)
                                {
                                    AngleCheck = Angle;
                                    CorrectCount++;
                                }
                                else if(Math.Abs(AngleCheck-Angle) < 0.5)
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
                        ShowImage(Save_Path);

                        if (CorrectCount <= 3)
                            MessageBox.Show("Find Angle Error");
                        else
                            MessageBox.Show($"Angle:{Angle.ToString("0.00")}");

                        Transition(WORK.SUCCESS);
                    }
                    break;
                case WORK.SUCCESS:
                    {
                        int time = GetTimeCount(test_time);
                        image.Dispose();
                        outputImage.Dispose();

                        //Transition(WORK.END);
                        state = WORK.END;
                        goto case WORK.END;
                    }
                    //break;

                case WORK.END:
                    {
                        GC.Collect();
                        IsFinish = true;
                        MessageBox.Show("Finish");
                    }
                    break;
            }           
        }
    }
}
