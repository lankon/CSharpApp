using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstrumentTest
{
    public partial class F_TC_ButtonGroup : Form
    {
        #region public function
        public void SetF_TC_ButtonGroup(Panel pnl, F_TC_ButtonGroup form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion


        public F_TC_ButtonGroup()
        {
            InitializeComponent();
        }
    }
}
