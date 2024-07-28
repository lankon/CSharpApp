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

namespace FileTransform
{
    public partial class F_NearField_ButtonGroup : Form
    {
        #region private function
        private void InitialApplication()
        {
            SetHint();

            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);
        }
        private void SetHint()
        {
            toolTip1.SetToolTip(Btn_Setting, "Setting");
        }
        private void CloseFormOnPanel(Panel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is Form && control.Visible == true)
                {
                    ((Form)control).Close();
                    ((Form)control).Dispose();
                    break;
                }
            }
        }
        #endregion

        #region public function
        public void SetF_NearFiled_ButtonGroup(Panel pnl, F_NearField_ButtonGroup form)
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

        public F_NearField_ButtonGroup()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            CloseFormOnPanel(GlobalVariable.MyStaticPanel);

            F_NearField_Setting f_NearField_Setting = new F_NearField_Setting();
            f_NearField_Setting.SetF_NearField_Setting(GlobalVariable.MyStaticPanel, f_NearField_Setting);
            f_NearField_Setting.Show();
        }
    }
}
