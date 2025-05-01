using CommonFunction;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tesseract;
using OpenCvSharp;
using System.Linq;

namespace FileTransform.Wafer_Align_Angle
{
    public partial class F_Wafer_Align_Angle : Form
    {
        #region parameter define
        Tool tool = new Tool();
        Dictionary<string, double> Dic_Picture = new Dictionary<string, double>();

        private bool _isDrawing;
        private Rectangle _rectangle;
        private System.Drawing.Point _startPoint;
        #endregion


        #region private function
        private void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);

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
        #endregion

        #region public function
        public void SetF_Wafer_Align_Angle(Panel pnl, F_Wafer_Align_Angle form)
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
            Dic_Picture = tool.LoadImageToPicBx(PicBx_Picture, Application.StartupPath + @"\Picture\" + "Calculate.png");
        }
        public void ShowOCR_Result(string res)
        {
            if (Labl_OCR_Result.InvokeRequired)
            {
                Labl_OCR_Result.Invoke(new Action(() => {
                    Labl_OCR_Result.Text = res;
                }));
            }
            else
            {
                Labl_OCR_Result.Text = res;
            }
        }
        #endregion



        public F_Wafer_Align_Angle()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            //強制MainThread至Idle狀態
            Scope.ProcessTask.ForceAction();
            //創建Task Class
            Scope.ProcessTask.SetTask<Task_AngleCalculate>();
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
            _rectangle = new Rectangle(_startPoint, new System.Drawing.Size(0, 0));
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
            //Mat img = Cv2.ImRead(ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath));

            //Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"prg.png", img);

            //Mat result = new Mat();
            //Mat mat = new Mat(2, 3, MatType.CV_64F);
            //mat.Set(0, 0, 1);
            //mat.Set(0, 1, 0);
            //mat.Set(0, 2, 3); // 向右平移 50
            //mat.Set(1, 0, 0);
            //mat.Set(1, 1, 1);
            //mat.Set(1, 2, 0);

            //Cv2.WarpAffine(img, result, mat, img.Size(), InterpolationFlags.Linear, BorderTypes.Constant, Scalar.White);
            //Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"Shift.png", result);


