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

namespace FileTransform
{
    public partial class F_Recursion_Setting : Form
    {
        #region private function
        private void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);
        }

        #endregion

        #region public function
        public void SetF_Recursion_Setting(Panel pnl, F_Recursion_Setting form)
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

        public F_Recursion_Setting()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void F_Recursion_Setting_FormClosed(object sender, FormClosedEventArgs e)
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
            openFileDialog.Filter = "TeachPicture|*.png|TeachPicture|*.tiff|All|*.*";

            // 如果用戶選擇了文件，顯示文件名
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;
            }

            TxtBx_TeachPath.Text = selectedFileName;
        }
    }
}
