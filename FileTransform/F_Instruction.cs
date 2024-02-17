using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileTransform
{
    public partial class F_Instruction : Form
    {
        public F_Instruction()
        {
            InitializeComponent();
        }

        public void LoadImagesToForm(string file_path)
        {
            List<string> imagePaths = new List<string>();
            string file_name = "";
            
            for(int i=1; i<= 10; i++)
            {
                file_name = i.ToString() + ".jpg";
                imagePaths.Add(file_path + file_name);
            }

            int y = 10; // 初始Y坐标
            foreach (var imagePath in imagePaths)
            {
                if (!File.Exists(imagePath))
                    break;
                
                PictureBox pictureBox = new PictureBox
                {
                    Image = Image.FromFile(imagePath),
                    Location = new Point(10, y),
                    SizeMode = PictureBoxSizeMode.AutoSize,
                };

                Pnl_ShowInfo.Controls.Add(pictureBox);
                y += pictureBox.Height + 10; // 更新Y坐标，10为两个PictureBox之间的间距
            }

            this.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            F_TransformFile FF = new F_TransformFile();
            this.Hide();
            FF.ShowDialog();            
        }
    }
}
