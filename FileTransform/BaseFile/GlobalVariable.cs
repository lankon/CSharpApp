using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileTransform
{
    class GlobalVariable
    {
        public static Panel MyStaticPanel;      //F_MainForm:Pnl_Group
        public static Panel MyStaticPanel_1;    //F_MainForm:Pnl_Group1

        public static int[] start_xy;    //暫時先寫在這
        public static int[] len;         //暫時先寫在這
        public static int[] orgin_xy;   //暫時先寫在這
        public static int[] orgin_len;  //暫時先寫在這
        public static int status;       //暫時先寫在這
        public static List<string> batch_path = new List<string>();
    }
}
