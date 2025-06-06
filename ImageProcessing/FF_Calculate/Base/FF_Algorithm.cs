using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace ImageProcessing.FF_Calculate
{
    public struct FarField_Parameter
    {
        public double Image_Heigh;
        public double Image_Width;
        public double Image_MaxPower;
        public int Center_X;
        public int Center_Y;
        public double[] FOV50;
        public double[] FOV1e2;
        public double[] FOV50_Width;
        public double[] FOV1e2_Width;
        public int ProfileAngleNum;

        //硬體
        public double PixelSize;    //相機Pixel Size
        public double TestHeigh;    //量測高度

        //結果
        public double Angle;    //發散角
        public double Valley;   //凹陷百分比
        public double EyeSafe;  //人眼安全
    }


    class FF_Algorithm
    {
        #region parameter define 
        private FarField_Parameter FF_Param = new FarField_Parameter();
        public double PixelSize { set { FF_Param.PixelSize = value; } }
        public double TestHeigh { set { FF_Param.TestHeigh = value; } }
        public double Angle { get; private set; }
        public double Valley { get; }
        public double EyeSafe { get; }
        enum ErrorCode
        {
            OUT_OF_POWER,
            NO_POWER,
            IMAGE_FORMAT_ERROR,
            IMAGE_LENGTH_ERROR,
        }
        #endregion

        #region private function
        /// <summary>
        /// 取得影像長寬
        /// </summary>
        /// <param name="image"></param>
        /// <param name="ff_param"></param>
        /// <returns></returns>
        private FarField_Parameter GetImageSize(Mat image, FarField_Parameter ff_param)
        {
            ff_param.Image_Heigh = image.Height;
            ff_param.Image_Width = image.Width;
            
            return ff_param;
        }
        /// <summary>
        /// 計算影像總能量
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private double CalculateImageTotalPower(Mat image)
        {
            // 總像素加總（支援所有深度）
            Scalar sum = Cv2.Sum(image);

            // 擷取灰階通道總和
            double valueSum = sum.Val0;

            return valueSum;
        }
        /// <summary>
        /// 取得影像最大及最小光強
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 找光斑質心座標
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private int[] FindImageCentroid(Mat image)
        {
            int[] XY_Center = new int[] { 0, 0 };

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
            int filter_value = (int)(total_power / (Background_SizeX * Background_SizeY));

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
            FF_Param.FOV50 = new double[FF_Param.ProfileAngleNum];
            FF_Param.FOV1e2_Width = new double[FF_Param.ProfileAngleNum];
            FF_Param.FOV1e2 = new double[FF_Param.ProfileAngleNum];


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

            FF_Param.Image_MaxPower = MinMax[1];

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

            int[] xy_center = FindImageCentroid(image);
            FF_Param.Center_X = xy_center[0];
            FF_Param.Center_Y = xy_center[1];

            return 0;
        }
        public int Calculate_Diameter(Mat image)
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
                for (int i = 0; i < 2 * maxLength; i++)
                {
                    if (Max < Data[i])
                        Max = (int)Data[i];
                }

                //閥值
                Threshold = (int)(Max * 0.135);

                for (int i = 0; i < 2 * maxLength; i++)
                {
                    if (Data[i] > Threshold)
                    {
                        start_point = i;
                        break;
                    }
                }

                for (int i = 2 * maxLength - 1; i > 0; i--)
                {
                    if (Data[i] > Threshold)
                    {
                        end_point = i;
                        break;
                    }
                }

                FF_Param.FOV1e2_Width[Phase] = end_point - start_point;

            }

            for (int i = 0; i < FF_Param.ProfileAngleNum; i++)
            {
                if (FF_Param.FOV1e2_Width[i] <= 0)
                    return (int)ErrorCode.IMAGE_LENGTH_ERROR;
            }

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

                FF_Param.FOV50_Width[Phase] = end_point - start_point;
            }

            for (int i = 0; i < FF_Param.ProfileAngleNum; i++)
            {
                if (FF_Param.FOV50_Width[i] <= 0)
                    return (int)ErrorCode.IMAGE_LENGTH_ERROR;
            }

            return 0;
        }
        public int Calculate_FarField_Result(bool angle = false, bool eye_safe = false, bool valley = false)
        {
            if(angle)   //計算發散角
            {
                for(int i=0; i<FF_Param.ProfileAngleNum; i++)
                {
                    FF_Param.FOV50[i] = 2 * (Math.Atan2(FF_Param.FOV50_Width[i] * FF_Param.PixelSize / 2, FF_Param.TestHeigh) * 180 / Math.PI);
                    FF_Param.FOV1e2[i] = 2 * (Math.Atan2(FF_Param.FOV1e2_Width[i] * FF_Param.PixelSize / 2, FF_Param.TestHeigh) * 180 / Math.PI);
                }

                //填入結果
                FF_Param.Angle = FF_Param.FOV1e2[0];
                Angle = FF_Param.Angle;
            }

            return 0;
        }
        public void CalculateEyeSafe(Mat image, out Mat out_image)  //還要再開發未完成
        {
            double threshold = FF_Param.Image_MaxPower * 0.9;

            out_image = new Mat();

            //影像複製，灰階處理
            if (image.Channels() == 3)
                Cv2.CvtColor(image, out_image, ColorConversionCodes.BGR2GRAY);
            else
                out_image = image.Clone();

            //二值化
            Cv2.Threshold(out_image, out_image, 6000, 255, ThresholdTypes.BinaryInv);

            ////閉運算
            //Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(3, 3));
            //Cv2.MorphologyEx(out_image, out_image, MorphTypes.Close, kernel, iterations: 2);

            ////填補孔洞
            //Mat floodFilled = out_image.Clone();
            //Cv2.BitwiseNot(floodFilled, floodFilled);

            //// 建立 mask，比原圖大 2 px
            //Mat mask = new Mat(floodFilled.Rows + 2, floodFilled.Cols + 2, MatType.CV_8UC1, Scalar.All(0));

            //// 從邊緣做 flood fill
            //try
            //{
            //    Cv2.FloodFill(floodFilled, mask, new Point(0, 0), new Scalar(255));
            //}
            //catch(Exception ex)
            //{
            //    int aa = 0;
            //}


            // 再反轉，填補原圖中的孔洞
            //Cv2.BitwiseNot(floodFilled, floodFilled);
            //Cv2.BitwiseOr(out_image, floodFilled, out_image);

        }
        #endregion

    }
}
