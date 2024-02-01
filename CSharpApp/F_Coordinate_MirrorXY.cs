using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpApp
{
    public partial class F_Coordinate_MirrorXY : Form
    {
        public F_Coordinate_MirrorXY()
        {
            InitializeComponent();
        }

        public bool GetMirrorX()
        {
            if (CkBx_MirrorX.Checked == true)
                return true;
            else
                return false;
        }

        public bool GetMirrorY()
        {
            if (CkBx_MirrorY.Checked == true)
                return true;
            else
                return false;
        }
    }
}
