using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapping
{
    public partial class F_GRR : Form
    {
        #region public function
        public void SetF_GRR(Panel pnl, F_GRR form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion

        public F_GRR()
        {
            InitializeComponent();
        }

        private void Btn_Calculate_Click(object sender, EventArgs e)
        {

        }
    }
}
