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
        F_TC_ButtonGroup f_TC_ButtonGroup;
        F_LoadCell f_LoadCell = new F_LoadCell();
        F_TemperatureController f_TemperatureController = new F_TemperatureController();
        F_Communication f_Communication = new F_Communication();
        #endregion

        #region private function
        private void InitialApplication()
        {
            
        }
        private void Show_TC_Form()
        {
            HideElementOnPanel(GlobalVariable.MyStaticPanel);

            f_TemperatureController.Show();
        }
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

            form.Show();
        }
        #endregion

        public F_SelectApp()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_DeltaLoadCell_Click(object sender, EventArgs e)
        {
            HideElementOnPanel(GlobalVariable.MyStaticPanel);

            f_LoadCell.SetF_LoadCell(GlobalVariable.MyStaticPanel, f_LoadCell);
            f_LoadCell.Show();
        }

        private void Btn_TemperatureControl_Click(object sender, EventArgs e)
        {
            HideElementOnPanel(GlobalVariable.MyStaticPanel);

            f_TemperatureController.SetF_TemperatureController(GlobalVariable.MyStaticPanel, f_TemperatureController);
            f_TemperatureController.Show();

            f_TC_ButtonGroup = new F_TC_ButtonGroup();
            f_TC_ButtonGroup.SetF_TC_ButtonGroup(GlobalVariable.MyStaticPanel_1, f_TC_ButtonGroup);
            f_TC_ButtonGroup.Show();

            F_Show_PV_TPT8000 f_Show_PV_TPT8000 = new F_Show_PV_TPT8000();

            f_TC_ButtonGroup.Show_TC_Form += Show_TC_Form;
        }

        private void Btn_Communication_Click(object sender, EventArgs e)
        {
            HideElementOnPanel(GlobalVariable.MyStaticPanel);

            f_Communication.SetF_Communication(GlobalVariable.MyStaticPanel, f_Communication);
            f_Communication.Show();
        }
    }
}
