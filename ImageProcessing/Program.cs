using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommonFunction;

namespace ImageProcessing
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

                if (input == "CallServer")
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
                Application.Run(new F_MainForm(""));
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
}
