﻿using System;
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
            NF_Algorithm NF_Cal = new NF_Algorithm();
            Mat image = new Mat();

            string path = ApplicationSetting.Get_String_Recipe((int)FormItem.TxtBx_TeachPath);
            image = new Mat(path, ImreadModes.AnyDepth | ImreadModes.Grayscale);
            NF_Cal.Threshold_Ratio = 0.35;


            //Teach AutoFocus
            NF_Cal.Detect_Position(image);

            //擷取左下角AOI_Size影像
            int AOI_Size = (int)(5 * NF_Cal.Emitter_Diameter[0]);
            int Capture_X = (int)(NF_Cal.Emitter_PosX[0] - 0.5 * AOI_Size);
            int Capture_Y = (int)(NF_Cal.Emitter_PosY[0] - 0.5 * AOI_Size);
            Rect roi = new Rect(Capture_X, Capture_Y, AOI_Size, AOI_Size);
            Mat Capture_Image = new Mat(image, roi).Clone();




            image.Dispose();
            Capture_Image.Dispose();

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
