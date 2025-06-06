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


namespace FileTransform
{
    public delegate void ShowImageCallBack(string path);
    public delegate void ShowNameCallBack(string name);

    public class SubTask_Teach
    {
        #region parameter define 
        enum WORK
        {
            INITIAL,

            LOAD_IMAGE,
            THRESHOLD_IMAGE,
            FIND_EMITTER,
           

            ERROR,

            SUCCESS,
            END,
        }

        //public UpdatePresentValueCallBack UpdatePresentValue { get; set; }
        //public UpdateSetValueCallBack UpdateSetValue { get; set; }
        //public GetBoardRTDCallBack GetBoardRTD { get; set; }


        private WORK state;// = WORK.INITIAL;
        private bool IsFinish = false;
        //private bool IsConnect = false; //確認連線
        //private bool IsMonitorAll = false;  //判斷是否啟動所有控制器
        private int test_time = 0;
        //private int board_num = 0;
        private string ErrorMsg = "";
        private string Save_Path = Application.StartupPath + @"\Picture\" + "Nearfile.png";
        //private string present_temp_value = "-99";
        //private string temp_5_rtd = "-99.0,-99.0,-99.0,-99.0,-99.0";
        //private string stc_type = "";
        //private string sbaudrate = "";
        //private string sparity = "";
        //private string scomport = "";
        //ITemperatureController[] TC = new ITemperatureController[4];
        private List<Point[]> filteredContours = new List<Point[]>();
        private Mat image;
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
                Tool.SaveHistoryToFile("(SubTask_NearField):"+ target.ToString());
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

        public SubTask_Teach()
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
                        Tool.SaveHistoryToFile("(SubTask_NearField):" + state.ToString());

                        ResetTimeCount(out test_time);

                        image = new Mat(@"C:\Users\lankon\Desktop\tmep\0.tiff", ImreadModes.AnyDepth | ImreadModes.Grayscale);

                        if (image.Empty())
                        {
                            Tool.SaveHistoryToFile("近場教學影像不存在");
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
                        // 二值化閥值處理
                        double thresholdValue = 80; 
                        double maxValue = 65535; //16位元影像,最大值65536
                        Cv2.Threshold(image, image, thresholdValue, maxValue, ThresholdTypes.Binary);

                        //將圖像轉換成8位元深度
                        image.ConvertTo(image, MatType.CV_8UC1, 255.0 / 65535.0);

                        Cv2.ImWrite(Save_Path, image);

                        //顯示圖像
                        ShowImage(Save_Path);

                        Transition(WORK.FIND_EMITTER);
                    }
                    break;
                case WORK.FIND_EMITTER:
                    {
                        // 檢測輪廓
                        OpenCvSharp.Point[][] contours;
                        HierarchyIndex[] hierarchy;
                        Cv2.FindContours(image, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

                        // 創建一個彩色圖像用於顯示結果
                        using(Mat outputImage = new Mat())
                        {
                            Cv2.CvtColor(image, outputImage, ColorConversionCodes.GRAY2BGR);

                            int aa = 0;

                            foreach (var contour in contours)
                            {
                                // 計算每個輪廓的最小外接圓
                                Point2f center;
                                float radius;
                                Cv2.MinEnclosingCircle(contour, out center, out radius);

                                // 計算半徑
                                double diameter = radius * 2;

                                if (diameter > 35)
                                {
                                    aa++;
                                    Cv2.Circle(outputImage, (OpenCvSharp.Point)center, 4, new Scalar(0, 255, 0), 10);   //繪製中心點
                                    Cv2.Circle(outputImage, (OpenCvSharp.Point)center, (int)radius, new Scalar(0, 0, 255), 4);  // 繪製輪廓圓
                                    filteredContours.Add(contour);
                                }
                            }

                            Cv2.ImWrite(Save_Path, outputImage);

                            ShowImage(Save_Path);
                        }
                        
                        Transition(WORK.SUCCESS);
                    }
                    break;
                case WORK.SUCCESS:
                    {
                        Point2f[] points = filteredContours.SelectMany(c => c).Select(p => new Point2f(p.X, p.Y)).ToArray();
                        RotatedRect minRect = Cv2.MinAreaRect(points);

                        // 創建一個彩色圖像用於顯示結果
                        Mat outputImage = new Mat();
                        Cv2.CvtColor(image, outputImage, ColorConversionCodes.GRAY2BGR);

                        // 繪製最小外接矩形
                        Point2f[] rectPoints = minRect.Points();
                        for (int i = 0; i < 4; i++)
                        {
                            Cv2.Line(outputImage, (Point)rectPoints[i], (Point)rectPoints[(i + 1) % 4], Scalar.Red, 2);
                        }

                        Cv2.ImWrite(Save_Path, outputImage);

                        ShowImage(Save_Path);


                        int time = GetTimeCount(test_time);
                        image.Dispose();
                        
                        
                        Transition(WORK.END);
                    }
                    break;

                case WORK.END:
                    {
                        GC.Collect();
                        IsFinish = true;
                    }
                    break;
            }           
        }
    }
}
