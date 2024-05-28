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
    public partial class F_TemperatureController : Form
    {
        #region parameter define
        
        #endregion

        #region private function
        private void InitialApplication()
        {
            
        }

        #endregion

        #region public function
        public void SetF_TemperatureController(Panel pnl, F_TemperatureController form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = false;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;
            form.TopMost = true;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion
        public F_TemperatureController()
        {
            InitializeComponent();

            InitialApplication();
        }
    }
}
