using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using CommonFunction;

namespace FileTransform
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Tool tool = new Tool();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string parentProcessName = GetParentProcessName();

            if (args.Length > 0)
            {
                string input = args[0];

                if(input == "CallServer")
                {
                    tool.SaveHistoryToFile("CallServer");
                    Application.Run(new MyApplicationContext());
                }
                else
                {
                    tool.SaveHistoryToFile("a");
                    Application.Run(new F_MainForm(""));
                }
            }
            else
            {
                tool.SaveHistoryToFile("b");
                Application.Run(new F_MainForm(""));
            }

            //if (parentProcessName == "explorer")
            //{
            //    Application.Run(new F_MainForm("ClickStart"));  //點擊執行檔啟動
            //}
            //else
            //{
            //    Application.Run(new MyApplicationContext());
            //}
        }

        static string GetParentProcessName()
        {
            //try
            //{
            //    using (var currentProcess = Process.GetCurrentProcess())
            //    {
            //        var parentProcessId = new PerformanceCounter("Process", "Creating Process ID", currentProcess.ProcessName).RawValue;
            //        var parentProcess = Process.GetProcessById((int)parentProcessId);
            //        return parentProcess.ProcessName.ToLower();
            //    }
            //}
            //catch
            //{
            //    return "unknown";
            //}

            try
            {
                using (var currentProcess = Process.GetCurrentProcess())
                {
                    int parentProcessId = 0;

                    // 使用 PerformanceCounter 取得父進程 ID
                    using (var performanceCounter = new PerformanceCounter("Process", "Creating Process ID", currentProcess.ProcessName))
                    {
                        parentProcessId = (int)performanceCounter.RawValue;
                    }

                    // 找到父進程
                    var parentProcess = Process.GetProcessById(parentProcessId);
                    return parentProcess.ProcessName.ToLower();
                }
            }
            catch
            {
                return "unknown";
            }
        }
    }

    public class MyApplicationContext : ApplicationContext
    {
        public MyApplicationContext()
        {
            // 啟動應用程式邏輯
            InitialApplication();
        }

        private void InitialApplication()
        {
            // 初始化程式
            //MessageBox.Show("Application Started!");

            F_MainForm f_MainForm = new F_MainForm("ProgramStart");
            // 結束應用程式
            //ExitThread();
        }
    }
}
