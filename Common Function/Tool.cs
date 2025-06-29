using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Drawing;
using System.Data;

namespace CommonFunction
{
    public static partial class Tool
    {
        #region 寫檔
        public static StreamWriter CreateFile(String Name, String Type, bool ContinueWrite)
        {            
            String path;
            StreamWriter File;
            path = System.IO.Directory.GetCurrentDirectory();
            path = path + "\\" + Name;
            path += Type;

            string directoryPath = Path.GetDirectoryName(path);

            CreateFolder(directoryPath);

            if (ContinueWrite)
                File = new StreamWriter(path, true, Encoding.Default);
            else
                File = new StreamWriter(path);

            return File;
        }

        public static void WriteFile(StreamWriter File, String Msg)
        {
            File.WriteLine(Msg);
        }

        public static void CloseFile(StreamWriter File)
        {
            File.Close();
        }
        public static void CreateFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                    SaveHistoryToFile("創建資料夾:" + folderPath);
                }
                catch (Exception ex)
                {
                    SaveHistoryToFile("無效的創建資料夾路徑");
                    SaveHistoryToFile($"Tool:CreateFolder Fail{ex}");
                }
            }
            else
            {
                //SaveHistoryToFile("資料夾已存在");
            }
        }
        #endregion

        #region 寫Log
        public static void SaveHistoryToFile(String Msg, string path = "defaut")
        {
            StreamWriter sw = null;
            DateTime currentTime = DateTime.Now;
            string Time = currentTime.ToString("MM-dd HH:mm:ss.fff");
            string Date = currentTime.ToString("yyyMMdd");

            if (path == "defaut")
                sw = CreateFile("History\\Log_" + Date, ".txt", true);
            else
                sw = CreateFile($"History\\{path}_Log_" + Date, ".txt", true);

            WriteFile(sw, "[" + Time + "] " + Msg);
            CloseFile(sw);
        }
        #endregion

        #region 讀檔
        public static List<Dictionary<string, string>> ReadCsvFile(String Path, bool HaveTitle)
        {
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

            try
            {
                // 使用 StreamReader 來讀取檔案
                using (StreamReader sr = new StreamReader(Path))
                {
                    if (sr == null) return data;

                    string[] headers = new string[200];// = sr.ReadLine().Split(',');

                    //若讀取資料沒有Title則填入Item方便搜尋
                    if(HaveTitle == false)
                    {
                        for (int i = 0; i < 200; i++)
                        {
                            headers[i] = "Item" + i.ToString();
                        }
                    }
                    
                    // 逐行讀取 CSV 檔案
                    while (!sr.EndOfStream)
                    {
                        // 讀取一行
                        string line = sr.ReadLine();

                        // 使用逗號分隔解析欄位
                        string[] fields = line.Split(',');

                        // 使用 Dictionary 來儲存每一行的資料
                        Dictionary<string, string> row = new Dictionary<string, string>();

                        // 將資料與欄位名稱對應起來
                        for (int i = 0; i < fields.Length; i++)
                        {
                            
                            //讀取的資料有Title的話則使用本身的Title
                            if(HaveTitle)
                            {
                                for(int j=0; j<fields.Length; j++)
                                {
                                    headers[j] = fields[j];
                                }

                                HaveTitle = false;
                            }
                            
                            row[headers[i]] = fields[i];

                            if (i > 199)
                            {
                                SaveHistoryToFile("Load Csv Item Over 200");
                                return data;
                            }
                        }

                        // 將此行的資料加入到 List 中
                        data.Add(row);

                    }

                    return data;
                }
            }
            catch
            {
                return data;
            }
        }
        #endregion

        #region 組態檔 寫入/讀取 config
        public static String ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return "Error";
            }
        }
        public static String AddUmpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                return "Success";
            }
            catch (ConfigurationErrorsException)
            {
                return "Error";
            }
        }
        #endregion

        #region 字串加密
        public static String FixPassword(String Password)
        {
            try
            {
                string ToReturn = "";
                string publickey = "19960321";
                string secretkey = "85032143";
                byte[] secretkeyByte = { };
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(Password);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte),
                                            CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        #endregion

        #region 型別轉換
        #region Enum to String List
        public static List<EnumName> EnumToStringList<EnumName>()
        {
            return Enum.GetValues(typeof(EnumName)).Cast<EnumName>().ToList<EnumName>();
        }

        public static double StringToDouble(string str)
        {
            double result;

            if (Double.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                SaveHistoryToFile("型別轉換錯誤");
                return -999;
            }           
        }
        #endregion

        #region IEEE754 to 十進制float
        public static void IEEE754ToFloat()
        {

        }
        #endregion

        public static int StringToInt(string str)
        {
            int result;

            if (Int32.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                SaveHistoryToFile("型別轉換錯誤");
                return -999;
            }
        }

        #endregion

        public static void CallExecute(string path, string input_command = "Non")
        {
            string workingDirectory = Path.GetDirectoryName(path);   //工作目錄

            // 使用 Process.Start 傳遞命令列參數
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = path,
                Arguments = $"\"{input_command}\"", // 將字串作為參數傳遞
                WorkingDirectory = workingDirectory, // 設定工作目錄為執行檔目錄
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                Process process = Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                SaveHistoryToFile(ex.ToString());
            }
        }
        public static void CloseExecute(string path)
        {
            string exeName = Path.GetFileNameWithoutExtension(path); // 取得執行檔名稱（不含副檔名）

            // 取得所有與 exeName 相同的處理程序
            Process[] processes = Process.GetProcessesByName(exeName);

            foreach (Process process in processes)
            {
                try
                {
                    // 確保是目標路徑的執行檔
                    if (process.MainModule.FileName.Equals(path, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"關閉進程: {process.ProcessName}");
                        process.CloseMainWindow(); // 優雅地關閉
                        process.WaitForExit(5000); // 等待 5 秒

                        if (!process.HasExited)
                        {
                            process.Kill(); // 強制結束
                            Console.WriteLine($"{process.ProcessName} 已被強制關閉");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"無法關閉 {exeName}: {ex.Message}");
                }
            }
        }

        #region 截圖相關功能
        public static void CaptureImage(Control ctrl, string filename)
        {            
            // 創建一個與 Panel 大小相同的 Bitmap
            Bitmap bitmap = new Bitmap(ctrl.Width, ctrl.Height);

            // 使用 Bitmap 的 Graphics 對象
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 定義 Panel 在螢幕上的位置
                Point panelLocationOnScreen = ctrl.PointToScreen(Point.Empty);

                // 將 Panel 當前的螢幕畫面複製到 Bitmap 上
                g.CopyFromScreen(panelLocationOnScreen, Point.Empty, ctrl.Size);
            }

            // 儲存 Bitmap 到檔案
            bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);

            // 釋放 Bitmap 資源
            bitmap.Dispose();
        }
        public static Dictionary<string, double> LoadImageToPicBx(PictureBox pic_bx, string filename)
        {
            using (Image img = Image.FromFile(filename)) // 從文件加載圖片
            {
                //// 計算縮放比例
                //float ratioX = (float)pnl.Width / img.Width;
                //float ratioY = (float)pnl.Height / img.Height;
                //float ratio = 0.0f;
                //if (ratioX > ratioY)
                //{
                //    ratio = ratioY;
                //    pic_bx.Height = pnl.Height;
                //    pic_bx.Height = pic_bx.Height * img.Width / img.Height;
                //    pic_bx.Location = new Point((int)(pnl.Width/2 + pic_bx.Width/2), 0);
                //}
                //else
                //{
                //    ratio = ratioX;
                //    pic_bx.Width = pnl.Width;
                //    pic_bx.Height = pic_bx.Width * img.Height / img.Width;
                //    pic_bx.Location = new Point(0, (int)(pnl.Height/2+pic_bx.Height/2));
                //}
                pic_bx.Image?.Dispose(); // 如果PictureBox已經有圖片，先釋放掉
                pic_bx.Image = new Bitmap(img); // 將加載的圖片設置到PictureBox中

                Dictionary<string, double> dic = new Dictionary<string, double>();

                int x = img.Width;
                int y = img.Height;

                dic.Add("width", x);
                dic.Add("height", y);

                return dic;
            } // 使用using語句確保加載的圖片資源在不再需要時釋放
        }
        #endregion

        #region DataGrid
        public static void DataGrid_AddRow(DataGridView dataGridView, string[] context)
        {
            if (context.Length != dataGridView.ColumnCount)
            {
                SaveHistoryToFile("新增行數與DataGrid行數不一致");
                return;
            }

            // 取得目前選取列的 index，如果沒有選取則預設加在最後
            int insertIndex = dataGridView.CurrentCell?.RowIndex ?? dataGridView.Rows.Count;

            // 插入一列
            dataGridView.Rows.Insert(insertIndex, context);
        }
        public static void DataGrid_DeleteRow(DataGridView dataGridView)
        {
            if (dataGridView.CurrentRow != null && !dataGridView.CurrentRow.IsNewRow)
            {
                dataGridView.Rows.Remove(dataGridView.CurrentRow);
            }
        }
        public static void DataGrid_RowUp(DataGridView dataGridView)
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
        public static bool DataGrid_DataSave(DataGridView dataGridView, string file_name)
        {
            bool res = false;

            SaveHistoryToFile($"{file_name}儲存Start");

            string FolderPath = Application.StartupPath + @"\Setting";
            CreateFolder(FolderPath);

            string file_path = FolderPath + @"\" + file_name;

            // 將 DataGridView 資料轉換為 DataTable
            DataTable dt = new DataTable("IOTable");

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
            catch (Exception ex)
            {
                res = false;
                return res;
            }

            SaveHistoryToFile($"{file_name}儲存End");

            return res;
        }
        public static bool DataGrid_DataLoad(DataGridView dataGridView, string file_name)
        {
            bool res = true;

            string FolderPath = Application.StartupPath + @"\Setting";

            string file_path = FolderPath + @"\" + file_name;

            DataSet ds = new DataSet();

            ds.ReadXml(file_path);

            DataTable dt = ds.Tables[0];

            dataGridView.Rows.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                int rowIndex = dataGridView.Rows.Add();

                //// 對應欄位名稱來設定資料（需與 DataGridView 欄位名稱一致）
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    if (dt.Columns.Contains(col.Name))
                    {
                        dataGridView.Rows[rowIndex].Cells[col.Name].Value = dr[col.Name];
                    }
                }
            }

            return res;
        }
        public static void DataGrid_RowDown(DataGridView dataGridView)
        {
            // 確保選取的列不為 null 且不是最後一列
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.Index < dataGridView.Rows.Count - 1)
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
    }

    //************* Form *******************
    public static partial class Tool
    {
        public static void SetForm(Panel pnl, Form form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);
        }

        public static void CloseFormOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Close();
                    ((Form)control).Dispose();
                    break;
                }
            }
        }

        public static void HideElementOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Hide();
                    //break; 
                }
                else if (control is Button && control.Visible == true)
                {
                    ((Button)control).Visible = false;
                }
                else if (control is Label && control.Visible == true)
                {
                    ((Label)control).Visible = false;
                }
            }
        }

        /// <summary>
        /// 顯示Form Name,Debug的時後使用,其他用途請勿使用,會持續占用記憶體
        /// </summary>
        /// <param name="form"></param>
        public static void ShowFormName(Form form, int pos = 0)
        {
            // 建立 Panel
            Panel namePanel = new Panel();
            namePanel.Name = "FormNamePanel";
            namePanel.BackColor = Color.FromArgb(0, 0, 0, 128); // 半透明黑底
            namePanel.Size = new Size(350, 30);

            if(pos == 0)
                namePanel.Location = new Point(0, 0); // 左上
            else if(pos == 1)
                namePanel.Location = new Point(form.Width - 350, 0); // 右上

            namePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            namePanel.BringToFront(); // 確保在最上層

            // 建立 Label
            Label nameLabel = new Label();
            nameLabel.Text = form.Name; // 或 form.Text 看你想顯示什麼
            nameLabel.ForeColor = Color.Red;
            nameLabel.AutoSize = false;
            nameLabel.Dock = DockStyle.Fill;
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            nameLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // 加入 Label 到 Panel
            namePanel.Controls.Add(nameLabel);

            // 加入 Panel 到 Form
            form.Controls.Add(namePanel);
            namePanel.BringToFront(); // 再次確保最上層
        }

        public static void ReleaseButtonImages(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Image?.Dispose();
                    btn.Image = null;

                    btn.BackgroundImage?.Dispose();
                    btn.BackgroundImage = null;
                }

                if (ctrl.HasChildren)
                    ReleaseButtonImages(ctrl);
            }
        }
    }
    
}



