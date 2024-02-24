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
    public partial class F_Test : Form
    {
        public F_Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Context context = new Context(new Task_LoadCell());

            while(true)
            {
                context.Request();
                context.Get_SubTaskState();
            }
                
        }
    }
}
