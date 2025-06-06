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


        #region private function
        private void CalculateMaxPower(Mat image)
        {
            

            //// 讀取 8-bit 或 16-bit 灰階影像
            //Mat image = Cv2.ImRead(path, ImreadModes.Grayscale);  // 8-bit
            //                                                      // Mat image = Cv2.ImRead(path, ImreadModes.Unchanged); // 若要支援 16-bit

            //// 總像素加總（支援所有深度）
            //Scalar sum = Cv2.Sum(image);

            //// 擷取灰階通道總和
            //double valueSum = sum.Val0;
        }
        #endregion

    }
}
