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
    public partial class F_SelectApp : Form
    {
        #region parameter define 
        F_LoadCell f_LoadCell = new F_LoadCell();

        #endregion


        #region public function
        public void SetF_SelectApp(Panel pnl, F_SelectApp form)
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

        public F_SelectApp()
        {
            InitializeComponent();
        }

        private void Btn_DeltaLoadCell_Click(object sender, EventArgs e)
        {

        }

        private void Btn_TemperatureController_Click(object sender, EventArgs e)
        {

        }
    }
}
