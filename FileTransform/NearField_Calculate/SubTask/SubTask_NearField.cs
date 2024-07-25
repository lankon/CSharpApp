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


        Tool tool = new Tool();
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
        Mat image;
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
                tool.SaveHistoryToFile("(SubTask_NearField):"+ target.ToString());
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
                        goto case WORK.LOAD_IMAGE;
                    }
                case WORK.LOAD_IMAGE:
                    {
                        ResetTimeCount(out test_time);

                        image = new Mat(@"C:\Users\lankon\Desktop\tmep\0.tiff", ImreadModes.AnyDepth | ImreadModes.Grayscale);

                        if (image.Empty())
                        {
                            tool.SaveHistoryToFile("近場教學影像不存在");
                            Transition(WORK.SUCCESS);
                            break;
                        }

                        goto case WORK.THRESHOLD_IMAGE;
                    }
                case WORK.THRESHOLD_IMAGE:
                    {
                        tool.SaveHistoryToFile("(SubTask_NearField):" + state.ToString());

                        // 二值化閥值處理
                        double thresholdValue = 30; 
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
                        Mat outputImage = new Mat();
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

                            if (diameter > 10)
                            {
                                aa++;
                                Cv2.Circle(outputImage, (OpenCvSharp.Point)center, 2, new Scalar(0, 255, 0), 10);
                            }

                            // 繪製輪廓圓
                            Cv2.Circle(outputImage, (OpenCvSharp.Point)center, (int)radius, new Scalar(0, 0, 255), 4);
                        }

                        Cv2.ImWrite(Save_Path, outputImage);

                        ShowImage(Save_Path);

                        Transition(WORK.SUCCESS);
                    }
                    break;
                case WORK.SUCCESS:
                    {
                        int time = GetTimeCount(test_time);
                        image.Dispose();
                        
                        GC.Collect();
                        Transition(WORK.END);
                    }
                    break;

                case WORK.END:
                    {
                        IsFinish = true;
                    }
                    break;
            }           
        }
    }
}
