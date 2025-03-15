using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommonFunction;

namespace InstrumentTest
{
    public partial class F_Communication : Form
    {
        Rs232 rs232 = new Rs232();
       
        
        #region private function
        
        private void SwitchForm()
        {

        }
        #endregion


        #region public function
        public void SetF_Communication(Panel pnl, F_Communication form)
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

        public F_Communication()
        {
            InitializeComponent();
        }

        private void Btn_Open_Click(object sender, EventArgs e)
        {
            if (rs232.Open("COM5", 19200, 0, 8, 1))
                MessageBox.Show("連線成功");
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            rs232.Close();
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            rs232.SenMsg(TxtBx_Msg.Text);
        }
    }
}
