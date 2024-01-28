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
using System.Text.RegularExpressions;
using System.Reflection;

namespace CSharpApp
{
    public partial class F_TransformFile : Form
    {
        Tool tool = new Tool();
        
        public F_TransformFile()
        {
            InitializeComponent();

            this.groupBox1.Paint += groupBox_Paint;
            //this.GpBx_Function.Paint += groupBox_Paint;
        }

        void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gBox = (GroupBox)sender;

            e.Graphics.Clear(gBox.BackColor);
            e.Graphics.DrawString(gBox.Text, gBox.Font, Brushes.Black, 10, 1);
            var vSize = e.Graphics.MeasureString(gBox.Text, gBox.Font);
            e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 8, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Black, vSize.Width + 8, vSize.Height / 2, gBox.Width - 2, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 1, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, gBox.Height - 2, gBox.Width - 2, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Black, gBox.Width - 2, vSize.Height / 2, gBox.Width - 2, gBox.Height - 2);
        }

        public List<Dictionary<string, string>> ReadCsvFile(String Path)
        {
            // 使用 Dictionary 來儲存資料
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            bool StartReadFile = false;
            try
            {
                // 使用 StreamReader 來讀取檔案
                using (StreamReader sr = new StreamReader(Path))
                {
                    if (sr == null) return data;

                    string[] headers = new string[200];// = sr.ReadLine().Split(',');

                    // 逐行讀取 CSV 檔案
                    while (!sr.EndOfStream)
                    {
                        // 讀取一行
                        string line = sr.ReadLine();

                        // 使用逗號分隔解析欄位
                        string[] fields = line.Split(',');

                        if (fields[0].Trim() == "PosX")
                        {
                            for (int i = 0; i < fields.Count(); i++)
                            {
                                headers[i] = fields[i].Trim();
                            }

                            StartReadFile = true;
                        }

                        if (StartReadFile == true)
                        {
                            // 使用 Dictionary 來儲存每一行的資料
                            Dictionary<string, string> row = new Dictionary<string, string>();

                            // 將資料與欄位名稱對應起來
                            for (int i = 0; i < fields.Length; i++)
                            {
                                row[headers[i]] = fields[i];
                            }

                            // 將此行的資料加入到 List 中
                            data.Add(row);
                        }
                    }

                    return data;
                }
            }
            catch
            {
                return data;
            }
        }

        public void FixFileName(string folderPath, int ShiftX, int ShiftY)
        {
            // 檢查資料夾是否存在
            if (Directory.Exists(folderPath))
            {
                // 取得資料夾內的所有檔案
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    // 建立新的檔案名稱
                    string newFileName = "";
                    string FileName = Path.GetFileName(filePath);

                    // 使用正則表達式提取數字
                    Match xMatch = Regex.Match(FileName, @"X(-?\d+(\.\d+)?)");
                    Match yMatch = Regex.Match(FileName, @"Y(-?\d+(\.\d+)?)");
               
                    if (xMatch.Success && yMatch.Success)
                    {
                        string xValue = xMatch.Groups[1].Value;
                        string yValue = yMatch.Groups[1].Value;

                        xValue = (Int32.Parse(xValue) + ShiftX).ToString();
                        yValue = (Int32.Parse(yValue) + ShiftY).ToString();

                        newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X"+xValue);
                        newFileName = Regex.Replace(newFileName, @"Y(-?\d+(\.\d+)?)", "Y"+yValue);
                    }
                    else
                    {
                        tool.SaveHistoryToFile("未找到匹配座標,錯誤檔案名稱:" + newFileName);
                        return;
                    }

                    // 建立新的檔案完整路徑
                    string newFilePath = Path.Combine(folderPath, newFileName);

                    // 修改檔案名稱
                    File.Move(filePath, newFilePath);
                }
            }
            else
            {
                tool.SaveHistoryToFile("指定的資料夾不存在");
            }
        }

        public string Preview_Coordinate_Transform(string FileName, int ShiftX, int ShiftY)
        {
            string newFileName = "error";

            // 使用正則表達式提取數字
            Match xMatch = Regex.Match(FileName, @"X(-?\d+(\.\d+)?)");
            Match yMatch = Regex.Match(FileName, @"Y(-?\d+(\.\d+)?)");

            if (xMatch.Success && yMatch.Success)
            {
                string xValue = xMatch.Groups[1].Value;
                string yValue = yMatch.Groups[1].Value;

                xValue = (Int32.Parse(xValue) + ShiftX).ToString();
                yValue = (Int32.Parse(yValue) + ShiftY).ToString();

                newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
                newFileName = Regex.Replace(newFileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);                         
            }
            else
            {
                tool.SaveHistoryToFile("未找到匹配座標,錯誤檔案名稱:" + newFileName);
            }

            return newFileName;

            //// 檢查資料夾是否存在
            //if (Directory.Exists(folderPath))
            //{
            //    // 取得資料夾內的所有檔案
            //    string[] files = Directory.GetFiles(folderPath);

            //    foreach (string filePath in files)
            //    {
            //        // 建立新的檔案名稱
            //        string newFileName = "";
            //        string FileName = Path.GetFileName(filePath);

            //        // 使用正則表達式提取數字
            //        Match xMatch = Regex.Match(FileName, @"X(-?\d+(\.\d+)?)");
            //        Match yMatch = Regex.Match(FileName, @"Y(-?\d+(\.\d+)?)");

            //        if (xMatch.Success && yMatch.Success)
            //        {
            //            string xValue = xMatch.Groups[1].Value;
            //            string yValue = yMatch.Groups[1].Value;

            //            xValue = (Int32.Parse(xValue) + ShiftX).ToString();
            //            yValue = (Int32.Parse(yValue) + ShiftY).ToString();

            //            newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
            //            newFileName = Regex.Replace(newFileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);
            //        }
            //        else
            //        {
            //            tool.SaveHistoryToFile("未找到匹配座標,錯誤檔案名稱:" + newFileName);
            //            return;
            //        }

            //        // 建立新的檔案完整路徑
            //        string newFilePath = Path.Combine(folderPath, newFileName);

            //        // 修改檔案名稱
            //        File.Move(filePath, newFilePath);
            //    }
            //}
            //else
            //{
            //    tool.SaveHistoryToFile("指定的資料夾不存在");
            //}
        }

        public void Coordinate_MirrorXY(string folderPath1, bool IsMirrorX, bool IsMirrorY)
        {
            // 指定資料夾的路徑
            string folderPath = @"C:\Users\lankon\Desktop\abc";

            // 檢查資料夾是否存在
            if (Directory.Exists(folderPath))
            {
                // 取得資料夾內的所有檔案
                string[] files = Directory.GetFiles(folderPath);

                foreach (string filePath in files)
                {
                    // 建立新的檔案名稱
                    string newFileName = "";
                    string FileName = Path.GetFileName(filePath);

                    // 使用正則表達式提取數字
                    Match xMatch = Regex.Match(FileName, @"X(-?\d+(\.\d+)?)");
                    Match yMatch = Regex.Match(FileName, @"Y(-?\d+(\.\d+)?)");

                    if (xMatch.Success && yMatch.Success)
                    {
                        string xValue = xMatch.Groups[1].Value;
                        string yValue = yMatch.Groups[1].Value;
             
                        xValue = (Int32.Parse(xValue) * -1).ToString();
                        yValue = (Int32.Parse(yValue) * -1).ToString();

                        if(IsMirrorX && !IsMirrorY)
                        {
                            newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
                        }
                        else if(!IsMirrorX && IsMirrorY)
                        {
                            newFileName = Regex.Replace(FileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);
                        }
                        else
                        {
                            newFileName = Regex.Replace(FileName, @"X(-?\d+(\.\d+)?)", "X" + xValue);
                            newFileName = Regex.Replace(newFileName, @"Y(-?\d+(\.\d+)?)", "Y" + yValue);
                        }
                    }
                    else
                    {
                        tool.SaveHistoryToFile("未找到匹配座標");
                        return;
                    }

                    // 建立新的檔案完整路徑
                    string newFilePath = Path.Combine(folderPath, newFileName);

                    // 修改檔案名稱
                    File.Move(filePath, newFilePath);
                }
            }
            else
            {
                tool.SaveHistoryToFile("指定的資料夾不存在");
            }
        }



        public void FixFile(String Path)
        {
            // 輸入CSV文件的路徑
            //string csvFilePath = "path/to/your/file.csv";

            // 讀取CSV文件的所有行
            string[] lines = File.ReadAllLines(Path);

            // 修改CSV數據（這裡的例子是將第一列的第一個值修改為新的值）
            if (lines.Length > 0)
            {
                string[] values = lines[0].Split(',');

                if (values.Length > 0)
                {
                    // 修改數據
                    values[0] = "NewValue";

                    // 將修改後的數據重新組合為一行
                    lines[0] = string.Join(",", values);
                }
            }

            // 將修改後的數據寫回CSV文件
            File.WriteAllLines(Path, lines);
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //string selectedFileName = "";

            //// 設置文件選擇對話框的屬性
            //openFileDialog.Title = "Select TestData";
            //openFileDialog.Filter = "TestData|*.csv|All|*.*";

            //// 如果用戶選擇了文件，顯示文件名
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    selectedFileName = openFileDialog.FileName;
            //}

            //FixFile(selectedFileName);
            ////ReadCsvFile(selectedFileName);
            ////TxtBx_FilePath.Text = selectedFileName;
            ///
            //FixFileName(string folderPath1, -461, int ShiftY);
            //Mirror_XY("123", false, true);
            FixFileName("123", -461, 644);
        }

        private void Btn_LoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFileName = "";

            // 設置文件選擇對話框的屬性
            openFileDialog.Title = "Select TestData";
            openFileDialog.Filter = "TestData|*.dat|TestData|*.csv|All|*.*";

            // 如果用戶選擇了文件，顯示文件名
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFileName = openFileDialog.FileName;

                TxtBx_FilePath.Text = selectedFileName;
                TxtBx_OldName.Text = Path.GetFileName(selectedFileName);
            }            
        }

        int index;

        private void LstBx_Function_Click(object sender, EventArgs e)
        {
            index = LstBx_Function.SelectedIndex;


        }

        TabPage[] TabPage_Array = new TabPage[10];
        Object[] UserCtrl_Array = new Object[10];

        private void LstBx_Function_DoubleClick(object sender, EventArgs e)
        {
            object SelectItemName = LstBx_Function.SelectedItem;
            int PageCount = TabCtrl_Function.TabPages.Count;

            if (SelectItemName.ToString() == "Delete Last Condition")
            {
                if(PageCount == 1)
                {
                    tool.SaveHistoryToFile("無測試項可刪除");
                    return;
                }
                else
                {
                    TabCtrl_Function.TabPages.Remove(TabPage_Array[PageCount-1]);
                    return;
                }
            }
            else if(SelectItemName.ToString() == "Clear All Condition")
            {
                return;
            }
            
            if (index == LstBx_Function.SelectedIndex)
            {
                if(SelectItemName.ToString() == "Coordinate Mirror XY")
                {
                    SetUserCtrl_Coordinate_MirrorXY(TabPage_Array, "Coordinate_MirrorXY", PageCount, F_Coordinate_MirrorXY);
                }
                else if (SelectItemName.ToString() == "Coordinate Transform")
                {
                    SetUserCtrl_Coordinate_Transform(TabPage_Array, "Coordinate_Transform", PageCount, F_Coordinate_Transform);
                }
            }
        }


        UserCtrl_Coordinate_MirrorXY F_Coordinate_MirrorXY = new UserCtrl_Coordinate_MirrorXY();
        private void SetUserCtrl_Coordinate_MirrorXY(TabPage[] tabpage, string tabpage_name, int count, UserCtrl_Coordinate_MirrorXY user_control)
        {
            tabpage[count] = new TabPage(tabpage_name);

            // 可以设置 UserControl 的大小和其他属性
            user_control.Dock = DockStyle.Fill;
            user_control.Visible = true;

            // 将 UserControl 添加到 TabPage
            tabpage[count].Controls.Add(user_control);
            TabCtrl_Function.TabPages.Add(tabpage[count]);
        }

        UserCtrl_Coordinate_Transform F_Coordinate_Transform = new UserCtrl_Coordinate_Transform();
        private void SetUserCtrl_Coordinate_Transform(TabPage[] tabpage, string tabpage_name, int count, UserCtrl_Coordinate_Transform user_control)
        {
            tabpage[count] = new TabPage(tabpage_name);

            // 可以设置 UserControl 的大小和其他属性
            user_control.Dock = DockStyle.Fill;
            user_control.Visible = true;

            // 将 UserControl 添加到 TabPage
            tabpage[count].Controls.Add(user_control);
            TabCtrl_Function.TabPages.Add(tabpage[count]);
        }

        private void LstBx_Function_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        string sNewName;
        private void Btn_Preview_Click(object sender, EventArgs e)
        {
            if(TxtBx_FilePath.Text == "")
            {
                tool.SaveHistoryToFile("未選擇檔案");
                MessageBox.Show("Please Select File");
            }
            
            int PageCount = TabCtrl_Function.TabPages.Count;

            if(PageCount == 1)
            {
                tool.SaveHistoryToFile("未選擇方法");
                MessageBox.Show("Please Select Function");
            }

            for (int i = 1; i< PageCount; i++)
            {
                string methodName = TabPage_Array[i].Text;

                if(i == 1)
                {
                    sNewName = TxtBx_OldName.Text;
                }

                switch (methodName)
                {
                    case "Coordinate_MirrorXY":

                        

                        Coordinate_MirrorXY(TxtBx_FilePath.Text, false, true);
                        break;

                    case "Coordinate_Transform":
                        int X = F_Coordinate_Transform.GetShiftX();
                        int Y = F_Coordinate_Transform.GetShiftY();
                        sNewName = Preview_Coordinate_Transform(sNewName, X, Y);
                        break;
                }
            }

            TxtBx_NewName.Text = sNewName;                                  
        }
    }
}
