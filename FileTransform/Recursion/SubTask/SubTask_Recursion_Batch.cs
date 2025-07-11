﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.IO;

using CommonFunction;


namespace FileTransform.Recursion
{
    public class SubTask_Recursion_Batch
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

        private WORK state;// = WORK.INITIAL;
        private bool IsFinish = false;
        private int test_time = 0;
        private int delay_time = 0;
        private int cal_count = 0;
        private string ErrorMsg = "";
        private string Save_Path = Application.StartupPath + @"\Picture\" + "Recursion.png";
        Rect boundingRect;
        Point2f center_final;
        private List<Point[]> filteredContours = new List<Point[]>();
        private Mat image;
        Mat outputImage = new Mat();
        public ShowImageCallBack ShowImage { get; set; }
        public ShowNameCallBack ShowName { get; set; }
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
                Tool.SaveHistoryToFile("(SubTask_Recursion_Batch):"+ target.ToString());
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
        public void SetCount(int count)
        {
            cal_count = count;
        }
        #endregion

        public SubTask_Recursion_Batch()
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
                        Tool.SaveHistoryToFile("(SubTask_Recursion_Batch):" + state.ToString());
                        ResetTimeCount(out test_time);

                        string path = Scope.batch_path[cal_count];
                        image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);
                        
                        if (image.Empty())
                        {
                            Tool.SaveHistoryToFile("針痕教學影像不存在");
                            Transition(WORK.SUCCESS);
                            break;
                        }

                        using(Mat dst = new Mat())
                        {
                            Cv2.Normalize(image, dst, 0, 255, NormTypes.MinMax, MatType.CV_8U); //16位元轉8位元
                            Cv2.ImWrite(Save_Path, dst);  //儲存圖像
                            //ShowImage(Save_Path);   // 顯示影像於主畫面
                        }

                        Transition(WORK.GRAB_IMAGE);

                        //MessageBox.Show("Capture Image");

                        //Scope.status = 1;

                    }
                    break;
                case WORK.GRAB_IMAGE:
                    {
                        //定義範圍(x, y, width, height)
                        Rect roi = new Rect((int)Scope.start_xy[0],
                                            (int)Scope.start_xy[1],
                                            (int)Scope.len[0],
                                            (int)Scope.len[1]);
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
                        double thresholdValue = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_Threshold);
                        //double maxValue = 65535; //16位元影像,最大值65536
                        double maxValue = 256; //8位元影像,最大值25
                        Cv2.Threshold(image, image, thresholdValue, maxValue, ThresholdTypes.Binary);

                        //將圖像轉換成8位元深度
                        //image.ConvertTo(image, MatType.CV_8UC1, 255.0 / 65535.0);

                        // 創建一個彩色圖像用於顯示結果
                        Cv2.CvtColor(image, outputImage, ColorConversionCodes.GRAY2BGR);

                        Cv2.ImWrite(Save_Path, image);

                        //顯示圖像
                        //ShowImage(Save_Path);

                        Transition(WORK.FIND_Circule);

                        //MessageBox.Show("Capture Needle Image");
                        //Scope.status = 2;
                    }
                    break;
                case WORK.FIND_Circule:
                    {
                        Scope.status = -1;

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

                            if (diameter < Scope.orgin_len[0] &&
                                diameter > largest_diameter &&
                                (Scope.orgin_xy[0] < center.X && center.X < Scope.orgin_xy[0] + Scope.orgin_len[0]) &&
                                (Scope.orgin_xy[1] < center.Y && center.Y < Scope.orgin_xy[1] + Scope.orgin_len[1]))
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
                        //ShowImage(Save_Path);
                        
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

                        //ShowImage(Save_Path);

                        Transition(WORK.CALCULATE_DIST);
                    }
                    break;
                case WORK.CALCULATE_DIST:
                    {
                        // 計算圓心到方框邊的距離
                        double PixelSizeX = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelX);//0.59261f;
                        double PixelSizeY = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_PixelY);
                        double distanceX = 0;// Math.Min(center_final.X - boundingRect.Left, boundingRect.Right - center_final.X) * PixelSizeX;
                        double distanceY = 0;// Math.Min(center_final.Y - boundingRect.Top, boundingRect.Bottom - center_final.Y) * PixelSizeY;
                        int c_x = 0, c_y = 0, r_x = 0, r_y = 0;
                        
                        if(center_final.X - boundingRect.Left < boundingRect.Right - center_final.X)
                        {
                            distanceX = (center_final.X - boundingRect.Left) * PixelSizeX;

                            c_x = (int)center_final.X;
                            c_y = (int)center_final.Y;
                            r_x = (int)boundingRect.Left;
                            r_y = (int)center_final.Y;
                        }
                        else
                        {
                            distanceX = (boundingRect.Right - center_final.X) * PixelSizeX;

                            c_x = (int)center_final.X;
                            c_y = (int)center_final.Y;
                            r_x = (int)boundingRect.Right;
                            r_y = (int)center_final.Y;
                        }

                        Cv2.Line(outputImage, c_x, c_y, r_x, r_y, Scalar.OrangeRed, 4);

                        if (center_final.Y - boundingRect.Top < boundingRect.Bottom - center_final.Y)
                        {
                            distanceY = (center_final.Y - boundingRect.Top) * PixelSizeY;

                            c_x = (int)center_final.X;
                            c_y = (int)center_final.Y;
                            r_x = (int)center_final.X;
                            r_y = (int)boundingRect.Top;
                        }
                        else
                        {
                            distanceY = (boundingRect.Bottom - center_final.Y) * PixelSizeY;

                            c_x = (int)center_final.X;
                            c_y = (int)center_final.Y;
                            r_x = (int)center_final.X;
                            r_y = (int)boundingRect.Bottom;
                        }

                        Cv2.Line(outputImage, c_x, c_y, r_x, r_y, Scalar.OrangeRed, 4);

                        Cv2.ImWrite(Save_Path, outputImage);

                        ShowImage(Save_Path);

                        //ShowName()

                        //MessageBox.Show($"X:{(int)distanceX}um, Y:{(int)distanceY}um");
                        #region 寫檔
                        StreamWriter File;
                        string path = Application.StartupPath + @"\Result\Recursion";
                        string file_name = Path.GetFileName(Scope.batch_path[cal_count]);
                        File = Tool.CreateFile(@"\Result\Recursion", ".csv", true);
                        Tool.WriteFile(File, $"{file_name},{distanceX},{distanceY}");
                        Tool.CloseFile(File);
                        #endregion

                        ShowName(file_name);

                        Transition(WORK.SUCCESS);
                    }
                    break;
                case WORK.SUCCESS:
                    {
                        int time = GetTimeCount(test_time);
                        image.Dispose();
                        outputImage.Dispose();

                        //Transition(WORK.END);
                        ResetTimeCount(out delay_time);
                        state = WORK.END;
                        goto case WORK.END;
                    }
                    //break;

                case WORK.END:
                    {
                        int time = ApplicationSetting.Get_Int_Recipe((int)FormItem.TxtBx_ResultDelayTime);
                        if(CheckTimeOverMilliSec(delay_time, time))
                        {
                            //GC.Collect();
                            IsFinish = true;
                            //MessageBox.Show("Finish");
                        }
                    }
                    break;
            }           
        }
    }
}
