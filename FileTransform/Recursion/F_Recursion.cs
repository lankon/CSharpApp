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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RC.Process_Teach();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            RC.Process_Continue();
            //GC.Collect();

        }

        private void PicBx_Picture_Click(object sender, EventArgs e)
        {

        }

        private void PicBx_Picture_MouseClick(object sender, MouseEventArgs e)
        {
            // 獲取滑鼠點擊的位置
            System.Drawing.Point mousePosition = e.Location;

            Dic_Picture.TryGetValue("width", out double image_x);
            Dic_Picture.TryGetValue("height", out double image_y);

            // 計算縮放比例
            double ratio_x = image_x / PicBx_Picture.Width;
            double ratio_y = image_y / PicBx_Picture.Height;
            double ratio = Math.Max(ratio_x, ratio_y);

            // 計算位移量
            double offset_x = 0;
            double offset_y = 0;
            if (ratio_x > ratio_y)
                offset_y = Pnl_Picture.Height / 2 - image_y / 2 / ratio;
            else
                offset_x = Pnl_Picture.Width / 2 - image_x / 2 / ratio;

            // 像素座標
            double show_x = (mousePosition.X - offset_x) * ratio;
            double show_y = (mousePosition.Y - offset_y) * ratio;

            string sx = show_x.ToString("0.0");
            string sy = show_y.ToString("0.0");

            ToTip_Image.Show($"X值: {sx}, " + $"Y值: {sy}", PicBx_Picture, e.X, e.Y - 15, 2000);
        }
    }
}
