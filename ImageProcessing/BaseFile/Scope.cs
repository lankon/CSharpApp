using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonFunction;

namespace ImageProcessing
{
    public static partial class Scope
    {
        public static Panel MyStaticPanel;      //F_MainForm:Pnl_Group
        public static Panel MyStaticPanel_1;    //F_MainForm:Pnl_Group1

        public static MainTask ProcessTask = new MainTask();    //主執行緒

        public static int[] start_xy;    //暫時先寫在這
        public static int[] len;         //暫時先寫在這
        public static int[] orgin_xy;   //暫時先寫在這
        public static int[] orgin_len;  //暫時先寫在這
        public static int status;       //暫時先寫在這
        public static List<string> batch_path = new List<string>();
    }

    public static partial class Scope   //Wafer_Align_Angle Parameter
    {
        public static double WaferAngle;
    }
}
