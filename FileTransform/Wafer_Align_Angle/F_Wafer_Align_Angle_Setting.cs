using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

using CommonFunction;

namespace FileTransform.Wafer_Align_Angle
{
    public partial class F_Wafer_Align_Angle_Setting : Form
    {
        #region parameter define
        Tool tool = new Tool();
        #endregion

        #region private function
        private void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);
        }

        #endregion

        #region public function
        public void SetF_Wafer_Align_Angle_Setting(Panel pnl, F_Wafer_Align_Angle_Setting form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion

        public F_Wafer_Align_Angle_Setting()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void F_Wafer_Align_Angle_Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);
            ApplicationSetting.ReadAllRecipe<FormItem>();
        }

        private void TxtBx_TeachPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFileName = "";

            // 設置文件選擇對話框的屬性
            openFileDialog.Title = "Select TeahPicture";
            openFileDialog.Filter = "TeachPicture|*.bmp|" +
                                    "TeachPicture|*.jpg|" +
                                    "TeachPicture|*.tiff|" +
                                    "All|*.*";

            // 如果用戶選擇了文件，顯示文件名
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
            }

            if (selectedFileName == "")
            {
                MessageBox.Show("Teach Path Set Fail");
                tool.SaveHistoryToFile("Teach Path設定失敗");
                return;
            }

            TxtBx_TeachPath.Text = selectedFileName;
        }

        private void TxtBx_BatchPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFileName = "";

            // 設置文件選擇對話框的屬性
            openFileDialog.Title = "Select TeahPicture";
            openFileDialog.Filter = "TeachPicture|*.bmp|" +
                                    "TeachPicture|*.jpg|" +
                                    "TeachPicture|*.tiff|" +
                                    "All|*.*";

            // 如果用戶選擇了文件，顯示文件名
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
            }

            if (selectedFileName == "")
            {
                MessageBox.Show("Bath Path Set Fail");
                tool.SaveHistoryToFile("Batch Path設定失敗");
                return;
            }

            TxtBx_BatchPath.Text = Path.GetDirectoryName(selectedFileName);
        }

        
    }
}