            // 原圖
            Mat image = Cv2.ImRead(ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath));


            // 建立仿射平移矩陣 (2x3)
            Mat translationMatrix = new Mat(2, 3, MatType.CV_32F);
            translationMatrix.Set<float>(0, 0, 1);
            translationMatrix.Set<float>(0, 1, 0);
            translationMatrix.Set<float>(0, 2, 100);

            translationMatrix.Set<float>(1, 0, 0);
            translationMatrix.Set<float>(1, 1, 1);
            translationMatrix.Set<float>(1, 2, 0);


            // 平移後圖像
            Mat movedImage = new Mat();
            Cv2.WarpAffine(image, movedImage, translationMatrix, image.Size(),
                           InterpolationFlags.Linear, BorderTypes.Constant, Scalar.White);

            // 儲存或顯示
            //Cv2.ImShow("Shifted", movedImage);
            Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"Shift.png", movedImage);


            //// 1. 讀取圖片（請換成你的路徑）
            //string imagePath = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
            //Mat image = Cv2.ImRead(imagePath, ImreadModes.Color);

            //if (image.Empty())
            //{
            //    Console.WriteLine("❌ 無法載入圖片，請確認路徑正確。");
            //    return;
            //}

            //// 2. 轉灰階 & 二值化
            //Mat gray = new Mat();
            //Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
            //Mat binary = new Mat();
            //Cv2.Threshold(gray, binary, 100, 255, ThresholdTypes.Binary);

            //Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"threshold.png", binary);

            //// 3. 找輪廓
            //OpenCvSharp.Point[][] contours;
            //HierarchyIndex[] hierarchy;
            //Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            //if (contours.Length == 0)
            //{
            //    Console.WriteLine("⚠️ 找不到輪廓。");
            //    return;
            //}

            //// 4. 找最大輪廓
            //var biggest = contours.OrderByDescending(c => Cv2.ContourArea(c)).First();
            //var boundingRect = Cv2.BoundingRect(biggest);

            //// 5. 計算矩形中心
            //int centerX = boundingRect.X + boundingRect.Width / 2;
            //int centerY = boundingRect.Y + boundingRect.Height / 2;

            //// 6. 設定平移距離（以中心為基準向右 50）
            //int dx = 50;
            //int dy = 0;

            //// 7. 建立平移矩陣（注意：這裡用 Mat 构造 2x3 affine）
            //Mat translationMatrix = new Mat(2, 3, MatType.CV_64F);
            //translationMatrix.Set(0, 0, 1);
            //translationMatrix.Set(0, 1, 0);
            //translationMatrix.Set(0, 2, dx);
            //translationMatrix.Set(1, 0, 0);
            //translationMatrix.Set(1, 1, 1);
            //translationMatrix.Set(1, 2, dy);

            //// 8. 套用仿射平移
            //Mat shifted = new Mat();
            //Cv2.WarpAffine(image, shifted, translationMatrix, image.Size(), InterpolationFlags.Linear, BorderTypes.Constant, Scalar.White);

            //// 9. 畫矩形對照：紅色為原位置，綠色為平移後
            //Cv2.Rectangle(image, boundingRect, Scalar.Red, 2); // 原圖
            //Cv2.Rectangle(shifted, new OpenCvSharp.Rect(boundingRect.X + dx, boundingRect.Y + dy, boundingRect.Width, boundingRect.Height), Scalar.Green, 2);

            //// 10. 儲存結果
            //Cv2.ImWrite(Application.StartupPath + @"\Picture\" + $"Shift.png", shifted);
            ////string outputPath = @"C:\your_image_path\shifted.png";
            ////Cv2.ImWrite(outputPath, shifted);


            #region OCR
            //using (Tesseract ocr = new Tesseract(@"./tessdata", "eng", OcrEngineMode.TesseractOnly))
            //{
            //    ocr.SetImage(img);
            //    ocr.Recognize();
            //    string result = ocr.GetUTF8Text();
            //    Console.WriteLine("辨識結果：");
            //    Console.WriteLine(result);
            //}

            using (var engine = new TesseractEngine(@"C:\Users\lankon\Desktop\CSharp\FileTransform\bin\Debug", "eng_custom", EngineMode.Default))
            {
                string path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
                Mat src = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);

                double thresholdValue = ApplicationSetting.Get_Double_Recipe((int)FormItem.TxtBx_Threshold);
                //double maxValue = 65535; //16位元影像,最大值65536
                double maxValue = 255; //8位元影像,最大值256
                thresholdValue = 170;
                Cv2.Threshold(src, src, thresholdValue, maxValue, ThresholdTypes.Binary);

                //2.儲存臨時處理圖像（Tesseract 需要 bitmap）
                string tempImagePath = "temp_ocr.png";
                Cv2.ImWrite(tempImagePath, src);

                // 1. 讀取圖片並前處理
                //Mat src = Cv2.ImRead("OCR.png"); // 你的圖片
                Mat gray = new Mat();
                Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(gray, gray, 250, 255, ThresholdTypes.Binary); // 可調參數

                // 2. 儲存臨時處理圖像（Tesseract 需要 bitmap）
                //string tempImagePath = "temp_ocr.png";
                Cv2.ImWrite(tempImagePath, gray);

                using (var img = Pix.LoadFromFile(tempImagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        Console.WriteLine("辨識結果：");
                        Console.WriteLine(text);
                    }
                }
            }
            #endregion

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
            //創建Task Class
            Scope.ProcessTask.SetTask<Task_Server>();
            //添加必要事件
            Scope.ProcessTask.UpdateTaskState += UpdateTask;
            Scope.ProcessTask.SetPauseAbortContinue += SetPauseAbortContinue;
            Scope.ProcessTask.SetErrorMsg += UpdateError;
            Scope.ProcessTask.base_task.UpdateTaskState += UpdateTask;
            Scope.ProcessTask.base_task.SetForm(this);
            //執行
            Scope.ProcessTask.Run();

            
        }

        private void Btn_ClientTest_Click(object sender, EventArgs e)
        {
            //開啟Server
            tool.CallExecute(@"C:\Users\lankon\Desktop\Debug\FileTransform.exe", "CallServer");

            Thread.Sleep(50);
            
            TCPIP_Client tCPIP_Client = new TCPIP_Client();

            tCPIP_Client.Open("127.0.0.1", 87);

            tCPIP_Client.SendMessage("WaferAlign");

            //Thread.Sleep(100);
            
            string IsOK = tCPIP_Client.ReceiveMessage();

            tool.SaveHistoryToFile("result:" + IsOK);

            tCPIP_Client.Close();

            tool.CloseExecute(@"C:\Users\lankon\Desktop\Debug\FileTransform.exe");

        }

        private void PicBx_Picture_Paint(object sender, PaintEventArgs e)
        {
            if (_rectangle != Rectangle.Empty)
            {
                e.Graphics.DrawRectangle(Pens.Red, _rectangle);
                
            }
        }

        private void PicBx_Picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDrawing = true;
                _startPoint = e.Location;
                _rectangle = new Rectangle(_startPoint, new System.Drawing.Size(0, 0));
            }
        }

        private void PicBx_Picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                var width = e.X - _startPoint.X;
                var height = e.Y - _startPoint.Y;
                _rectangle = new Rectangle(_startPoint.X, _startPoint.Y, width, height);
                PicBx_Picture.Invalidate();
            }
        }

        private void PicBx_Picture_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                _isDrawing = false;
                PicBx_Picture.Invalidate();
                System.Drawing.Point point = _rectangle.Location;
                int width = _rectangle.Width;
                int height = _rectangle.Height;

                Dic_Picture.TryGetValue("width", out double image_x);
                Dic_Picture.TryGetValue("height", out double image_y);

                if (Scope.status == 1)
                    Scope.start_xy = PicBxPositionToPixel(PicBx_Picture, point.X, point.Y, image_x, image_y);
                else if (Scope.status == 2)
                    Scope.orgin_xy = PicBxPositionToPixel(PicBx_Picture, point.X, point.Y, image_x, image_y);

                // 計算縮放比例
                double ratio_x = image_x / PicBx_Picture.Width;
                double ratio_y = image_y / PicBx_Picture.Height;
                double ratio = Math.Max(ratio_x, ratio_y);

                if (Scope.status == 1)
                {
                    Scope.len = new int[2];
                    Scope.len[0] = (int)(width * ratio);
                    Scope.len[1] = (int)(height * ratio);
                }
                else if (Scope.status == 2)
                {
                    Scope.orgin_len = new int[2];
                    Scope.orgin_len[0] = (int)(width * ratio);
                    Scope.orgin_len[1] = (int)(height * ratio);
                }

            }
        }

        private void Btn_BatchCalculate_Click(object sender, EventArgs e)
        {
            string folderPath = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_BatchPath);
            string[] files = Directory.GetFiles(folderPath);
            int count = 0;
            Scope.batch_path.Clear();

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);

                // 根據你的需求篩選特定副檔名的檔案，例如 .txt 和 .jpg
                if (extension == ".JPG" || extension == ".jpg" || extension == ".png")
                {
                    Scope.batch_path.Add(file);
                    count++;
                }
            }

            #region 執行Task
            //強制MainThread至Idle狀態
            Scope.ProcessTask.ForceAction();
            //創建Task Class
            Scope.ProcessTask.SetTask<Task_AngleCalculate>("Batch_Calculate");
            //添加必要事件
            Scope.ProcessTask.UpdateTaskState += UpdateTask;
            Scope.ProcessTask.SetPauseAbortContinue += SetPauseAbortContinue;
            Scope.ProcessTask.SetErrorMsg += UpdateError;
            Scope.ProcessTask.base_task.UpdateTaskState += UpdateTask;
            Scope.ProcessTask.base_task.SetForm(this);
            //執行
            Scope.ProcessTask.Run();
            #endregion

        }
    }
}
