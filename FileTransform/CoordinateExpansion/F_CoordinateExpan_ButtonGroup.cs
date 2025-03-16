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
    public partial class F_CoordinateExpan_ButtonGroup : Form
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
        public void SetF_CoordinateExpan_ButtonGroup(Panel pnl, F_CoordinateExpan_ButtonGroup form)
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

        public F_CoordinateExpan_ButtonGroup()
        {
            InitializeComponent();

            InitialApplication();
        }

        private void Btn_Setting_Click(object sender, EventArgs e)
        {
            CloseFormOnPanel(Scope.MyStaticPanel);

            F_CoordinateExpanSetting f_CoordinateExpanSetting = new F_CoordinateExpanSetting();
            f_CoordinateExpanSetting.SetF_CoordinateExpanSetting(Scope.MyStaticPanel, f_CoordinateExpanSetting);
            f_CoordinateExpanSetting.Show();
        }
    }
}
