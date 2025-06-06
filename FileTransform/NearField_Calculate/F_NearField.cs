using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

using CommonFunction;


namespace FileTransform
{
    public partial class F_NearField : Form
    {
        #region parameter define 
        Task_NearField NF = new Task_NearField();
        #endregion

        #region private function
        private void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);


            //ShowHint();

            NF.ShowImage += ShowImage;

        }
        private void ShowImage(string path)
        {
            Tool.LoadImageToPicBx(PicBx_Picture, @"C:\Users\lankon\Desktop\新增資料夾\FileTransform\bin\Debug\Picture\Nearfile.png");
        }
        #endregion

        #region public function
        public void SetF_NearField(Panel pnl, F_NearField form)
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


        public F_NearField()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NF.Process_Teach();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {
            NF.Process_Continue();
            //GC.Collect();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}
