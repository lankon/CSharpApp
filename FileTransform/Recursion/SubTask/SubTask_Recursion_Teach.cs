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


namespace FileTransform.Recursion
{
    public class SubTask_Recursion_Teach
    {
        #region parameter define 
        enum WORK
        {
            INITIAL,

            LOAD_IMAGE,
            GRAB_IMAGE,
            GRAB_ORRGIN,
            THRESHOLD_IMAGE,
            FIND_Circule,
            FIND_RECTANGLE,
            CALCULATE_DIST,

            ERROR,

            SUCCESS,
            END,
        }

        //public UpdatePresentValueCallBack UpdatePresentValue { get; set; }
        //public UpdateSetValueCallBack UpdateSetValue { get; set; }
        //public GetBoardRTDCallBack GetBoardRTD { get; set; }


        Tool tool = new Tool();
        private WORK state;// = WORK.INITIAL;
        private bool IsFinish = false;
        //private bool IsConnect = false; //確認連線
        //private bool IsMonitorAll = false;  //判斷是否啟動所有控制器
        private int test_time = 0;
        //private int board_num = 0;
        private string ErrorMsg = "";
        private string Save_Path = Application.StartupPath + @"\Picture\" + "Recursion.png";
        //private string present_temp_value = "-99";
        //private string temp_5_rtd = "-99.0,-99.0,-99.0,-99.0,-99.0";
        //private string stc_type = "";
        //private string sbaudrate = "";
        //private string sparity = "";
        //private string scomport = "";
        //ITemperatureController[] TC = new ITemperatureController[4];
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
                tool.SaveHistoryToFile("(SubTask_Recursion_Teach):"+ target.ToString());
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
        //public void MonitorAll(bool flag)
        //{
        //    IsMonitorAll = flag;
        //}
        #endregion

        public SubTask_Recursion_Teach()
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
                        tool.SaveHistoryToFile("(SubTask_Recursion_Teach):" + state.ToString());
                        ResetTimeCount(out test_time);

                        string path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
                        image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);
                        
                        if (image.Empty())
                        {
                            tool.SaveHistoryToFile("針痕教學影像不存在");
                            Transition(WORK.SUCCESS);
                            break;
                        }

                        using(Mat dst = new Mat())
                        {
                            Cv2.Normalize(image, dst, 0, 255, NormTypes.MinMax, MatType.CV_8U); //16位元轉8位元
                            Cv2.ImWrite(Save_Path, dst);  //儲存圖像
                            ShowImage(Save_Path);   // 顯示影像於主畫面
                        }

                        Transition(WORK.GRAB_IMAGE);

                        MessageBox.Show("Capture Image");

                        GlobalVariable.status = 1;

                    }
                    break;
                case WORK.GRAB_IMAGE:
                    {
                        //定義範圍(x, y, width, height)
                        Rect roi = new Rect((int)GlobalVariable.start_xy[0],
                                            (int)GlobalVariable.start_xy[1],
                                            (int)GlobalVariable.len[0],
                                            (int)GlobalVariable.len[1]);
                        //Rect roi = new Rect(177, 30, 100, 100);

                        // 擷取圖像範圍
                        Mat cropped = new Mat(image, roi);
                        image.Dispose();
                        image = cropped;

                        state = WORK.THRESHOLD_IMAGE;
                        goto case WORK.THRESHOLD_IMAGE;

                        //Transition(WORK.GRAB_ORRGIN);
                    }
                    //break;
                case WORK.THRESHOLD_IMAGE:
                    {
                        // 二值化閥值處理
                        double thresholdValue = 50;
                        //double maxValue = 65535; //16位元影像,最大值65536
                        double maxValue = 256; //8位元影像,最大值25
                        Cv2.Threshold(image, image, thresholdValue, maxValue, ThresholdTypes.Binary);

                        //將圖像轉換成8位元深度
                        //image.ConvertTo(image, MatType.CV_8UC1, 255.0 / 65535.0);

                        // 創建一個彩色圖像用於顯示結果
                        Cv2.CvtColor(image, outputImage, ColorConversionCodes.GRAY2BGR);

                        Cv2.ImWrite(Save_Path, image);

                        //顯示圖像
                        ShowImage(Save_Path);

                        Transition(WORK.FIND_Circule);

                        MessageBox.Show("Capture Needle Image");
                        GlobalVariable.status = 2;
                    }
                    break;
                case WORK.FIND_Circule:
                    {
                        GlobalVariable.status = -1;

                        // 檢測輪廓
                        OpenCvSharp.Point[][] contours;
                        HierarchyIndex[] hierarchy;
                        Cv2.FindContours(image, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

                        int aa = 0;
                        double largest_diameter = 0;
                        double radius_final = 0;

                        foreach (var contour in contours)
                        {
                            // 計算每個輪廓的最小外接圓
                            Point2f center;
                            float radius = 0;
                            Cv2.MinEnclosingCircle(contour, out center, out radius);

                            // 計算半徑
                            double diameter = radius * 2;

                            if (diameter < GlobalVariable.orgin_len[0] &&
                                diameter > largest_diameter &&
                                (GlobalVariable.orgin_xy[0] < center.X && center.X < GlobalVariable.orgin_xy[0] + GlobalVariable.orgin_len[0]) &&
                                (GlobalVariable.orgin_xy[1] < center.Y && center.Y < GlobalVariable.orgin_xy[1] + GlobalVariable.orgin_len[1]))
                            {
                                largest_diameter = diameter;
                                aa++;
                                center_final = center;
                                radius_final = radius;
                            }

                            filteredContours.Add(contour);
                        }

                        Cv2.Circle(outputImage, (OpenCvSharp.Point)center_final, 3, new Scalar(0, 255, 0), 5);   //繪製中心點
                        Cv2.Circle(outputImage, (OpenCvSharp.Point)center_final, (int)radius_final, new Scalar(0, 0, 255), 4);  // 繪製輪廓圓

                        Cv2.ImWrite(Save_Path, outputImage);
                        ShowImage(Save_Path);
                        
                        Transition(WORK.FIND_RECTANGLE);
                    }
                    break;
                case WORK.FIND_RECTANGLE:
                    {
                        Point2f[] points = filteredContours.SelectMany(c => c).Select(p => new Point2f(p.X, p.Y)).ToArray();

                        // 尋找最小外接矩形
                        boundingRect = Cv2.BoundingRect(points);

                        // 繪製矩形
                        Cv2.Rectangle(outputImage, boundingRect, Scalar.Blue, 2);

                        Cv2.ImWrite(Save_Path, outputImage);

                        ShowImage(Save_Path);

                        Transition(WORK.CALCULATE_DIST);
                    }
                    break;
                case WORK.CALCULATE_DIST:
                    {
                        // 計算圓心到方框邊的距離
                        double PixelSizeX = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelX);//0.59261f;
                        double PixelSizeY = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelY);
                        double distanceX = Math.Min(center_final.X - boundingRect.Left, boundingRect.Right - center_final.X) * PixelSizeX;
                        double distanceY = Math.Min(center_final.Y - boundingRect.Top, boundingRect.Bottom - center_final.Y) * PixelSizeY;

                        MessageBox.Show($"X:{(int)distanceX}um, Y:{(int)distanceY}um");

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
