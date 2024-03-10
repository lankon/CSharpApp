using System;
using System.IO;
using System.Management;


namespace CommonFunction
{
    class Licence
	{
        public bool CheckLicence()
        {
            string file_path = System.IO.Directory.GetCurrentDirectory() + @"\Temp\Licence.ini";

            if(!File.Exists(file_path))
            {
                CreateLicenceFile();
                return true;
            }
            else
            {
                string sLicence;
                string serial_num;

                sLicence = ReadLicenceFile();
                serial_num = GetSerialNum();

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



	}
}


