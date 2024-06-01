using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using CommonFunction;

namespace InstrumentTest
{
    public partial class F_TemperatureController : Form
    {
        #region parameter define
        Task_TemperatureController Task_TC = new Task_TemperatureController(); 
        #endregion

        #region private function
        private void InitialApplication()
        {
            ShowHint();

            ApplicationSetting.ReadAllRecipe<eFormAppSet>();
            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);
           
            Task_TC.UpdatePresentValue += Update_PV;
            Task_TC.UpdateSetValue += Update_SV;
        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Btn_Connect, "Connect");
            toolTip1.SetToolTip(Btn_DisConnect, "DisConnect");
        }
        private void Update_PV(double value)
        {
            if (Labl_PV.InvokeRequired)
            {
                Labl_PV.Invoke(new Action(() => Labl_PV.Text = value.ToString("0.0")));
            }
            else
            {
                Labl_PV.Text = value.ToString();
            }
        }
        private void Update_SV(double value)
        {
            if (Labl_SV.InvokeRequired)
            {
                Labl_SV.Invoke(new Action(() => Labl_SV.Text = value.ToString("0.0")));
            }
            else
            {
                Labl_SV.Text = value.ToString();
            }
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

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            Task_TC.Connect();

            Thread.Sleep(100);

            if(Task_TC.GetError() == "")
            {
                Btn_Connect.Visible = false;
                Btn_DisConnect.Visible = true;
            }
            else
            {
                MessageBox.Show("TC Connect Fail");
            }
        }   

        private void Btn_DisConnect_Click(object sender, EventArgs e)
        {
            Task_TC.DisConnect();

            Thread.Sleep(100);

            if (Task_TC.GetError() == "")
            {
                Btn_Connect.Visible = true;
                Btn_DisConnect.Visible = false;

            }
            else
            {
                MessageBox.Show("TC DisConnect Fail");
            }
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {

        }
    }
}
