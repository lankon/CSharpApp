using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;


namespace CommonFunction
{
    public class Licence
	{
        public bool CheckLicence()
        {
            string file_path = System.IO.Directory.GetCurrentDirectory() + @"\Temp\Licence.ini";

            if(!File.Exists(file_path))
            {
                // 設定一個日期（例如，三天前的日期）
                DateTime targetDate = new DateTime(2024, 10, 14);

                // 取得目前時間
                DateTime currentDate = DateTime.Now;

                // 計算日期之間的差異
                TimeSpan difference = currentDate - targetDate;

                // 判斷是否在三天內
                if (Math.Abs(difference.TotalDays) > 3)
                    return false;

                CreateLicenceFile();
                return true;
            }
            else
            {
                string sLicence;
                string serial_num;

                sLicence = ReadLicenceFile();
                serial_num = GetSerialNum();

                serial_num = FixPassword(serial_num);

                if (sLicence == serial_num)
                    return true;
                else
                    return false;
            }
        }
        private void CreateLicenceFile()
        {
            string serial_num;
            serial_num = GetSerialNum();
            serial_num = FixPassword(serial_num);

            //寫檔 serial_num
            string Path;
            StreamWriter File;
            Path = System.IO.Directory.GetCurrentDirectory();
            Path = Path + @"\Temp\Licence.ini";

            File = new StreamWriter(Path);

            File.WriteLine(serial_num);

            File.Close();
        }
        private string ReadLicenceFile()
        {
            string filePath = System.IO.Directory.GetCurrentDirectory() + @"\Temp\Licence.ini";
                      
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    return line;
                }
            }
            
            return "";
        }
        private string GetSerialNum()
        {
            string serialNumber = "";
            try
            {
                // 创建一个管理对象搜索器，用于查询WMI信息
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

                // 遍历查询结果
                foreach (ManagementObject obj in searcher.Get())
                {
                    // 获取Hard Disk序列号
                    serialNumber = obj["SerialNumber"].ToString();
                    break;
                }
            }
            catch
            {
                serialNumber = "Default Number";
            }

            return serialNumber;
        }
        private String FixPassword(String Password)
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
    }
}


