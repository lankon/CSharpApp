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
    public partial class F_CoordinateExpanSetting : Form
    {
        #region public function
        public void SetF_CoordinateExpanSetting(Panel pnl, F_CoordinateExpanSetting form)
        {
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            form.TopLevel = false;
            form.Top = 0;
            form.Left = 0;

            pnl.Controls.Add(form);

            form.Hide();
        }
        #endregion

        #region privaet function
        private void Initial()
        {
            ApplicationSetting.ReadAllRecipe<FormItem>();
            ApplicationSetting.UpdataRecipeToForm<FormItem>(this);

            ExpansionType_Show();
        }
        private void ExpansionType_Show()
        {
            ApplicationSetting.SetRecipe((int)FormItem.Cmbx_ExpansionType, Cmbx_ExpansionType.SelectedIndex.ToString());

            int index = ApplicationSetting.Get_Int_Recipe((int)FormItem.Cmbx_ExpansionType);

            if (index == 0)
            {
                TxtBx_RadiusExpansion.Enabled = false;
                TxtBx_X_Expansion.Enabled = true;
                TxtBx_Y_Expansion.Enabled = true;
            }
            else if(index == 1) 
            {
                TxtBx_RadiusExpansion.Enabled = true;
                TxtBx_X_Expansion.Enabled = false;
                TxtBx_Y_Expansion.Enabled = false;
            }
        }

        #endregion


        public F_CoordinateExpanSetting()
        {
            InitializeComponent();

            Initial();
        }

        private void F_CoordinateExpanSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationSetting.SaveAllRecipe(this);
            ApplicationSetting.ReadAllRecipe<FormItem>();

            this.Dispose(); // 显式调用 Dispose 以释放资源

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void F_CoordinateExpanSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ApplicationSetting.SaveAllRecipe(this);
                ApplicationSetting.ReadAllRecipe<FormItem>();
            }
        }

        private void Cmbx_ExpansionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExpansionType_Show();
        }
    }
}
