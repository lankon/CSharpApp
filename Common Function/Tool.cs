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

namespace CommonFunction
{
    public class Tool
    {
        #region 寫檔
        public StreamWriter CreateFile(String Name, String Type, bool ContinueWrite)
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

        public void WriteFile(StreamWriter File, String Msg)
        {
            File.WriteLine(Msg);
        }

        public void CloseFile(StreamWriter File)
        {
            File.Close();
        }
        public void CreateFolder(string folderPath)
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
        public void SaveHistoryToFile(String Msg, string path = "defaut")
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
        public List<Dictionary<string, string>> ReadCsvFile(String Path, bool HaveTitle)
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
        public String ReadSetting(string key)
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
        public String AddUmpdateAppSettings(string key, string value)
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
        public String FixPassword(String Password)
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
        public List<EnumName> EnumToStringList<EnumName>()
        {
            return Enum.GetValues(typeof(EnumName)).Cast<EnumName>().ToList<EnumName>();
        }

        public double StringToDouble(string str)
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
        public void IEEE754ToFloat()
        {

        }
        #endregion

        public int StringToInt(string str)
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

        #region 呼叫/關閉執行檔
        public void CallExecute(string path, string input_command = "Non")
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
        public void CloseExecute(string path)
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
        #endregion

        #region 截圖相關功能
        public void CaptureImage(Control ctrl, string filename)
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
        public Dictionary<string, double> LoadImageToPicBx(PictureBox pic_bx, string filename)
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
    }
}



