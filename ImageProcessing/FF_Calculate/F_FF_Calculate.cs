using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using OpenCvSharp;

using CommonFunction;

namespace ImageProcessing.FF_Calculate
{
    public partial class F_FF_Calculate : Form
    {
        #region parameter define
        Dictionary<string, double> Dic_Picture = new Dictionary<string, double>();
        #endregion

        #region private function
        private void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>("FF_Calculate.exe.Config");
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this, "FF_Calculate.exe.Config");

            //ShowHint();
        }
        
        private int[] PicBxPositionToPixel(PictureBox pic, double mouse_x, double mouse_y, double image_w, double image_h)
        {
            // 功能：將PictureBox點擊的位置轉換成實際像素
            // 參數：pic->PictureBox , mouse_x->滑鼠x位置, mouse_y->滑鼠y位置, image_h->影像高度, image_w->影像寬度
            // 回傳: 滑鼠點擊位置的圖片像素xy座標

            // 計算縮放比例
            double ratio_x = image_w / pic.Width;
            double ratio_y = image_h / pic.Height;
            double ratio = Math.Max(ratio_x, ratio_y);

            // 計算位移量
            double offset_x = 0;
            double offset_y = 0;
            if (ratio_x > ratio_y)
                offset_y = Pnl_Picture.Height / 2 - image_h / 2 / ratio;
            else
                offset_x = Pnl_Picture.Width / 2 - image_w / 2 / ratio;

            // 像素座標
            int[] show_pos = new int[2];
            show_pos[0] = (int)((mouse_x - offset_x) * ratio);
            show_pos[1] = (int)((mouse_y - offset_y) * ratio);

            return show_pos;
        }
        private void updateTxtBx_Invoke(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
                textBox.Invoke(new Action(() => textBox.Text = text));
            else
                textBox.Text = text;
        }
        #endregion

        #region public function
        public void SetF_FF_Calculate(Panel pnl, F_FF_Calculate form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);

            form.Hide();
        }
        public void ShowImage(string path)
        {
            Dic_Picture = Tool.LoadImageToPicBx(PicBx_Picture, Application.StartupPath + @"\Picture\" + "Calculate.png");
        }
        public void ShowFarFieldResult(double angle = 0.0, double eye_safe = 0.0, double valley = 0.0)
        {
            updateTxtBx_Invoke(TxtBx_RltAngle, angle.ToString("0.000"));
            updateTxtBx_Invoke(TxtBx_RltEyeSafe, eye_safe.ToString("0.000"));
            updateTxtBx_Invoke(TxtBx_RltValley, valley.ToString("0.000"));
        }
        public void ShowTestTimeResult(int millsecond)
        {
            updateTxtBx_Invoke(TxtBx_RltTestTime, millsecond.ToString());
        }
        #endregion



        public F_FF_Calculate()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            //強制MainThread至Idle狀態
            Scope.ProcessTask.ForceAction();
            //創建Task Class
            Scope.ProcessTask.SetTask<Task_FF_Angle_Calculate>();
            //添加必要事件
            Scope.ProcessTask.UpdateTaskState += UpdateTask;
            Scope.ProcessTask.SetPauseAbortContinue += SetPauseAbortContinue;
            Scope.ProcessTask.SetErrorMsg += UpdateError;
            Scope.ProcessTask.base_task.UpdateTaskState += UpdateTask;
            Scope.ProcessTask.base_task.SetForm(this);
            //執行
            Scope.ProcessTask.Run();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            Scope.ProcessTask.GoToContinue();
        }

        private void PicBx_Picture_MouseClick(object sender, MouseEventArgs e)
        {
            // 獲取滑鼠點擊的位置
            System.Drawing.Point mousePosition = e.Location;

            Dic_Picture.TryGetValue("width", out double image_x);
            Dic_Picture.TryGetValue("height", out double image_y);

            int[] show = new int[2];
            show = PicBxPositionToPixel(PicBx_Picture, mousePosition.X, mousePosition.Y, image_x, image_y);

            string sx = show[0].ToString("0.0");
            string sy = show[1].ToString("0.0");

            ToTip_Image.Show($"X值: {sx}, " + $"Y值: {sy}", PicBx_Picture, e.X, e.Y - 15, 2000);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //NF_Algorithm NF_Cal = new NF_Algorithm();
            //Mat image = new Mat();

            //string path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
            //image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);
            //NF_Cal.Threshold_Ratio = 0.35;


            ////Teach AutoFocus
            //NF_Cal.Detect_Position(image);

            ////擷取左下角AOI_Size影像
            //int AOI_Size = (int)(5 * NF_Cal.Emitter_Diameter[0]);
            //int Capture_X = (int)(NF_Cal.Emitter_PosX[0] - 0.5 * AOI_Size);
            //int Capture_Y = (int)(NF_Cal.Emitter_PosY[0] - 0.5 * AOI_Size);
            //Rect roi = new Rect(Capture_X, Capture_Y, AOI_Size, AOI_Size);
            //Mat Capture_Image = new Mat(image, roi).Clone();




            //image.Dispose();
            //Capture_Image.Dispose();
            TestImage_1();
        }

        private void TestImage_1()  //ChatGpt
        {
            // 讀取影像
            Mat img = Cv2.ImRead(@"C:\Users\leo_li\Desktop\BDSA\WG正面軸環光光源不同大小\正面WG_環光滿.png");

            // 轉灰階
            Mat gray = new Mat();
            Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);

            // 高斯模糊
            Mat blur = new Mat();
            Cv2.GaussianBlur(gray, blur, new OpenCvSharp.Size(5, 5), 0);

            // Canny 邊緣偵測
            Mat edges = new Mat();
            Cv2.Canny(gray, edges, 20, 50);

            // 找輪廓
            Cv2.FindContours(edges, out OpenCvSharp.Point[][] contours, out HierarchyIndex[] hierarchy,
                             RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // 過濾小輪廓，只留下大面積的
            var largeContours = new List<OpenCvSharp.Point[]>();
            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                if (area > 100)
                    largeContours.Add(contour);
            }

            // 畫出大輪廓
            Mat result = img.Clone();
            Cv2.DrawContours(result, largeContours, -1, new Scalar(0, 255, 0), 2);

            Cv2.ImWrite(@"D:\GPT.png", result);

            // 顯示結果
            //Cv2.ImShow("Chip Boundary", result);
            //Cv2.WaitKey(0);
            //Cv2.DestroyAllWindows();
        }

        private void TestImage()    //Gemini
        {
            // 替換成您影像的實際路徑
            string imagePath = @"C:\Users\leo_li\Desktop\BDSA\WG正面軸環光光源不同大小\正面WG_右環光滿.png";
            string outputImagePath = @"D:\aa.png";
            try
            {
                // 1. 讀取影像
                // ImreadModes.Color 表示以彩色模式讀取，如果您的處理不依賴顏色，
                // 建議使用 ImreadModes.Grayscale
                Mat src = Cv2.ImRead(imagePath, ImreadModes.Grayscale);

                if (src.Empty())
                {
                    Console.WriteLine($"無法讀取影像：{imagePath}");
                    return;
                }

                // 1.1. 轉換為灰度圖
                Mat gray = new Mat();
                // 從 BGR（彩色）轉換到 GRAY（灰度）
                //Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                // 1.2. 應用閾值（二值化）
                Mat binary = new Mat();
                // 這裡使用 Otsu's Method (大津法) 自動計算最佳閾值，
                // 通常用於前景背景差異明顯的影像。
                double otsuThreshold = Cv2.Threshold(
                    src,                   // 輸入：灰度影像
                    binary,                 // 輸出：二值化影像
                    0,                      // 閾值（因為使用 Otsu，這裡設為 0 即可）
                    255,                    // 最大值
                    ThresholdTypes.Binary | ThresholdTypes.Otsu // 類型：二值化 + 大津法
                );


                // 4. 儲存影像（關鍵步驟：Cv2.ImWrite）
                bool success = Cv2.ImWrite(@"D:\二值化.png", binary);


                // 2. 定義核心（Kernel / Structuring Element）
                // 核心的大小和形狀決定了侵蝕和膨脹的程度。
                // 這裡使用 5x5 的矩形核心。
                Mat kernel = Cv2.GetStructuringElement(
                    MorphShapes.Rect, // 形狀：矩形 (Rect)
                    new OpenCvSharp.Size(2, 2)    // 大小：5x5
                );

                // 3. 執行開運算 (侵蝕後膨脹)
                Mat dst = new Mat();
                // 使用 MorphologyEx 函式執行開運算（MorphTypes.OPEN）
                Cv2.MorphologyEx(
                    binary,            // 輸入影像
                    dst,            // 輸出影像
                    MorphTypes.Open, // 運算類型：開運算 (Open)
                    kernel,         // 核心
                    new OpenCvSharp.Point(-1, -1), // 錨點 (通常設為 -1, -1)
                    1               // 迭代次數 (Iterations)
                );


                Cv2.ImWrite(@"D:\開運算.png", dst);

                // ===================================
                // 階段二：尋找輪廓 (Find Contours)
                // ===================================

                // 宣告儲存輪廓的列表
                OpenCvSharp.Point[][] contours;
                HierarchyIndex[] hierarchy;

                // 尋找輪廓
                // RetrieveMode.External 只找最外層輪廓
                // ContourMethod.ChainApproxSimple 壓縮輪廓點，減少儲存空間
                Cv2.FindContours(
                    dst,      // 輸入：經過處理的二值化影像
                    out contours,
                    out hierarchy,
                    RetrievalModes.External,
                    ContourApproximationModes.ApproxSimple
                );

                Console.WriteLine($"找到輪廓數量: {contours.Length}");


                // ===================================
                // 階段三：計算並繪製最小外接旋轉矩形
                // ===================================

                // 2.3. 準備繪圖畫布 (將單通道的 processed 影像轉為三通道 BGR)
                Mat drawing = new Mat();

                // 假設您的形態學運算結果是 processed
                // (如果您使用單獨變數 dst 儲存，請替換 processed 為 dst)
                Cv2.CvtColor(
                    src,                           // 輸入：形態學運算後的單通道（黑白）影像
                    drawing,                             // 輸出：三通道（彩色）影像
                    ColorConversionCodes.GRAY2BGR        // 轉換類型：灰度轉 BGR（彩色）
                );


                // 迭代每一個找到的輪廓
                for (int i = 0; i < contours.Length; i++)
                {
                    // 1. 忽略太小的輪廓 (可選，防止處理噪點)
                    double area = Cv2.ContourArea(contours[i]);
                    if (area < 1000000) continue;

                    // 2. 計算最小外接旋轉矩形 (RotatedRect)
                    RotatedRect minRect = Cv2.MinAreaRect(contours[i]);

                    // 3. 獲取矩形的四個頂點
                    Point2f[] rectPoints = minRect.Points();

                    // 4. 在繪圖影像上繪製這四條線
                    // 顏色使用綠色 (0, 255, 0)，粗細為 2
                    Scalar color = new Scalar(0, 255, 0);

                    for (int j = 0; j < 4; j++)
                    {
                        Cv2.Line(
                            drawing,                            // 繪圖影像
                            (OpenCvSharp.Point)rectPoints[j],               // 起點
                            (OpenCvSharp.Point)rectPoints[(j + 1) % 4],     // 終點 (循環到第 0 點)
                            color,                              // 顏色
                            2                                   // 線條粗細
                        );
                    }
                }


                // 4. 儲存影像（關鍵步驟：Cv2.ImWrite）
                Cv2.ImWrite(outputImagePath, drawing);

                // 釋放記憶體
                src.Dispose();
                dst.Dispose();
                kernel.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }
        }
    

        private void UpdateTask(string msg)
        {

        }
        private void UpdateError(string msg)
        {

        }
        private void SetPauseAbortContinue(TASK_STATUS status)
        {

        }

        public void Btn_ServeTest_Click(object sender, EventArgs e)
        {
            ////創建Task Class
            //Scope.ProcessTask.SetTask<Task_Server>();
            ////添加必要事件
            //Scope.ProcessTask.UpdateTaskState += UpdateTask;
            //Scope.ProcessTask.SetPauseAbortContinue += SetPauseAbortContinue;
            //Scope.ProcessTask.SetErrorMsg += UpdateError;
            //Scope.ProcessTask.base_task.UpdateTaskState += UpdateTask;
            //Scope.ProcessTask.base_task.SetForm(this);
            ////執行
            //Scope.ProcessTask.Run();

            
        }

        private void Btn_ClientTest_Click(object sender, EventArgs e)
        {
            ////開啟Server
            //Tool.CallExecute(@"C:\Users\lankon\Desktop\Debug\FileTransform.exe", "CallServer");

            //Thread.Sleep(50);
            
            //TCPIP_Client tCPIP_Client = new TCPIP_Client();

            //tCPIP_Client.Open("127.0.0.1", 87);

            //tCPIP_Client.SendMessage("WaferAlign");

            ////Thread.Sleep(100);
            
            //string IsOK = tCPIP_Client.ReceiveMessage();

            //Tool.SaveHistoryToFile("result:" + IsOK);

            //tCPIP_Client.Close();

            //Tool.CloseExecute(@"C:\Users\lankon\Desktop\Debug\FileTransform.exe");

        }
    }
}
