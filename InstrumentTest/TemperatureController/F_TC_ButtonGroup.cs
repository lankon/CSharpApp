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
    public delegate void Show_TC_FormCallBack ();
    
    public partial class F_TC_ButtonGroup : Form
    {
        #region parameter define
        public Show_TC_FormCallBack Show_TC_Form;
        #endregion

        #region private function
        private void HideElementOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Hide();
                    //break; 
                }
                else if (control is Button && control.Visible == true)
                {
                    ((Button)control).Visible = false;
                }
                else if (control is Label && control.Visible == true)
                {
                    ((Label)control).Visible = false;
                }
            }
        }
        private void InitialApplication()
        {
            ShowHint();
        }
        private void ShowHint()
        {
            toolTip1.SetToolTip(Btn_Back, "Back");
            toolTip1.SetToolTip(Btn_Setting, "Setting");
            toolTip1.SetToolTip(Btn_Setting_TPT8000, "TPT8000 Setting");
            toolTip1.SetToolTip(Btn_Show_PV, "Show PV");
        }
        #endregion

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

            InitialApplication();
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            HideElementOnPanel(F_MainForm.MyStaticPanel);

            F_TC_Setting f_TC_Setting = new F_TC_Setting();
            f_TC_Setting.SetF_TC_Setting(F_MainForm.MyStaticPanel, f_TC_Setting);
            f_TC_Setting.Show();
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            Show_TC_Form();
        }

        private void Btn_Setting_TPT8000_Click(object sender, EventArgs e)
        {
            HideElementOnPanel(F_MainForm.MyStaticPanel);

            F_TC_Setting_TPT8000 f_TC_Setting_TPT8000 = new F_TC_Setting_TPT8000();
            f_TC_Setting_TPT8000.SetF_TC_Setting_TPT8000(F_MainForm.MyStaticPanel, f_TC_Setting_TPT8000);
            f_TC_Setting_TPT8000.Show();
        }

        private void Btn_Show_PV_Click(object sender, EventArgs e)
        {
            HideElementOnPanel(F_MainForm.MyStaticPanel);

            F_Show_PV_TPT8000 f_Show_PV_TPT8000 = new F_Show_PV_TPT8000();
            f_Show_PV_TPT8000.SetF_Show_PV_TPT8000(F_MainForm.MyStaticPanel, f_Show_PV_TPT8000);
            f_Show_PV_TPT8000.Show();

            GlobalVariable.Task_TC.MonitorAll(true);
        }
    }
}
