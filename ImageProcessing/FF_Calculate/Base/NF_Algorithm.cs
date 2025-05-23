using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace ImageProcessing.FF_Calculate
{
    public struct NearField_Parameter
    {
        public double Threshold;
        public int EmitterCount;   //光窗數量
        public List<int> PosX;
        public List<int> PosY;
        public List<double> Diameters;
    }

    class NF_Algorithm
    {
        #region parameter define
        private NearField_Parameter NF_Param = new NearField_Parameter();
        public double Threshold_Ratio { set { NF_Param.Threshold = value; } }
        public List<double> Emitter_Diameter { get; private set; }
        public List<int> Emitter_PosX { get; private set; }
        public List<int> Emitter_PosY { get; private set; }

        enum ErrorCode
        {
            FIND_EMITTER_COUNT_ERROR,   //光窗數量過少
            EMITTER_NO_POWER,           //無光強
        }
        #endregion

        #region private function
        private void Initial_Param()
        {
            NF_Param.Diameters = new List<double>();
            NF_Param.PosX = new List<int>();
            NF_Param.PosY = new List<int>();
        }
        private NearField_Parameter FindEmitterInfo(List<Point[]> Emitter_Array, NearField_Parameter nf_param)
        {
            //找各個光窗的中心點以及直徑
            foreach (var contour in Emitter_Array)
            {
                Moments m = Cv2.Moments(contour);
                int cx = (int)(m.M10 / m.M00);
                int cy = (int)(m.M01 / m.M00);

                // 模擬 Feret Max（找出距離重心最遠點 * 2）
                double maxDist = 0;
                foreach (var pt in contour)
                {
                    double dist = Math.Sqrt(Math.Pow(pt.X - cx, 2) + Math.Pow(pt.Y - cy, 2));
                    if (dist > maxDist) maxDist = dist;
                }

                nf_param.PosX.Add(cx);
                nf_param.PosY.Add(cy);
                nf_param.Diameters.Add(maxDist * 2);
            }

            return nf_param;
        }
        private NearField_Parameter Resort_Array(NearField_Parameter Data)
        {
            for (int i = 0; i < Data.EmitterCount; i++)
            {
                int TempX, TempY;
                double TempDiameter;

                for (int j = i + 1; j < Data.EmitterCount; j++)
                {
                    if (Data.PosX[i] > Data.PosX[j])
                    {

                        TempX = Data.PosX[i];
                        Data.PosX[i] = Data.PosX[j];
                        Data.PosX[j] = TempX;

                        TempY = Data.PosY[i];
                        Data.PosY[i] = Data.PosY[j];
                        Data.PosY[j] = TempY;

                        TempDiameter = Data.Diameters[i];
                        Data.Diameters[i] = Data.Diameters[j];
                        Data.Diameters[j] = TempDiameter;
                    }

                }
            }
            // Y axis Down -> Up
            for (int i = 0; i < Data.EmitterCount; i++)
            {
                int TempX, TempY;
                double TempDiameter;

                for (int j = i + 1; j < Data.EmitterCount; j++)
                {
                    if (Math.Abs(Data.PosX[i] - Data.PosX[j]) < 15)
                    {
                        if (Data.PosY[i] < Data.PosY[j])
                        {
                            TempX = Data.PosX[i];
                            Data.PosX[i] = Data.PosX[j];
                            Data.PosX[j] = TempX;

                            TempY = Data.PosY[i];
                            Data.PosY[i] = Data.PosY[j];
                            Data.PosY[j] = TempY;

                            TempDiameter = Data.Diameters[i];
                            Data.Diameters[i] = Data.Diameters[j];
                            Data.Diameters[j] = TempDiameter;
                        }
                    }
                }
            }

            return Data;
        }
        #endregion

        #region public function
        public int Calculate_Diameter(Mat image)
        {
            double SigmaX_2 = 0, SigmaY_2 = 0, SigmaXY = 0, Sigma2 = 0;

            Calculate_sigma_square(image, out SigmaX_2, out SigmaY_2, out SigmaXY, out Sigma2);

            //if ((SigmaX_2 <= 0) || (SigmaY_2 <= 0))
            //{
            //    SigmaX = 0;
            //    SigmaY = 0;
            //}
            //else
            //{
            //    SigmaX = Math.Sqrt(SigmaX_2);
            //    SigmaY = Math.Sqrt(SigmaY_2);
            //}

            //if (SigmaX_2 != 0 && SizeX != 0)
            //{
            //    if ((SigmaX_2 - SigmaY_2) > 0.0)
            //        Gamma = 1.0;
            //    else
            //        Gamma = -1.0;

            //    double ValueSigX = (SigmaX_2 - SigmaY_2) * (SigmaX_2 - SigmaY_2) + 4 * (SigmaXY_2 * SigmaXY_2);
            //    double ValueSigY = (SigmaX_2 - SigmaY_2) * (SigmaX_2 - SigmaY_2) + 4 * (SigmaXY_2 * SigmaXY_2);

            //    if (ValueSigX >= 0)
            //        DiameterX = 2 * Math.Sqrt(2.0) * Math.Sqrt((SigmaX_2 + SigmaY_2) + Gamma * Math.Sqrt(ValueSigX)) * 1E3 * Pixelsize;
            //    else
            //        DiameterX = -999;

            //    if (ValueSigY >= 0)
            //        DiameterY = 2 * Math.Sqrt(2.0) * Math.Sqrt((SigmaX_2 + SigmaY_2) - Gamma * Math.Sqrt(ValueSigY)) * 1E3 * Pixelsize;
            //    else
            //        DiameterY = -999;

            //    if ((SigmaX_2 + SigmaY_2) > 0)
            //    {
            //        Diameter = 2 * Math.Sqrt(2.0) * Math.Sqrt(SigmaX_2 + SigmaY_2) * 1E3 * Pixelsize;
            //    }
            //    else
            //    {
            //        Diameter = -999;
            //    }
            //}
            //else
            //{
            //    SigmaX = -999;
            //    SigmaY = -999;
            //    DiameterX = -999;
            //    DiameterY = -999;
            //    Diameter = -999;
            //}

            return 0;
        }
        /// <summary>
        /// 二階矩計算
        /// </summary>
        public void Calculate_sigma_square(Mat image, out double sigmaX2, out double sigmaY2, 
                                                      out double sigmaXY, out double sigma2)
        {
            int width = image.Width;
            int height = image.Height;

            // Convert to float64 for accurate calculation
            Mat image64F = new Mat();
            image.ConvertTo(image64F, MatType.CV_64FC1);

            double wholePower = Cv2.Sum(image64F)[0];

            //if (wholePower == 0)
            //    return (int)ErrorCode.EMITTER_NO_POWER;

            #region 計算XY重心 xBar,yBar
            double xSum = 0, ySum = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double val = image64F.At<double>(y, x);
                    xSum += val * (x + 0.5);
                    ySum += val * (y + 0.5);
                }
            }

            double xBar = xSum / wholePower;
            double yBar = ySum / wholePower;
            #endregion

            //平均值
            double meanVal = wholePower / (width * height);

            #region 計算 σ_x^2 , σ_y^2 , σ_xy , σ(整張影像亮度分佈)
            double secOrdSumX = 0, secOrdSumY = 0, secOrdSumXY = 0, secOrdSum = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double val = image64F.At<double>(y, x);
                    double dx = (x + 0.5) - xBar;
                    double dy = (y + 0.5) - yBar;
                    double dz = val - meanVal;

                    secOrdSumX += val * dx * dx;
                    secOrdSumY += val * dy * dy;
                    secOrdSumXY += val * dx * dy;
                    secOrdSum += dz * dz;
                }
            }
             
            sigmaX2 = secOrdSumX / wholePower;
            sigmaY2 = secOrdSumY / wholePower;
            sigmaXY = secOrdSumXY / wholePower;
            sigma2 = secOrdSum / (width * height);
            #endregion

            //釋放記憶體
            image64F.Dispose();

            //return 0;
        }
        public int Detect_Position(Mat image)
        {
            Mat gray = new Mat();

            if (image.Channels() > 1)
                Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
            else
                gray = image.Clone();

            //找最大灰階值
            double minVal, maxVal;
            Cv2.MinMaxLoc(gray, out minVal, out maxVal);

            //二值化
            Mat binary = new Mat();
            double threshold = maxVal * NF_Param.Threshold;
            Cv2.Threshold(gray, binary, threshold, 255, ThresholdTypes.Binary);

            //膨脹
            Cv2.Dilate(binary, binary, Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3)));

            //16位元轉8位元
            if (binary.Depth() == MatType.CV_16U)
                Cv2.Normalize(binary, binary, 0, 255, NormTypes.MinMax, MatType.CV_8U); //16位元轉8位元

            //找輪廓
            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            //釋放記憶體
            gray.Dispose();
            binary.Dispose();

            //沒有找到有效的光窗
            if (contours.Length < 1) 
                return (int)ErrorCode.FIND_EMITTER_COUNT_ERROR;

            //過濾面積小於 Area_Threshold的輪廓
            double Area_Threshold = 28900;  //有問題時可調整
            List<Point[]> filteredContours = new List<Point[]>();
            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                if (area < Area_Threshold)
                {
                    filteredContours.Add(contour);
                }
            }

            //光窗小於1顆(有問題時可調整)
            if (filteredContours.Count < 1)  
                return (int)ErrorCode.FIND_EMITTER_COUNT_ERROR;

            NF_Param.EmitterCount = filteredContours.Count;

            //取得光窗位置
            NF_Param = FindEmitterInfo(filteredContours, NF_Param);

            //排序光窗位置
            NF_Param = Resort_Array(NF_Param);

            Emitter_Diameter = NF_Param.Diameters;

            return 0;
        }

        #endregion

        public NF_Algorithm()
        {
            Initial_Param();
        }
    }
}
