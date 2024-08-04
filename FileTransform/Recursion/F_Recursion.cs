using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommonFunction;

namespace FileTransform.Recursion
{
    

    public partial class F_Recursion : Form
    {
        #region parameter define 
        Task_Recursion RC = new Task_Recursion();
        Tool tool = new Tool();
        Dictionary<string, double> Dic_Picture = new Dictionary<string, double>();

        private bool _isDrawing;
        private Rectangle _rectangle;
        private Point _startPoint;

        #endregion

        #region private function
        private void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);


            //ShowHint();
            RC.ShowImage += ShowImage;
        }
        private void ShowImage(string path)
        {
            Dic_Picture = tool.LoadImageToPicBx(PicBx_Picture, Application.StartupPath + @"\Picture\" + "Recursion.png");
        }
        private double[] PicBxPositionToPixel(PictureBox pic, double mouse_x, double mouse_y, double image_w, double image_h)
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
            double[] show_pos = new double[2];
            show_pos[0] = (mouse_x - offset_x) * ratio;
            show_pos[1] = (mouse_y - offset_y) * ratio;

            return show_pos;
        }
        #endregion

        #region public function
        public void SetF_Recursion(Panel pnl, F_Recursion form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion

        public F_Recursion()
        {
            InitializeComponent();

            InitialApplication();

            //_rectangle = new Rectangle(50, 50, 100, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RC.Process_Teach();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            RC.Process_Continue();
            _rectangle = new Rectangle(_startPoint, new Size(0, 0));
            //GC.Collect();

        }

        private void PicBx_Picture_MouseClick(object sender, MouseEventArgs e)
        {
            // 獲取滑鼠點擊的位置
            System.Drawing.Point mousePosition = e.Location;

            Dic_Picture.TryGetValue("width", out double image_x);
            Dic_Picture.TryGetValue("height", out double image_y);

            double[] show = new double[2];
            show = PicBxPositionToPixel(PicBx_Picture, mousePosition.X, mousePosition.Y, image_x, image_y);

            string sx = show[0].ToString("0.0");
            string sy = show[1].ToString("0.0");

            ToTip_Image.Show($"X值: {sx}, " + $"Y值: {sy}", PicBx_Picture, e.X, e.Y - 15, 2000);
        }

        private void PicBx_Picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDrawing = true;
                _startPoint = e.Location;
                _rectangle = new Rectangle(_startPoint, new Size(0, 0));
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
                Point point = _rectangle.Location;
                double width = _rectangle.Width;
                double height = _rectangle.Height;

                Dic_Picture.TryGetValue("width", out double image_x);
                Dic_Picture.TryGetValue("height", out double image_y);

                double[] start_xy = PicBxPositionToPixel(PicBx_Picture, point.X, point.Y, image_x, image_y);
                double[] len = PicBxPositionToPixel(PicBx_Picture, width, height, image_x, image_y);
            }
        }

        private void PicBx_Picture_Paint(object sender, PaintEventArgs e)
        {
            if (_rectangle != Rectangle.Empty)
            {
                e.Graphics.DrawRectangle(Pens.Red, _rectangle);
            }
        }
    }
}
