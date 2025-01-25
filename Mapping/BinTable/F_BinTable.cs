using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapping.BinTable
{
    public partial class F_BinTable : Form
    {
        #region public function
        public void SetF_Mapping_ButtonGroup(Panel pnl, F_BinTable form)
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



        public F_BinTable()
        {
            InitializeComponent();
        }
    }
}
