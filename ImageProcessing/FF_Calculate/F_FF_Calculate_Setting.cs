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

namespace ImageProcessing.FF_Calculate
{
    public partial class F_FF_Calculate_Setting : Form
    {
        #region parameter define
        Tool tool = new Tool();
        #endregion

        #region private function
        private void InitialApplication()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>(read_path: "FF_Calculate.exe.Config");
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this, "FF_Calculate.exe.Config");
        }
        private void DataGridInitial(DataGridView dataGridView)
        {
            ////Title置中
            //dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////內容置中
            //dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////禁止調整欄位寬度
            //dataGridView.AllowUserToResizeColumns = false;
            ////3.禁止調整欄位高度（標題列高度固定）
            //dataGridView.AllowUserToResizeRows = false;
            //dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ////停止使用者新增列
            //dataGridView.AllowUserToAddRows = false;
            //dataGridView.RowHeadersVisible = false;
        }
        private void DataGrid_AddRow(DataGridView dataGridView, string[] context)
        {
            if (context.Length != dataGridView.ColumnCount)
            {
                tool.SaveHistoryToFile("新增行數與DataGrid行數不一致");
                return;
            }

            dataGridView.Rows.Add(context);
        }
        private void DataGrid_DeleteRow(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow != null && !dataGridView.CurrentRow.IsNewRow)
            {
                dataGridView.Rows.Remove(dataGridView.CurrentRow);
            }
        }
        private void DataGrid_RowUp(DataGridView dataGridView)
        {
            // 確保選取的列不為 null 且不是第一列
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index > 0)
            {
                int currentIndex = dataGridView.CurrentRow.Index;
                int previousIndex = currentIndex - 1;

                // 交換當前列與上一列
                var currentRow = dataGridView.Rows[currentIndex];
                var previousRow = dataGridView.Rows[previousIndex];

                // 交換資料
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    var temp = currentRow.Cells[i].Value;
                    currentRow.Cells[i].Value = previousRow.Cells[i].Value;
                    previousRow.Cells[i].Value = temp;
                }

                // 更新選擇列
                dataGridView.CurrentCell = previousRow.Cells[dataGridView.CurrentCell.ColumnIndex];
            }
            else
            {
                return;
            }
        }
        private bool DataGrid_DataSave(DataGridView dataGridView, string file_name)
        {
            bool res = false;

            tool.SaveHistoryToFile($"{file_name}儲存Start");

            string FolderPath = Application.StartupPath + @"\Setting";
            tool.CreateFolder(FolderPath);

            string file_path = FolderPath + @"\" + file_name;

            // 將 DataGridView 資料轉換為 DataTable
            DataTable dt = new DataTable();

            // 假設 DataGridView 已經有資料
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dt.Columns.Add(column.Name);
            }

            // 把每一列資料加到 DataTable 中
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)  // 忽略最後一行空白列
                {
                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Value;
                    }
                    dt.Rows.Add(dataRow);
                }
            }

            // 儲存為 XML 檔案
            try
            {
                dt.WriteXml(file_path);
                res = true;
            }
            catch(Exception ex)
            {
                res = false;
                return res;
            }

            tool.SaveHistoryToFile($"{file_name}儲存End");

            return res;
        }
        private bool DataGrid_DataLoad()
        {
            bool res = false;


            return res;
        }
        private void DataGrid_RowDown(DataGridView dataGridView)
        {
            // 確保選取的列不為 null 且不是最後一列
            if (dataGridView.CurrentRow != null && dataGridView1.CurrentRow.Index < dataGridView1.Rows.Count - 1)
            {
                int currentIndex = dataGridView.CurrentRow.Index;
                int previousIndex = currentIndex + 1;

                // 交換當前列與下一列
                var currentRow = dataGridView.Rows[currentIndex];
                var previousRow = dataGridView.Rows[previousIndex];

                // 交換資料
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    var temp = currentRow.Cells[i].Value;
                    currentRow.Cells[i].Value = previousRow.Cells[i].Value;
                    previousRow.Cells[i].Value = temp;
                }

                // 更新選擇列
                dataGridView.CurrentCell = previousRow.Cells[dataGridView.CurrentCell.ColumnIndex];
            }
            else
            {
                return;
            }
        }
        #endregion

        #region public function
        public void SetF_FF_Calculate_Setting(Panel pnl, F_FF_Calculate_Setting form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();

            DataGridInitial(dataGridView1);
        }
        #endregion

        public F_FF_Calculate_Setting()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void F_Wafer_Align_Angle_Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this,"FF_Calculate.exe.Config");
            ApplicationSetting.ReadAllRecipe<FormItem>(read_path: "FF_Calculate.exe.Config");
        }

        private void TxtBx_TeachPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFileName = "";

            // 設置文件選擇對話框的屬性
            openFileDialog.Title = "Select TeahPicture";
            openFileDialog.Filter = "TeachPicture|*.bmp|TeachPicture|*.jpg|TeachPicture|*.tiff|All|*.*";

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
            openFileDialog.Filter = "TeachPicture|*.bmp|TeachPicture|*.tiff|All|*.*";

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

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            string[] context = new string[] { "None", "None", "None", "None", "1", "1", "1" };

            DataGrid_AddRow(dataGridView1, context);
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            DataGrid_DeleteRow(dataGridView1);
        }

        private void Btn_RowUp_Click(object sender, EventArgs e)
        {
            DataGrid_RowUp(dataGridView1);
        }

        private void Btn_RowDown_Click(object sender, EventArgs e)
        {
            DataGrid_RowDown(dataGridView1);
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            DataGrid_DataSave(dataGridView1, "IO.xml");
        }
    }
}
