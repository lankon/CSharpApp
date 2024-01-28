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

namespace CSharpApp
{
    class Tool
    {
        #region 寫檔
        public StreamWriter CreateFile(String Name, String Type, bool ContinueWrite)
        {            
            String Path;
            StreamWriter File;
            Path = System.IO.Directory.GetCurrentDirectory();
            Path = Path + "\\" + Name;
            Path += Type;

            if (ContinueWrite)
                File = new StreamWriter(Path, true, Encoding.Default);
            else
                File = new StreamWriter(Path);

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
        #endregion

        #region 寫Log
        public void SaveHistoryToFile(String Msg)
        {
            StreamWriter sw;
            DateTime currentTime = DateTime.Now;
            string Time = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
            
            sw = CreateFile("Log", ".txt", true);
            WriteFile(sw, "["+Time+"] " + Msg);
            CloseFile(sw);
        }

        #endregion

        #region 讀檔
        
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

        #endregion

        public String GetCurrentFunc()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame frame = stackTrace.GetFrame(1); // 使用 1 表示获取调用者的信息
            MethodBase method = frame.GetMethod();

            // 返回调用者的方法名称
            return method.Name;
        }


    }


}
