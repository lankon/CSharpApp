using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace ImageProcessing.FF_Calculate
{
    class FF_Algorithm
    {
        #region parameter define 
        FarField_Parameter FF_Param = new FarField_Parameter();
        
        
        struct FarField_Parameter
        {
            public double Image_Heigh;
            public double Image_Width;
            public double Center_X;
            public double Center_Y;
            public double[] FOV50_Width;
            public int ProfileAngleNum;
        }
        enum ErrorCode
        {
            OUT_OF_POWER,
            NO_POWER,
            IMAGE_FORMAT_ERROR,
            IMAGE_LENGTH_ERROR,
        }
        #endregion

        #region private function
        private FarField_Parameter GetImageSize(Mat image, FarField_Parameter ff_param)
        {
            ff_param.Image_Heigh = image.Height;
            ff_param.Image_Width = image.Width;
            
            return ff_param;
        }
        private double CalculateImageTotalPower(Mat image)
        {
            // 總像素加總（支援所有深度）
            Scalar sum = Cv2.Sum(image);

            // 擷取灰階通道總和
            double valueSum = sum.Val0;

            return valueSum;
        }
        private double[] FindImageMinMaxPower(Mat image)
        {
            double minVal, maxVal;
            OpenCvSharp.Point minLoc, maxLoc;   //最大最小值所在的像素位置

            // 找出最大最小值及其位置
            Cv2.MinMaxLoc(image, out minVal, out maxVal, out minLoc, out maxLoc);

            double[] MaxMin = new double[2];
            MaxMin[0] = minVal;
            MaxMin[1] = maxVal;

            return MaxMin;
        }
        private double[] FindImageCentroid(Mat image)
        {
            double[] XY_Center = new double[] { 0.0, 0.0 };

            // 計算影像矩（moments）
            Moments m = Cv2.Moments(image, false);

            // 總亮度值
            double value_sum = m.M00;

            // 防止除以 0
            if (value_sum == 0)
                return XY_Center;

            // 計算質心位置
            XY_Center[0] = (int)(m.M10 / value_sum);
            XY_Center[1] = (int)(m.M01 / value_sum);

            return XY_Center;   //[0]:X座標,[1]:Y座標
        }
        /// <summary>
        /// 影像背景濾波
        /// </summary>
        /// <param name="image">影像</param>
        private void BackGroundFilter(Mat image)
        {
            // 定義背景的尺寸
            int Background_SizeX = 180, Background_SizeY = 300;
            int Size_X = image.Width, Size_Y = image.Height;
            
            // 裁切出背景區域（ROI）
            Rect roi = new Rect(430, 900, Background_SizeX, Background_SizeY);
            Mat Background_image = new Mat(image, roi);

            //計算背景閥值
            double total_power = CalculateImageTotalPower(Background_image);
            double filter_value = total_power / (Background_SizeX * Background_SizeY);

            //將圖像以背景閥值濾波
            Cv2.Threshold(image, image, filter_value, 0, ThresholdTypes.Tozero);

            Background_image.Dispose();
        }
        /// <summary>
        /// 取得影像特定角度的profile
        /// </summary>
        private List<double> GetProfileDegree(Mat image, FarField_Parameter ff_param, int angle)
        {
            // 確保是單通道灰階
            //if (image.Channels() != 1)
            //    return (int)ErrorCode.IMAGE_FORMAT_ERROR;

            // 採樣長度限制(需為int) 
            int maxLength = Math.Min((int)ff_param.Center_X, (int)ff_param.Center_Y); 

            double angleRad = angle * Math.PI / 180.0;
            double dx = Math.Cos(angleRad);
            double dy = Math.Sin(angleRad);

            List<double> profile = new List<double>();

            for (int i = -maxLength; i < maxLength; i++)
            {
                int x = (int)Math.Round(ff_param.Center_X + dx * i);
                int y = (int)Math.Round(ff_param.Center_Y + dy * i);

                if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
                    break;

                double value = 0.0;
                if (image.Depth() == MatType.CV_16U)
                    value = image.At<ushort>(y, x);
                else if (image.Depth() == MatType.CV_8U)
                    value = image.At<byte>(y, x);

                profile.Add(value);
            }

            return profile;
        }
        #endregion

        #region public function
        public int PreProcess(Mat image)
        {
            FF_Param.ProfileAngleNum = 4;
            FF_Param.FOV50_Width = new double[FF_Param.ProfileAngleNum];
            
            //取得影像長寬
            FF_Param = GetImageSize(image, FF_Param);
            
            //加總整張圖像灰階值大小
            double WholePower = CalculateImageTotalPower(image);

            //取得影像最大灰階值
            double[] MinMax = FindImageMinMaxPower(image);

            //判斷影像是否過曝
            if(image.Depth() == MatType.CV_16U)
            {
                if (MinMax[1] > 65000)
                    return (int)ErrorCode.OUT_OF_POWER;
            }
            else if(image.Depth() == MatType.CV_8U)
            {
                if (MinMax[1] > 254)
                    return (int)ErrorCode.OUT_OF_POWER;
            }

            //判斷是否有曝光
            if (WholePower == 0)
                return (int)ErrorCode.NO_POWER;

            return 0;
        }
        public int ImageFiltering(Mat image)
        {
            //中間值濾波
            Cv2.MedianBlur(image, image, 3); //3x3

            //背景濾波
            BackGroundFilter(image);

            double[] xy_center = FindImageCentroid(image);
            FF_Param.Center_X = xy_center[0];
            FF_Param.Center_Y = xy_center[1];

            return 0;
        }
        public int Calculate_Diameter()
        {
            
            
            return 0;
        }
        public int Calculate_Half_Diameter(Mat image)
        {
            int maxLength = Math.Min((int)FF_Param.Center_X, (int)FF_Param.Center_Y);
            List<double> Data;

            for (int Phase = 0; Phase < FF_Param.ProfileAngleNum; Phase++)
            {
                int Max = 0;
                int start_point = 0, end_point = 0;
                int Threshold = 0;

                Data = GetProfileDegree(image, FF_Param, Phase * 45);

                //取得峰值
                for (int i = 0; i < 2*maxLength; i++)
                {
                    if (Max < Data[i])
                        Max = (int)Data[i];
                }

                //閥值
                Threshold = (int)(Max * 0.5);

                for (int i = 0; i < 2*maxLength; i++)
                {
                    if (Data[i] > Threshold)
                    {
                        start_point = i;
                        break;
                    }
                }

                for (int i = 2*maxLength - 1; i > 0; i--)
                {
                    if (Data[i] > Threshold)
                    {
                        end_point = i;
                        break;
                    }
                }

                //45,135長度修正
                if (Phase % 2 != 0)
                    FF_Param.FOV50_Width[Phase] = (float)((end_point - start_point) * Math.Sqrt(2.0));
                else
                    FF_Param.FOV50_Width[Phase] = end_point - start_point;

            }

            for (int i = 0; i < FF_Param.ProfileAngleNum; i++)
            {
                if (FF_Param.FOV50_Width[i] <= 0)
                    return (int)ErrorCode.IMAGE_LENGTH_ERROR;
            }

            return 0;
        }
        #endregion

    }
}
