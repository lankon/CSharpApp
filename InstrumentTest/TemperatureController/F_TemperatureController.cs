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
        //Task_TemperatureController Task_TC = new Task_TemperatureController();
        #endregion

        #region private function
        private void InitialApplication()
        {
            ShowHint();

            ApplicationSetting.ReadAllRecipe<eFormAppSet>();
            ApplicationSetting.UpdataRecipeToForm<eFormAppSet>(this);
           
            GlobalVariable.Task_TC.UpdatePresentValue += Update_PV;
            GlobalVariable.Task_TC.UpdateSetValue += Update_SV;
        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Btn_Connect, "Connect");
            toolTip1.SetToolTip(Btn_DisConnect, "DisConnect");
            toolTip1.SetToolTip(Btn_Start, "Start");
            toolTip1.SetToolTip(Btn_Stop, "Stop");
            toolTip1.SetToolTip(Panel_ShowFormName, "F_TemperatureController");
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
            GlobalVariable.Task_TC.Connect();

            Thread.Sleep(500);

            if(GlobalVariable.Task_TC.GetError() == "")
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
            GlobalVariable.Task_TC.DisConnect();

            Thread.Sleep(500);

            if (GlobalVariable.Task_TC.GetError() == "")
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
            GlobalVariable.Task_TC.Start();

            Labl_SV.Text = TxtBx_Target.Text;

            Thread.Sleep(500);

            if (GlobalVariable.Task_TC.GetError() == "")
            {
                //Btn_Start.Visible = false;
                //Btn_Stop.Visible = true;
            }
            else
            {
                MessageBox.Show("TC Start Fail");
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            GlobalVariable.Task_TC.Stop();

            Thread.Sleep(500);

            if (GlobalVariable.Task_TC.GetError() == "")
            {
                //Btn_Start.Visible = true;
                //Btn_Stop.Visible = false;
            }
            else
            {
                MessageBox.Show("TC Stop Fail");
            }
        }

        private void Btn_StartAll_Click(object sender, EventArgs e)
        {
            GlobalVariable.Task_TC.StartAll();

            Labl_SV.Text = TxtBx_Target.Text;

            Thread.Sleep(500);

            if (GlobalVariable.Task_TC.GetError() == "")
            {
                Btn_StartAll.Visible = false;
                Btn_StopAll.Visible = true;
            }
            else
            {
                MessageBox.Show("TC Start All Fail");
            }
        }

        private void Btn_StopAll_Click(object sender, EventArgs e)
        {
            GlobalVariable.Task_TC.StopAll();

            Thread.Sleep(500);

            if (GlobalVariable.Task_TC.GetError() == "")
            {
                Btn_StartAll.Visible = true;
                Btn_StopAll.Visible = false;
            }
            else
            {
                MessageBox.Show("TC Stop All Fail");
            }
        }
		
		private void TxtBx_Target_TextChanged(object sender, EventArgs e)
        {
            ApplicationSetting.SetRecipe((int)eFormAppSet.TxtBx_Target, TxtBx_Target.Text);
            ApplicationSetting.SaveAllRecipe(this);
            ApplicationSetting.ReadAllRecipe<eFormAppSet>();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
